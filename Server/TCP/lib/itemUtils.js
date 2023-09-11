exports.Give = function(id, code, amount) {
    const player = UserList[id];
    if (player === undefined) return false;

    const { inventory: { item } } = player;

    const nowAmount = item[code] || 0;
    item[code] = nowAmount + amount;

    return true;
}

exports.Try = function(id, code, amount) {
    const player = UserList[id];
    if (player === undefined) return false;
    
    const { inventory: { item } } = player;
    if (item[code] === undefined || (item[code] - amount) < 0) return false;
    
    item[code] -= amount;
    if (item[code] === 0)
        delete item[code];

    return true;
}

exports.GetAmount = function(id, code) {
    const player = UserList[id];
    if (player === undefined) return 0;

    const { inventory: { item } } = player;
    
    return item[code] || 0;
}