global.UserList = {};

class PlayerForm {
    name = undefined;
    socket = undefined;
    coin = 0;
    crystal = 0;
    level = 0;
    exp = 0;

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
    if (UserData === undefined || UserData.name === undefined) {
        socket.kick("유저 정보를 불러올 수 없습니다.");
        sql.close();
        return;
    }

    const Player = UserList[id] = new PlayerForm(UserData.name, socket);

    // 플레이어 정보들을 불러오자
    const PlayerStats = await sql.Aget("SELECT * FROM stats WHERE id = ?", id);
    if (PlayerStats) { // 정보들이 있으면 (만약 없다면 다 0임)
        Player.coin = PlayerStats.coin;
        Player.crystal = PlayerStats.crystal;
        Player.level = PlayerStats.level;
        Player.exp = PlayerStats.exp;
    }

    // 로비로 바꾸라고 요청해야지
    Player.socket.send("Lobby.Init", {
        ID: id,
        Name: Player.name,
        Coin: Player.coin,
        Crystal: Player.crystal
    });
}

exports.RemovePlayer = async function(id) {
    const CachePlayer = UserList[id];
    if (CachePlayer === undefined) return;
    delete UserList[id];

    console.log(CachePlayer.name, UserList);
}