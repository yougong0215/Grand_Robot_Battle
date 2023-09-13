const RoomManager = require("./RoomManager.js");

RoomManager.RoomClass.prototype.WaitForPlayer = function() {
    this.waitData = {}; // 준비 다 되면 삭제됨

    Object.keys(this.players).forEach(playerID => {
        const player = UserList[playerID];
        this.waitData[playerID] = false;

        player.socket.send("ingame.load", null);
    });
}

TriggerEvent["ingame.ready"] = function(id, ai) {
    const player = UserList[id];
    if (player === undefined) return;

    if (ai) {
        GetPartsWithAI(id);
        return;
    }
    
    const room = RoomManager.getRoomToPlayer(id);
    if (room === undefined || room.waitData === undefined || room.waitData[id] !== false) return;

    room.waitData[id] = true; // 활성화
    
    for (const [nplayerID, ready] of Object.entries(room.waitData)) {
        if (ready !== true) {
            return; // 아직 다 준비가 안됨.
        }
    }

    // 모두다 준비 됨
    delete room.waitData;
    room.GameStart();
}

const equipmentUtils = require("../lib/equipmentUtils.js");
const itemStatManager = require("../ItemStat");
function GetPartsWithAI(id) {
    const player = UserList[id];
    if (player === undefined) return;

    const playerWear = {};
    for (const [part, itemToken] of Object.entries(player.preset)) {
        if (itemToken === undefined) continue;
        const item = player.inventory.equipment[itemToken];
        if (item === undefined) continue;

        const itemStat = itemStatManager.itemStats[Number(item.level)][equipmentUtils.GetGradID(item.grade) + "_" + item.code];
        if (itemStat === undefined) continue;

        playerWear[part] = {
            id: item.code,
            ...itemStat
        }
    }

    player.socket.send("ingame.AIinit", {
        name: player.name,
        ...playerWear
    });
}