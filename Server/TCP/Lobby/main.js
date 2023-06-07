TriggerEvent["Lobby.RequestInfo"] = function(id) {
    const Player = UserList[id];
    if (Player === undefined) return;

    Player.socket.send("Lobby.ResultInfo", {
        ID: id,
        Name: Player.name,
        Coin: Player.coin,
        Crystal: Player.crystal
    });
}