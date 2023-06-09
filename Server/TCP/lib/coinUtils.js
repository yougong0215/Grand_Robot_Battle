exports.Get = function(PlayerID) {
    const Player = UserList[PlayerID];
    if (Player === undefined || !Player.ready) return;
    
    return Player.coin;
}

exports.Set = function(PlayerID, value) {
    const Player = UserList[PlayerID];
    if (typeof(value) !== "number" || Player === undefined || !Player.ready) return;
    
    Player.coin = Math.floor(value);
}

exports.Add = function(PlayerID, value) {
    const Player = UserList[PlayerID];
    if (typeof(value) !== "number" || Player === undefined || !Player.ready) return;
    
    Player.coin += Math.floor(value);
}

exports.TryPayment = function(PlayerID, value) {
    const Player = UserList[PlayerID];
    value = Math.floor(value);
    if (typeof(value) !== "number" || value <= 0 || Player === undefined || !Player.ready) return false;

    if ((Player.coin - value) < 0) return false; // 돈이 충분하지 않음
    
    Player.coin -= value;
    return true;
}