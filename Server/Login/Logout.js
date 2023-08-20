const sqlUtil = require("../utils/sqlite.js");

module.exports = function(req, res) {
    if (req.body === undefined || typeof(req.body.domi) !== "string") return;
    
    let data;
    try {
        data = JSON.parse(req.body.domi);
    } catch {
        res.sendStatus(400);
        return;
    }

    if (typeof(data.token) !== "string") { // 머임 왜 토큰 없음
        res.sendStatus(400);
        return;
    }

    const sql = sqlUtil.GetObject();
    sql.run("delete from sessions where token = ?", data.token, function(err) {
        if (err)
            console.error(err);
    });
    
    sql.close();
    res.send("OK!");
}