const mail = require("./index.js");

TriggerEvent["mail.requestMails"] = async function(id, pageid) {
    const player = UserList[id];
    if (player === undefined) return;

    const result = await mail.GetMails(id, 50, pageid);
    if (!player.socket.isConnect()) return;

    player.socket.send("mail.resultMails", result);
}