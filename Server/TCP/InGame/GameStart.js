const RoomManager = require("./RoomManager.js");
const itemStatManager = require("../ItemStat");

RoomManager.RoomClass.prototype.GameStart = function() {
    this.ready = true;
    console.log("[RoomManager] 게임 시작! ("+ this.roomID +")");

    const playersWear = {};

    // 플레이어 스텟 셋팅
    Object.keys(this.players).forEach(playerID => {
        const player = UserList[playerID];
        playersWear[playerID] = {
            left: null,
            right: null,
            head: null,
            body: null,
            leg: null,
        }
        for (const [part, itemToken] of Object.entries(player.preset)) {
            if (itemToken === undefined) continue;
            const item = player.inventory.equipment[itemToken];
            if (item === undefined) continue;

            const itemStat = itemStatManager.itemStats[Number(item.level)]["n_" /* 임시?? */+item.code];
            if (itemStat === undefined) continue;

            playersWear[playerID][part] = item.code;

            // 스탯 추가
            this.players[playerID].attack += itemStat.attack;
            this.players[playerID].shield += itemStat.shield;
            this.players[playerID].speed += itemStat.speed;
            this.players[playerID].health += itemStat.health;
        }
    });

    Object.keys(this.players).forEach(playerID => {
        const player = UserList[playerID];
        const packets = [];
        
        for (const [id, data] of Object.entries(this.players)) {
            const wear = playersWear[id];
            packets.push({
                my: id === playerID,
                name: player.name,
                health: data.health,
                attack: data.attack,
                shield: data.shield,
                speed: data.speed,
                // 파츠
                ...wear
            });
        }

        console.log(packets);

        player.socket.send("ingame.playerInit", packets);
    });

}