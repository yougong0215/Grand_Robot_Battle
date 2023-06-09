console.log("[TEST] 테스트 코드 Load");

const CoinUtil = require("./lib/coinUtils.js");
const equipmentUtil = require("./lib/equipmentUtils.js");

TriggerEvent["test.coinAdd"] = function(id) {
    // const Player = UserList[id];

    console.log(CoinUtil.Get(id));
    CoinUtil.Add(CoinUtil.Add(id, 10));
    console.log(CoinUtil.Get(id));
}