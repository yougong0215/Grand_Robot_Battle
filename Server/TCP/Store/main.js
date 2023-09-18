const crystalUtil = require("../lib/crystalUtils.js");
const iap = require("iap");
const iap_token = require(__rootdir+"/Config/IAP_account.json");

TriggerEvent["store.buy"] = function(id, data) {
    const player = UserList[id];
    if (player === undefined) return;
    
    let purchaseToken;
    try {
        purchaseToken = JSON.parse(JSON.parse(data.payload).json).purchaseToken;
    } catch (err) {
        // console.log("store.buy data : "+data);
        return;
    }

    iap.verifyPayment("google", {
        receipt: purchaseToken,
        productId: data.id,
        packageName: "com.Isthisno.sdfd",
        subscription: false,
        keyObject: iap_token
    }, function(err, result) {
        if (err != undefined || result == undefined) {
            player.socket.send("store.complete", false);
            return;
        }

        if (!player.socket.isConnect()) {
            console.log("결제 중 오프라인 : "+ result.productId + " / "+ id);
            return;
        }

        player.socket.send("store.complete", true);
        PlayerBuyHandler(id, result.productId);
    });
}

const GiveCrystal_List = {
    "crystal_990": 90,
    "crystal_4900": 520,
    "crystal_9900": 990,
    "crystal_19000": 2000,
    "crystal_29000": 3000,
    "crystal_49000": 5000,
}
function PlayerBuyHandler(id, productId) {
    const player = UserList[id];
    if (player === undefined) return;

    const value = GiveCrystal_List[productId];
    if (value === undefined) {
        console.log(`${id}이가 ${productId}를 구매했지만 설정된게 없음`);
        return;
    }

    console.log(`${id}이가 ${productId} 삼`);
    crystalUtil.Add(id, value);
    player.socket.send("Lobby.Reload", null);
}