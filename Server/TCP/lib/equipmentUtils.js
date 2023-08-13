const RnadomString = function(itmelist) {
    const random = Math.random().toString(36).substring(2,11);
    if (itmelist[random] !== undefined)
        return RnadomString(); // 이미 있는 토큰이라면 다시 해

    return random;
}

exports.AddItem = function(PlayerID, itemcode, level, grade) {
    const Player = UserList[PlayerID];
    if (Player === undefined || !Player.ready) return;
    if (typeof(level) === "number" && level <= 0) {
        throw new Error("level 인수가 잘못되었습니다.");
    }
    if (typeof(grade) === "number" && grade < 0) {
        throw new Error("grade 인수가 잘못되었습니다.");
    }

    const { inventory: { equipment } } = Player;
    const ItemToken = RnadomString(equipment);

    equipment[ItemToken] = {
        code: itemcode,
        level: level || 1, // level이 없으면 기본값 1
        grade: grade || 0 // 0: 노멀, 1: 유니크, 2: 마스터피스
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

exports.GetAllItem = function(PlayerID) {
    const Player = UserList[PlayerID];
    if (Player === undefined || !Player.ready) return;

    const { inventory: { equipment } } = Player;
    
    return equipment;
}

exports.GetGradID = function(grade) {
    switch (grade) {
        case 0:
            return "n";
        case 1:
            return "u";
        case 2:
            return "m";
    
        default:
            return;
    }
}