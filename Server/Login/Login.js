const Encrypt = require("../utils/Encrypt.js");
const sqlUtil = require("../utils/sqlite.js");

module.exports = async function(req, res) {
    
    let body;
    try {
        body = JSON.parse(req.body.domi);
    } catch {
        return;
    }
    
    const ID = body.ID;
    const password = body.password;

    console.log(body);

    if (typeof(ID) !== "string" || typeof(password) !== "string") {
        res.sendStatus(400);
        return;   
    }

    const sql = sqlUtil.GetObject();
    const Account = await sql.Aget("SELECT * FROM users WHERE id = ?", ID);

    // 계정이 없음
    if (Account === undefined) {
        res.json({
            success: false,
            why: "아이디, 비밀번호를 확인하세요.",
        });
        return;
    }
    
    const [salt, passHash] = Account.password.split(":");
    if (salt === undefined || passHash === undefined) {
        res.json({
            success: false,
            why: "무결성 검사 실패 (1)",
        });
        return;
    }

    // 비번이 일치하지 않음
    if (!await Encrypt.verifyPassword(password, salt, passHash)) {
        res.json({
            success: false,
            why: "아이디, 비밀번호를 확인하세요.",
        });
        return;
    }
    
    const Session_Token = Encrypt.randomString(30);
    const result = await sql.Arun("INSERT INTO sessions(id, token, Create_At) VALUES(?,?,?)", [Account.id, Session_Token, Number(new Date())]);
    if (!result) {
        res.json({
            success: false,
            why: "SQL 오류",
        });
        return;
    }

    // sql 소켓 닫자
    sql.close();
    
    // 끗
    res.json({
        success: true,
        token: Session_Token,
        name: Account.name
    });
}