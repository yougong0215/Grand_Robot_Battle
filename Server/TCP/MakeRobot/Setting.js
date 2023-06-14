TriggerEvent["MakeRobot.SetSetting"] = function(id, data) {
    const Player = UserList[id];
    if (Player === undefined || !Player.ready) return;

    console.log(data);
}