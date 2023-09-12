const itemUtil = require("../lib/itemUtils.js");
const equipmentUtil = require("../lib/equipmentUtils.js");

TriggerEvent["puzzel.getList"] = function(id) {
    const player = UserList[id];
    if (player === undefined) return;

    const items = player.inventory.item;
    let packet = [];

    for (const [itemID, amount] of Object.entries(items)) {
        if (!endsWithPuzzel(itemID)) continue;

        const partID = removePuzzel(itemID);
        
        packet.push({
            id: partID,
            amount: amount
        });
    }

    player.socket.send("puzzel.resultList", packet);
}

TriggerEvent["puzzel.createParts"] = function(id, data) {
    const player = UserList[id];
    if (player === undefined || data === undefined || data.part === undefined || typeof(data.grade) !== "number") return;

    let TryValue;
    switch (data.grade) {
        case 0:
            TryValue = 20;
            break;
        case 1:
            TryValue = 50;
            break;
        case 2:
            TryValue = 100;
            break;
        default:
            return;
    }

    if (!itemUtil.Try(id, data.part+"_puzzel", TryValue)) {
        // 업글할 퍼즐이 부족합니다 ㄱㄱ
        console.log(`[puzzel] ${player.name}(${id})님이 파츠 교환에 실패했어요.`);
        return;
    }

    // 파츠 줘
    equipmentUtil.AddItem(id, data.part, 0, data.grade);
    console.log(`[puzzel] ${player.name}(${id})님이 파츠를 교환하였어요. ${data.part} / ${data.grade}등금`);
}

// 끝에 _puzzel 이 있는지 확인함
function endsWithPuzzel(str) {
    return /_puzzel$/.test(str);
}

// _puzzel 내용 삭제
function removePuzzel(str) {
    return str.replace(/_puzzel/g, '');
}