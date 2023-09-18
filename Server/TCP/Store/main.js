const iap = require("iap");
const iap_token = require(__rootdir+"/Config/IAP_account.json");

TriggerEvent["store.test"] = function(id, data) {
    let purchaseToken;
    try {
        purchaseToken = JSON.parse(JSON.parse(data).json).purchaseToken;
    } catch (err) {
        console.log(JSON.parse(data));
        // console.log(err);
        return;
    }

    console.log(purchaseToken);

    iap.verifyPayment("google", {
        receipt: purchaseToken,
        productId: "domi_test2",
        packageName: "com.Isthisno.sdfd",
        subscription: false,
        keyObject: iap_token
    }, function(err, result) {
        console.log(err, result);
    });
}

// iap.verifyPayment("google", {
//     receipt: `djaacjkaieiglgifmfmjpibf.AO-J1OzrAJgQbmyUQiPWRnepXeAC62uTgMIRBkh3V9ZJFmTPLyVt_M4zTqD9s6Xgz0jTfp-lhOqlsjg846ueoQzy7pRMzBy7_w` /* purchase token */,
//     productId: "domi_test2" /* product id */,
//     packageName: "com.Isthisno.sdfd" /* package name */,
//     subscription: false/* 구독 상품인지 여부 */,
//     keyObject: iap_token // 구글 클라우드 IAP 서비스 계정 키
    
// }, function(err, result) {
//     console.log(err, result);
// });