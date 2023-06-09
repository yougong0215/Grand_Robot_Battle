const RnadomString = function(itmelist) {
    const random = Math.random().toString(36).substring(2,11);
    if (itmelist[random] !== undefined)
        return RnadomString(); // 이미 있는 토큰이라면 다시 해

    return random;
}

exports.AddItem = function(PlayerID, itemcode, level) {
    const Player = UserList[PlayerID];
    if (Player === undefined || !Player.ready) return;

    const { inventory: { equipment } } = Player;
    const ItemToken = RnadomString(equipment);

    equipment[ItemToken] = {
        code: itemcode,
        level: level || 0 // level이 없으면 기본값 0
    }

    return ItemToken;
}

exports.RemoveItem = function(PlayerID, token) {
    const Player = UserList[PlayerID];
    if (Player === undefined || !Player.ready) return;

    const { inventory: { equipment } } = Player;
    delete equipment[token];
}

exports.CheckItem = function(PlayerID, token) {
    const Player = UserList[PlayerID];
    if (Player === undefined || !Player.ready) return;

    const { inventory: { equipment } } = Player;
    return equipment[token] !== undefined;
}

exports.GetItem = function(PlayerID, token) {
    const Player = UserList[PlayerID];
    if (Player === undefined || !Player.ready) return;

    const { inventory: { equipment } } = Player;
    return equipment[token];
}