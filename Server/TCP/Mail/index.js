const sql = require(__rootdir+"/utils/sqlite.js");

exports.GetMails = async function(user, MaxAmount, page) {
    const db = sql.GetObject();

    const startVal = MaxAmount * page;
    const endVal = startVal + MaxAmount;

    const result = await db.Aall("SELECT * FROM mails WHERE user = ? ORDER BY time DESC LIMIT ?,?", [ user, startVal, endVal ]);
    db.close(); // 꼭 닫자

    return result;
}

// exports.GetContent = async function(id) {
//     id = Number(id);
    
//     const db = sql.GetObject();
//     const result = await db.Aget("SELECT title, user, content, items, sender, time FROM mails WHERE id = ?", id);
//     db.close();

//     return result;
// }

exports.AddMail = async function(user, title, content, items, sender) {
    const db = sql.GetObject();
    const result = await db.Arun("INSERT INTO mails(user, title, content, items, sender, time) VALUES(?, ?, ?, ?, ?, ?)", [
        user,
        title,
        content,
        JSON.stringify(items),
        sender,
        Number(new Date())
    ]);

    db.close();
    return result;
}

exports.RemoveMail = async function(id) {
    const db = sql.GetObject();
    const result = await db.Arun("DELETE FROM mails WHERE id = ?", Number(id));
    db.close();

    return result;
}