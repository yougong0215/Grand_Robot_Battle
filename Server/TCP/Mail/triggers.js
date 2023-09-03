const mail = require("./index.js");

TriggerEvent["mail.requestMails"] = async function(id, pageid) {
    const player = UserList[id];
    if (player === undefined) return;

    const result = await mail.GetMails(id, 50, pageid);
    if (!player.socket.isConnect()) return;

    player.socket.send("mail.resultMails", result);
}

// TriggerEvent["mail.requestContent"] = async function(id, mailID) {
//     mailID = Number(mailID);
    
//     const player = UserList[id];
//     if (player === undefined || mailID === NaN) return;

//     const mailInfo = await mail.GetContent(mailID);
//     player.socket.send("mail.resultContent", mailInfo || null);
// }