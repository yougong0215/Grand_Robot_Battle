global.UserList = {};

class PlayerForm {
    name = undefined;
    socket = undefined;
    ready = false; // ready가 false면 사용자가 종료할때 데이터를 저장하지 않음 (데이터가 잘못됬거나 오류로 데이터 손실 방지)
    coin = 0;
    crystal = 0;
    level = 0;
    exp = 0;
    join = 0;
    clearStory = 0;

    // 인벤토리
    inventory = {
        equipment: {},
        item: {}
    }
    // 프로필
    profile = {
        prefix: null, // 칭호
        picture: null // 프사
    }
    // 로봇 프리셋
    preset = {
        left: null,
        right: null,
        head: null,
        body: null,
        leg: null
    }

    constructor(name, socket, avatar) {
        this.name = name;
        this.socket = socket;
        this.avatarURL = avatar || null;
    }
}

const sqlite = require("../../utils/sqlite.js");
exports.AddPlayer = async function(id, socket, avatar) {
    const sql = sqlite.GetObject();
    const UserData = await sql.Aget("SELECT name FROM users WHERE id = ?", id);
    if (socket.readyState !== "open") return; // 머야 연결이 끊겨있네
    if (UserData === false || UserData === undefined || UserData.name === undefined) {
        socket.kick("유저 정보를 불러올 수 없습니다.");
        sql.close();
        return;
    }

    const Player = UserList[id] = new PlayerForm(UserData.name, socket, avatar);
    console.log(`[UserManager] ${Player.name}(${id})님이 서버를 접속하였습니다.`);

    // 플레이어 정보들을 불러오자
    ///////////////// 코인, 레벨등 /////////////////
    const PlayerStats = await sql.Aget("SELECT * FROM stats WHERE id = ?", id);
    if (PlayerStats === false) { // 데베 오류
        socket.kick("유저 정보를 불러올 수 없습니다. (2)");
        sql.close();
        return;
    }

    if (PlayerStats) { // 정보들이 있으면 (만약 없다면 다 0임)
        Player.coin = PlayerStats.coin;
        Player.crystal = PlayerStats.crystal;
        Player.level = PlayerStats.level;
        Player.exp = PlayerStats.exp;
        Player.join = PlayerStats.join;
        Player.clearStory = PlayerStats.clear_story;
    }

    ///////////////// 인벤토리 /////////////////
    const PlayerInventory = await sql.Aget("SELECT equipment,item FROM inventory WHERE id = ?", id);
    if (PlayerInventory === false) {
        socket.kick("유저 정보를 불러올 수 없습니다. (3)");
        sql.close();
        return;
    }

    if (PlayerInventory) { // 정보들이 있으면 인벤 불러오고 아니면 초기화값
        Player.inventory.equipment = JSON.parse(PlayerInventory.equipment);
        Player.inventory.item = JSON.parse(PlayerInventory.item);
    }

    ///////////////// 프리셋 /////////////////
    const PlayerPreset = await sql.Aget("SELECT * FROM preset WHERE id = ?", id);
    if (PlayerPreset === false) {
        socket.kick("유저 정보를 불러올 수 없습니다. (4)");
        sql.close();
        return;
    }

    if (PlayerPreset) {
        Player.preset.left = PlayerPreset.left;
        Player.preset.right = PlayerPreset.right;
        Player.preset.head = PlayerPreset.head;
        Player.preset.body = PlayerPreset.body;
        Player.preset.leg = PlayerPreset.leg;
    }

    sql.close(); // 데베 사용 끝남
    Player.ready = true; // 준비 완료

    // 접속 카운트 올리기
    Player.join ++;

    // 클라이언트한테 준비 되었다고 알림
    Player.socket.send("Server.PlayerReady", null);
}

const RoomManager = require("../InGame/RoomManager.js");
const MatchManager = require("../InGame/main.js");
exports.RemovePlayer = async function(id) {
    const CachePlayer = UserList[id];
    if (CachePlayer === undefined) return;
    delete UserList[id];

    console.log(`[UserManager] ${CachePlayer.name}(${id})님이 서버를 나갔습니다.`);
    if (!CachePlayer.ready) { // 비정상적으로 종료하면 데이터 손실을 방지하기 위해 저장하지 않음
        console.log(`[UserManager_Warning] ${CachePlayer.name}(${id})님이 비정상적으로 종료되었습니다.`);
        return;
    }

    // ! 주의 ! 꼭 다 사용하면 sql.close를 해서 connection 을 끊어야 함
    const sql = sqlite.GetObject();

    // 재화 저장
    sql.run("INSERT OR REPLACE INTO stats (id, coin, crystal, level, exp, `join`, clear_story) VALUES ($id, $coin, $crystal, $level, $exp, $join, $clear_story)", {
        $id: id,
        $coin: CachePlayer.coin,
        $crystal: CachePlayer.crystal,
        $level: CachePlayer.level,
        $exp: CachePlayer.exp,
        $join: CachePlayer.join,
        $clear_story: CachePlayer.clearStory
    }, err => { if (err) console.error(err) });

    // 인벤
    sql.run("INSERT OR REPLACE INTO inventory (id, equipment, item) VALUES ($id, $equipment, $item)", {
        $id: id,
        $equipment: JSON.stringify(CachePlayer.inventory.equipment),
        $item: JSON.stringify(CachePlayer.inventory.item)
    }, err => { if (err) console.error(err) });

    // 프리셋
    sql.run("INSERT OR REPLACE INTO preset (id, left, right, head, body, leg) VALUES ($id, $left, $right, $head, $body, $leg)", {
        $id: id,
        $left: CachePlayer.preset.left,
        $right: CachePlayer.preset.right,
        $head: CachePlayer.preset.head,
        $body: CachePlayer.preset.body,
        $leg: CachePlayer.preset.leg,
    }, err => { if (err) console.error(err) });

    sql.close();

    // 탈주 확인
    if (MatchManager.MatchPlayers.has(id)) // 매치 대기열 해제
        MatchManager.MatchPlayers.delete(id);

    const turnRoom = RoomManager.getRoomToPlayer(id);
    if (turnRoom !== undefined) { // 턴제 게임중에 탈주
        turnRoom.PlayerLeft(id, CachePlayer.name);
    }
}