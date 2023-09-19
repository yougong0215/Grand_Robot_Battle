const aixos = require("axios");

const END_POINT = "https://sbpp.onestore.co.kr/";
const packageName = "com.Isthisno.sdfd";
const client_secret = "7MSMtdh5PANQZ58wM+yeaNIEwzHsVovYP3HSLXLGd6A=";

let tokenData;

exports.getPurchaseDetails = async function(productId, purchaseToken) {
    if (tokenData === undefined) {
        let result = await CreateToken();
        if (result == false) return;
    }

    let response;
    try {
        response = await aixos({
            method: "GET",
            url: END_POINT+`v7/apps/${packageName}/purchases/inapp/products/${productId}/${purchaseToken}`,
            // params: {
            //     client_id: "com.Isthisno.sdfd",
            //     client_secret: client_secret,
            //     grant_type: "client_credentials"
            // },
            headers: {
                "Authorization": "Bearer "+tokenData.access_token,
                "Content-Type": "application/json",
                "x-market-code": "MKT_ONE"
            }
        });
    } catch (err) {
        if (err.response.status == 401) {
            await CreateToken();
            return await exports.getPurchaseDetails(productId, purchaseToken);
        }

        console.log(`[onestore] 조회 불가 (${err.response.status})`);
        return;
    }

    return response.data;
}

async function CreateToken() {
    let response;
    try {
        response = await aixos({
            method: "POST",
            url: END_POINT+"v7/oauth/token",
            params: {
                client_id: "com.Isthisno.sdfd",
                client_secret: client_secret,
                grant_type: "client_credentials"
            },
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "x-market-code": "MKT_ONE"
            }
        });
    } catch (err) {
        console.log(`[onestore] 토큰 발급불가 (${err.response.status})`);
        return false;
    }

    tokenData = response.data;
    return true;
}