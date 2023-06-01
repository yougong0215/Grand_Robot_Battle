global.UserList = {};

class Player {
    name = undefined;
    socket = undefined;
    coin = 0;
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

    UserList[id] = new Player(UserData.name, socket);

    console.log(UserList);
}

exports.RemovePlayer = async function(id) {
    const CachePlayer = UserList[id];
    if (UserList[id] === undefined) return;

    delete UserList[id];

    console.log(CachePlayer.name, UserList);
}