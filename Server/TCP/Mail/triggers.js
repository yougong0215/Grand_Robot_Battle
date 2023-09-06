const mail = require("./index.js");
const itemManager = require("../lib/itemUtils.js");

TriggerEvent["mail.requestMails"] = async function(id, pageid) {
    const player = UserList[id];
    if (player === undefined) return;

    const result = await mail.GetMails(id, 50, pageid);
    if (!player.socket.isConnect()) return;

    player.socket.send("mail.resultMails", result);
}

TriggerEvent["mail.openItem"] = async function(id, mailID) {
    const player = UserList[id];
    if (player === undefined) return;

    const result = await mail.GetMail(mailID);
    if (result === undefined) return;

    if (result.user !== id) { // 머지 받은 사람이랑 보상 받는 사람이랑 다른데? (핵인가?)
        return;
    }

    if (!mail.ItemClear(mailID)) return; // 메일 아이템 비우기 실패

    JSON.parse(result.items).forEach(item => {
        itemManager.Give(id, item[0], Number(item[1]));
    });

    // 보상 받았다!!
}

// TriggerEvent["mail.requestContent"] = async function(id, mailID) {
//     mailID = Number(mailID);
    
//     const player = UserList[id];
//     if (player === undefined || mailID === NaN) return;

//     const mailInfo = await mail.GetContent(mailID);
//     player.socket.send("mail.resultContent", mailInfo || null);
// }