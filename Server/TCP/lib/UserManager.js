global.UserList = {};

class PlayerForm {
    name = undefined;
    socket = undefined;
    ready = false; // ready가 false면 사용자가 종료할때 데이터를 저장하지 않음 (데이터가 잘못됬거나 오류로 데이터 손실 방지)
    coin = 0;
    crystal = 0;
    level = 0;
    exp = 0;

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

    constructor(name, socket) {
        this.name = name;
        this.socket = socket;
    }
}

const sqlite = require("../../utils/sqlite.js");
exports.AddPlayer = async function(id, socket) {
    if (UserList[id] !== undefined) { // 이미 접속해있다!!
        // 나중에 이미 접속한거 처리 해야함
        return;
    }

    const sql = sqlite.GetObject();
    const UserData = await sql.Aget("SELECT name FROM users WHERE id = ?", id);
    if (UserData === false || UserData === undefined || UserData.name === undefined) {
        socket.kick("유저 정보를 불러올 수 없습니다.");
        sql.close();
        return;
    }

    const Player = UserList[id] = new PlayerForm(UserData.name, socket);
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

    sql.close(); // 데베 사용 끝남
    Player.ready = true; // 준비 완료

    // 클라이언트한테 준비 되었다고 알림
    Player.socket.send("Server.PlayerReady", null);
}

exports.RemovePlayer = async function(id) {
    const CachePlayer = UserList[id];
    if (CachePlayer === undefined) return;
    delete UserList[id];

    console.log(`[UserManager] ${CachePlayer.name}(${id})님이 서버를 나갔습니다.`);
    if (!CachePlayer.ready)
        console.log(`[UserManager_Warning] ${CachePlayer.name}(${id})님이 비정상적으로 종료되었습니다.`);
}