const RoomManager = require("./RoomManager.js");
const equipmentUtils = require("../lib/equipmentUtils.js");
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

            const itemStat = itemStatManager.itemStats[Number(item.level)][equipmentUtils.GetGradID(item.grade) + "_" + item.code];
            if (itemStat === undefined) continue;

            playersWear[playerID][part] = item.code;

            // 스탯 추가
            this.players[playerID].attack += itemStat.attack;
            this.players[playerID].shield += itemStat.shield;
            this.players[playerID].speed += itemStat.speed;
            this.players[playerID].health += itemStat.health;

            this.players[playerID].parts[part] = {
                id: item.code,
                ...itemStat
            }
        }
    });

    Object.keys(this.players).forEach(playerID => {
        const player = UserList[playerID];
        const packets = [];
        
        for (const [id, data] of Object.entries(this.players)) {
            const wear = playersWear[id];
            packets.push({
                my: id === playerID,
                name: UserList[id].name,
                health: data.health,
                attack: data.attack,
                shield: data.shield,
                speed: data.speed,
                // 파츠 쿨타임
                cools: {
                    left: data.parts.left?.cooltime || 0,
                    right: data.parts.right?.cooltime || 0,
                    head: data.parts.head?.cooltime || 0,
                    body: data.parts.body?.cooltime || 0,
                    leg: data.parts.leg?.cooltime || 0
                },
                // 파츠
                ...wear
            });
        }

        player.socket.send("ingame.playerInit", packets);
    });

    this.SkillChoice();
}