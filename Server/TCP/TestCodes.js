console.log("[TEST] 테스트 코드 Load");

const CoinUtil = require("./lib/coinUtils.js");
const equipmentUtil = require("./lib/equipmentUtils.js");

TriggerEvent["test.coinAdd"] = function(id) {
    // const Player = UserList[id];
    
    console.log("-----TEST------");
    console.log(CoinUtil.Get(id));
    CoinUtil.Add(CoinUtil.Add(id, 10));
    console.log(CoinUtil.Get(id));

    console.log("-----------");
    console.log(CoinUtil.TryPayment(id, 50000));
    console.log(CoinUtil.Get(id));
}

TriggerEvent["test.equip"] = function(id) {
    console.log("-----TEST------");
    const WaterItemToken = equipmentUtil.AddItem(id, "water");
    console.log(WaterItemToken);
    console.log(equipmentUtil.GetItem(id,WaterItemToken));
    console.log(equipmentUtil.CheckItem(id, WaterItemToken));

    // console.log("-----------");
    // console.log(equipmentUtil.RemoveItem(id, WaterItemToken));
    // console.log(equipmentUtil.GetItem(id,WaterItemToken));

    const Player = UserList[id];
    const { inventory: { equipment: equip } } = Player;

    console.log("-----------");
    console.log(equip);
}