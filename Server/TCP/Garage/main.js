const itemStatManager = require("../ItemStat");
const equipment = require("../lib/equipmentUtils.js");

TriggerEvent["garage.getItems"] = function(id) {
    const Player = UserList[id];
    if (Player === undefined || !Player.ready) return;

    const MaxLevel = Object.keys(itemStatManager.itemStats).length;

    let items = [];
    for (const [token, data] of Object.entries(Player.inventory.equipment)) {
        const stat_levelsheet = itemStatManager.itemStats[data.level];
        if (stat_levelsheet === undefined) continue;
        const stat = stat_levelsheet[equipment.GetGradID(data.grade) + "_" + data.code];

        items.push({
            token: token,
            code: data.code,
            level: data.level,
            maxLevel: MaxLevel,
            attack: stat.attack,
            shield: stat.shield,
            speed: stat.speed,
            health: stat.health,
            grade: data.grade
        });
    }

    Player.socket.send("garage.resultItems", items);
}