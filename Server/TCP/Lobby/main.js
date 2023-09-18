TriggerEvent["Lobby.RequestInfo"] = function(id) {
    const Player = UserList[id];
    if (Player === undefined || !Player.ready) return;

    Player.socket.send("Lobby.ResultInfo", {
        ID: id,
        Name: Player.name,
        Coin: Player.coin,
        Crystal: Player.crystal,
        Prefix: Player.profile.prefix || "Unknown", // 임시로 칭호가 설정되지 않은경우
        AvatarURL: Player.avatarURL,
        ADtime: Math.max((60 * 10) - (Math.floor(Number(new Date()) / 1000) - Player.adShow), -1)
    });
}