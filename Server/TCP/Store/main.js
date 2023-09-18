const iap = require("iap");
const iap_token = require(__rootdir+"/Config/IAP_account.json");

TriggerEvent["store.test"] = function(id, data) {
    const player = UserList[id];
    if (player === undefined) return;
    
    let purchaseToken;
    try {
        purchaseToken = JSON.parse(JSON.parse(data).json).purchaseToken;
    } catch (err) {
        return;
    }

    iap.verifyPayment("google", {
        receipt: purchaseToken,
        productId: "domi_test2",
        packageName: "com.Isthisno.sdfd",
        subscription: false,
        keyObject: iap_token
    }, function(err, result) {
        if (err != undefined || result == undefined) {
            player.socket.send("store.complete", false);
            return;
        }

        console.log(err, result);
        player.socket.send("store.complete", true);
    });

    
}