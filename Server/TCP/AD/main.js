const crystalUtil = require("../lib/crystalUtils.js");

TriggerEvent["ad.TryShow"] = function(id) {
    const player = UserList[id];
    if (player === undefined) return;

    const result = Math.floor(Number(new Date()) / 1000) - player.adShow;

    player.socket.send("ad.ResultTryShow", result >= 60 * 10);
}

TriggerEvent["ad.give"] = function(id) {
    const player = UserList[id];

    const result = Math.floor(Number(new Date()) / 1000) - player.adShow;
    if (result < 60 * 10) return;

    player.adShow = Math.floor(Number(new Date()) / 1000);
    crystalUtil.Add(id, 50);
}