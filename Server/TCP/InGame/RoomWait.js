const RoomManager = require("./RoomManager.js");

RoomManager.RoomClass.prototype.WaitForPlayer = function() {
    this.waitData = {}; // 준비 다 되면 삭제됨

    this.players.forEach(playerID => {
        const player = UserList[playerID];
        this.waitData[playerID] = false;

        player.socket.send("ingame.load", null);
    });
}

TriggerEvent["ingame.ready"] = function(id) {
    const player = UserList[id];
    if (player === undefined) return;
    
    const room = RoomManager.getRoomToPlayer(id);
    if (room === undefined || room.waitData === undefined || room.waitData[id] !== false) return;

    room.waitData[id] = true; // 활성화
    
    for (const iterator of room.waitData) {
        if (iterator !== true) {
            return; // 아직 다 준비가 안됨.
        }
    }

    // 모두다 준비 됨
    delete room.waitData;
    room.GameStart();
}