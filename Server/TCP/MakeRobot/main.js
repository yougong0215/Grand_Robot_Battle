const equipmentUtils = require("../lib/equipmentUtils.js");
require("./Setting.js");

TriggerEvent["MakeRobot.GetSO"] = function(id) {
    const Player = UserList[id];
    if (Player === undefined || !Player.ready) return;

    Player.socket.send("MakeRobot.ResultSO", equipmentUtils.GetAllItem(id));
}