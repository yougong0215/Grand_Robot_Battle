const itemStatManager = require("../ItemStat");

TriggerEvent["garage.getItems"] = function(id) {
    const Player = UserList[id];
    if (Player === undefined || !Player.ready) return;

    const MaxLevel = Object.keys(itemStatManager.itemStats).length;

    let items = [];
    for (const [token, data] of Object.entries(Player.inventory.equipment)) {
        const stat_levelsheet = itemStatManager.itemStats[data.level];
        if (stat_levelsheet === undefined) continue;
        const stat = stat_levelsheet["n_" /* 임시?? */+data.code];

        items.push({
            token: token,
            code: data.code,
            level: data.level,
            maxLevel: MaxLevel,
            attack: stat.attack,
            shield: stat.shield,
            speed: stat.speed,
            health: stat.health
        });
    }

    Player.socket.send("garage.resultItems", items);
}