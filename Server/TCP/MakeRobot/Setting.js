TriggerEvent["MakeRobot.SetSetting"] = function(id, data) {
    const Player = UserList[id];
    if (Player === undefined || !Player.ready || typeof(data) !== "object") return;

    const { preset } = Player;

    preset.left = preset.right = preset.head = preset.body = preset.leg = null;

    if (data.Left)
        preset.left = data.Left;
    if (data.Right)
        preset.right = data.Right;
    if (data.Head)
        preset.head = data.Head;
    if (data.Body)
        preset.body = data.Body;
    if (data.Leg)
        preset.leg = data.Leg;
}