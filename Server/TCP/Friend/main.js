const sql = require("../../utils/sqlite.js");

// 자신이 팔로우 한거
exports.getAllFollowers = async id => await handler(false, id);

// 다른사람이 자신을 팔로우 한거
exports.getAllMyFollows = async id => await handler(true, id);

async function handler(reverse, id) {
    const db = sql.GetObject();
    const result = await db.Aall(`SELECT ${reverse ? "id" : "target"} FROM friends WHERE ${reverse ? "target" : "id"} = ?`, [id]);
    db.close();

    // error!
    if (result === false) return false;

    return result.map(domi => domi[reverse ? "id" : "target"]);
};

// 내가 target을 팔로우 중인가?
exports.isFollwer = async function(id, target) {
    const db = sql.GetObject();
    const result = await db.Aget("SELECT EXISTS (SELECT id FROM friends WHERE id = ? AND target = ?) as ok;", [ id, target ]);
    db.close();

    if (result === false) return false;
    return result.ok !== 0;
};

// target이 나를 팔로우 중인가? (이건 좀 이상한듯)
// exports.isFollow = async (id, target) => await exports.isFollwer(target, id);