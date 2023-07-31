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

    let LevelSheet = {};
    let sheetIndex = 0;

    let finish_Count = 0;
    const FinishCheck = function() {
        finish_Count ++;
        if (doc.sheetsByIndex.length === finish_Count) exports.itemStats = LevelSheet;
    }

    doc.sheetsByIndex.forEach(async sheet => {
        const sheetID = ++sheetIndex;
        console.log("[GoogleSheet] " + sheet.title + "("+sheetID+") 시트 로드중...");
        await sheet.loadCells("A:E"); 
        console.log("[GoogleSheet] " + sheet.title + "("+sheetID+") 시트 데이터 읽는중...");
        const data = await sheet.getCellsInRange("A:E");
        LevelSheet[sheetID] = {};
        data.forEach(itemStats => {
            LevelSheet[sheetID][itemStats[0]] = {
                attack: Number(itemStats[1]),
                shield: Number(itemStats[2]),
                speed: Number(itemStats[3]),
                health: Number(itemStats[4]),
            };
        });
        FinishCheck();
    });
})();