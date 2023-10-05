TriggerEvent["Lobby.RequestInfo"] = function(id) {
    const Player = UserList[id];
    if (Player === undefined || !Player.ready) return;

    Player.socket.send("Lobby.ResultInfo", {
        ID: id,
        Name: Player.name,
        Coin: Player.coin,
        Crystal: Player.crystal,
        Prefix: Player.profile.prefix || "Unknown", // 임시로 칭호가 설정되지 않은경우
        AvatarURL: Player.avatarURL,
        ADtime: Math.max((60 * 10) - (Math.floor(Number(new Date()) / 1000) - Player.adShow), -1)
    });
}


const mysql = require("../../utils/sqlite.js");
// 계정 삭제
TriggerEvent["account.remove"] = function(id, input) {
    const player = UserList[id];
    if (player === undefined || input !== "삭제") return;

    player.ready = false;// 이제 데베 저장 안해도 됨
    player.socket.end();

    const sql = mysql.GetObject();

    const tables = [
        ["inventory", "id"],
        ["mails", "user"],
        ["preset", "id"],
        ["sessions", "id"],
        ["stats", "id"],
        ["users", "id"],
    ]
    tables.forEach(element => {
        sql.run(`DELETE FROM ${element[0]} WHERE ${element[1]} = ?`, [ id ], (err) => {});
    });
}