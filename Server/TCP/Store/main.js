const crystalUtil = require("../lib/crystalUtils.js");
const onestore = require("./onestore.js");
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

        PlayerBuyHandler(id, result.productId);
    });
}

TriggerEvent["store.buy_onestore"] = async function(id, data) {
    const player = UserList[id];
    if (player === undefined || data.id === undefined || data.index === undefined || data.token === undefined) return;

    const result = await onestore.getPurchaseDetails(data.id, data.token);
    if (!player.socket.isConnect()) {
        console.log("onestore - 결제 중 오프라인 : "+ data.id + " / "+ id);
        return;
    }

    if (result === undefined) {
        console.log("onestore - 영수증 찾을 수 없음 : "+ data.id + " / "+ id);
        player.socket.send("store.complete", {
            ok: false,
            index: data.index
        });
        return;
    }

    if (result.consumptionState  === 1) {
        console.log("onestore - 이미 소비상태 : "+ data.id + " / "+ result.developerPayload +" / "+ id);
        return;
    }
    if (result.purchaseState === 1) {
        console.log("onestore - 구매 취소됨 : "+ data.id + " / "+ result.developerPayload +" / "+ id);
        return;
    }

    PlayerBuyHandler(id, data.id, data.index);
}

const GiveCrystal_List = {
    "crystal_990": 90,
    "crystal_4900": 520,
    "crystal_9900": 990,
    "crystal_19000": 2000,
    "crystal_29000": 3000,
    "crystal_49000": 5000,
}
function PlayerBuyHandler(id, productId, index) {
    const player = UserList[id];
    if (player === undefined) return;

    const value = GiveCrystal_List[productId];
    if (value === undefined) {
        console.log(`${id}이가 ${productId}를 구매했지만 설정된게 없음`);
        return;
    }

    console.log(`${id}이가 ${productId} 삼`);
    crystalUtil.Add(id, value);
    if (index === undefined)
        player.socket.send("store.complete", true);
    else
        player.socket.send("store.complete", {
            ok: true,
            index: index
        });
    player.socket.send("Lobby.Reload", null);
}