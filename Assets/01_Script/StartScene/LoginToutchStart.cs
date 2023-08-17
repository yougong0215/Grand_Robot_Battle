using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginToutchStart : MonoBehaviour
{
    string SelectToken;
    bool _disable = false;

    private void Awake() {
        // 서버 리스너 연결
        NetworkCore.EventConnect += ConnectOK;
        NetworkCore.EventDisconnect += ConnectFailed;
        NetworkCore.EventListener["Server.PlayerReady"] = ServerReady;
    }


    void Update()
    {
        if (Input.anyKeyDown && !_disable) {
            SelectToken = PlayerPrefs.GetString(LoginSession.SAVE_KEY);
            if (SelectToken.Length <= 0) return;

            _disable = true;
            
            // 로딩 띄움
            LoginLoadingSystem.ShowUI("서버와 연결중 입니다.");
            
            // 서버 로그인!!!
            NetworkCore.instance.ServerConnect();

        }
    }

        //////////////////// 서버 리스너 ////////////////////
    void ConnectOK() {
        LoginLoadingSystem.ShowUI("계정정보를 불러오는 중입니다.");
        NetworkCore.Send("domiServer.Login", SelectToken);
    }
    void ConnectFailed(string why) {
        print("[LoginManager] 서버 로그인 실패 : "+why);
        LoginLoadingSystem.HideUI();

        // 서버에서 세션이 만료되었으니 토큰을 삭제하라는 요청함
        if (why == "domi.session_remove") {
            why = "세션이 만료되었습니다.";
        }

        // 처리 할거....
        _disable = false;
    }
    void ServerReady(LitJson.JsonData data) {
        print("[LoginManager] 계정정보 불러오기 성공! - 로비로 이동");
        SceneManager.LoadScene("Menu");
    }

}
