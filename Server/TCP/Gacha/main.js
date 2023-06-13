const CrystalUtils = require("../lib/crystalUtils.js");
const equipmentUtils = require("../lib/equipmentUtils.js");
const ItemList = require("../../Config/Gacha_List.json");

function getRandomNumber(min, max) {
    min = Math.ceil(min); // 최소값 올림
    max = Math.floor(max); // 최대값 내림
    return Math.floor(Math.random() * (max - min + 1)) + min;
}
const RnadomItemCode = () => ItemList[getRandomNumber(0,ItemList.length - 1)];

TriggerEvent["Gacha.Start_1"] = function(id) {
    const Player = UserList[id];
    if (Player === undefined || !Player.ready) return;

    if (!CrystalUtils.TryPayment(id, 100)) {
        console.log(`[Gacha] ${Player.name}님이 1회 가챠뽑기 하다가 돈이 없어요.`);
        Player.socket.send("Gacha.error", "잼이 충분하지 않습니다.");
        return;
    }

    const RandomItem = RnadomItemCode();
    equipmentUtils.AddItem(id, RandomItem);

    Player.socket.send("Gacha.Result_1", RandomItem);
}

TriggerEvent["Gacha.Start_10"] = function(id) {
    const Player = UserList[id];
    if (Player === undefined || !Player.ready) return;

    if (!CrystalUtils.TryPayment(id, 100 * 10)) {
        console.log(`[Gacha] ${Player.name}님이 10회 가챠뽑기 하다가 돈이 없어요.`);
        Player.socket.send("Gacha.error", "잼이 충분하지 않습니다.");
        return;
    }

    const Items = [];
    for (let index = 0; index < 10; index++) {
        const RandomItem = RnadomItemCode();
        Items.push(RandomItem);
        equipmentUtils.AddItem(id, RandomItem);
    }

    console.log(UserList[id].inventory);

    Player.socket.send("Gacha.Result_10", Items);
}