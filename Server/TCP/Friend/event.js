const friendUtil = require("./main.js");
const sql = require("../../utils/sqlite.js");

TriggerEvent["friend.getList"] = async function(id) {
    const friends = await friendUtil.getAllFollowers(id);
    const domiPlayer = UserList[id];
    let players;
    let requireCount = 0;

    players = friends.map(value => {
        const nplayer = UserList[value];
        if (nplayer === undefined) requireCount ++;
        return {
            id: value,
            name: nplayer?.name
        };
    });

    const db = sql.GetObject();

    const finishDB = function() {
        requireCount --;
        if (requireCount == 0) {
            domiPlayer.socket.send("friend.resultList", players);
            db.close();
        }
    }

    if (requireCount <= 0) {
        requireCount = 1;
        finishDB();
        return;
    }


    players.forEach(value => {
        if (value.name !== undefined) return;
        db.get("SELECT name FROM users WHERE id = ?", [value.id], function(err, row) {
            if (!err && row !== undefined) {
                value.name = row.name;
            }
            finishDB();
        });
    });
}