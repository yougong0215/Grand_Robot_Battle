const equipmentUtils = require("../lib/equipmentUtils.js");
require("./Setting.js");

TriggerEvent["MakeRobot.GetSO"] = function(id) {
    const Player = UserList[id];
    if (Player === undefined || !Player.ready) return;

    const preset = Player.preset;
    Player.socket.send("MakeRobot.ResultSO", equipmentUtils.GetAllItem(id));
    Player.socket.send("MakeRobot.SendPreset", {
        left: preset.left || "",
        right: preset.right || "",
        head: preset.head || "",
        body: preset.body || "",
        leg: preset.leg || ""
    });
}