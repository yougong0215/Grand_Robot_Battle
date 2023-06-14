const equipmentUtils = require("../lib/equipmentUtils.js");
require("./Setting.js");

TriggerEvent["MakeRobot.GetSO"] = function(id) {
    const Player = UserList[id];
    if (Player === undefined || !Player.ready) return;

    const preset = Player.preset;
    for (const key in preset) {
        const element = preset[key];
        preset[key] = element || "";
    }

    Player.socket.send("MakeRobot.ResultSO", equipmentUtils.GetAllItem(id));
    Player.socket.send("MakeRobot.SendPreset", preset);
}