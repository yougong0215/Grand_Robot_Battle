module.exports = null; // 초기값

const googleAPI = require(__rootdir+"/Config/GoogleAPI.json");
const { GoogleSpreadsheet } = require("google-spreadsheet");
const { JWT } = require("google-auth-library");
const serviceAccountAuth = new JWT({
    email: googleAPI.email,
    key: googleAPI.private_key,
    scopes: [
      'https://www.googleapis.com/auth/spreadsheets',
    ],
});

const doc = new GoogleSpreadsheet(googleAPI.sheetID, serviceAccountAuth);

(async function() {
    console.log("[GoogleSheet] 데이터 로드중...");
    await doc.loadInfo();
    console.log("[GoogleSheet] 로드 완료");

    let sheetIndex = 0;
    doc.sheetsByIndex.forEach(async sheet => {
        const sheetID = ++sheetIndex;
        console.log("[GoogleSheet] " + sheet.title + "("+sheetID+") 시트 로드중...");
        await sheet.loadCells("M:Q"); 
        console.log("[GoogleSheet] " + sheet.title + "("+sheetID+") 시트 데이터 읽는중...");
        const data = await sheet.getCellsInRange("M:Q");
        console.log(data);
    });
})();