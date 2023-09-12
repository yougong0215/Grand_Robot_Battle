const googleAPI = require(__rootdir+"/Config/GoogleAPI.json");
const axios = require("axios");
const sqlite = require(__rootdir+"/utils/sqlite.js");
const { OAuth2Client } = require("google-auth-library");
const oauth = new OAuth2Client(googleAPI.OAuth2.client_id, googleAPI.OAuth2.client_secret);

module.exports = function(IdToken) {
    return new Promise((ok,fail) => loginHandler((id, avatar) => ok({status: true, id:id, avatar:avatar}), (why) => ok({status: false, why: why}), IdToken));
}

function loginHandler(reslove, reject, idToken) {
    oauth.getToken(idToken, function(err, tokenInfo, res) {
        if (err) {
            reject("Google 계정을 인증할 수 없습니다.");
            return;
        }

        axios({
            method: 'get',
            url: "https://www.googleapis.com/games/v1/players/me",
            params: { access_token: tokenInfo.access_token },
            responseType: 'json'
        }).then(async function(response) {
            const data = response.data;

            if (data.error) {
                reject("Google Play Games 계정을 불러올 수 없습니다. ("+data.error.code+")");
                return;
            }

            if (data.playerId === undefined || data.displayName === undefined) {
                reject("Google Play Games 요청이 잘못되었습니다.");
                return;
            }

            const sql = sqlite.GetObject();
            const result = await sql.Aget("SELECT id FROM users WHERE id = ?;", [ data.playerId ]);
            if (result === false) {
                reject("DB에 오류가 발생하여 로그인을 할 수 없습니다. (3)");
                sql.close();
                return;
            }
            if (result === undefined) { // 유저 정보 없음 추가 ㄱㄱ (강제 회원가입)
                if (!await sql.Arun("INSERT INTO users VALUES(?,?,?);", [data.playerId, data.displayName, "domi:Google"])) {
                    reject("DB에 오류가 발생하여 로그인을 할 수 없습니다. (4)");
                    return;
                }
            }

            sql.close();
            reslove(data.playerId, data.avatarImageUrl);
        }).catch(function(error) {
            reject("Google Play Games 계정을 불러올 수 없습니다.");
        });
    });
}