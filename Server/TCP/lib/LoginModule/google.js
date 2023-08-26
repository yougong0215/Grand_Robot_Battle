const googleAPI = require(__rootdir+"/Config/GoogleAPI.json");
const { OAuth2Client } = require("google-auth-library");
const oauth = new OAuth2Client(googleAPI.OAuth2.client_id, googleAPI.OAuth2.client_secret);
// oauth.getToken("4/0Adeu5BUicumSQzuxuvHu0mvfGPC3x71GvEShXhj_vDJpa6kvMuVKTWLM1FpmoVNF4M-5sQ", function(err, token, res) {
//     console.log(err, token);
// });

module.exports = function(IdToken) {
    return new Promise((ok,fail) => loginHandler((id) => ok({status: true, id:id}), (why) => ok({status: false, why: why}), IdToken));
}

function loginHandler(reslove, reject, idToken) {
    oauth.getToken(idToken, function(err, token, res) {
        console.log(err, token);
    });
}