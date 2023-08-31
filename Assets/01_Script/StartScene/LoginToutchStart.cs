using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// google
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Unity.Services.Authentication;
using Unity.Services.Core;

public class LoginToutchStart : MonoBehaviour
{
    [SerializeField] LoginSession _session;

    string SelectToken;
    bool _disable = false;
    bool isGoogle = false;

    private void Awake() {
        // 서버 리스너 연결
        NetworkCore.EventConnect += ConnectOK;
        NetworkCore.EventDisconnect += ConnectFailed;
        NetworkCore.EventListener["Server.PlayerReady"] = ServerReady;
        
        // 폰에서는 30프레임이 기본셋팅임
        #if UNITY_ANDROID
        Application.targetFrameRate = 60;
        #endif
    }

    public void Connect() {
        if (_disable) return;

        SelectToken = PlayerPrefs.GetString(LoginSession.SAVE_KEY);
        if (SelectToken.Length <= 0) return;

        _disable = true;
        isGoogle = false;

        if (SelectToken == "google") { // 구글 로그인
            SocialGoogle();
            return;
        }
        
        // 로딩 띄움
        LoginLoadingSystem.ShowUI("서버와 연결중 입니다.");
        
        // 서버 로그인!!!
        NetworkCore.instance.ServerConnect();
    }

    //////////////////// Google ////////////////////

    async void SocialGoogle() {
        LoginLoadingSystem.ShowUI("Google 정보를 불러오고 있습니다.");

        PlayGamesPlatform.Activate();
        await UnityServices.InitializeAsync();

        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                PlayGamesPlatform.Instance.RequestServerSideAccess(false, code =>
                {
                    isGoogle = true;
                    SelectToken = code;

                    LoginLoadingSystem.ShowUI("서버와 연결중 입니다.");
                    NetworkCore.instance.ServerConnect();
                });
            }
            else
            {
                LoginLoadingSystem.HideUI();
                LoginAlertWindow.ShowUI("Google 정보를 불러올 수 없습니다.", "Google 로그인을 할 수 없거나 필요한 권한이 없을 수 있습니다.");
                _disable = false;
            }
        });
    }
    public async void LoginGoogle() {
        LoginLoadingSystem.ShowUI("Google 정보를 불러오고 있습니다.");

        PlayGamesPlatform.Activate();
        await UnityServices.InitializeAsync();

        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                PlayerPrefs.SetString(LoginSession.SAVE_KEY, "google");
                _session.ChangeLayout(true);
            }
            else
            {
                LoginAlertWindow.ShowUI("Google 정보를 불러올 수 없습니다.", "Google 로그인을 할 수 없거나 필요한 권한이 없을 수 있습니다.");
            }
            LoginLoadingSystem.HideUI();
        });
    }

    //////////////////// 서버 리스너 ////////////////////
    void ConnectOK() {
        LoginLoadingSystem.ShowUI("계정정보를 불러오는 중입니다.");
        NetworkCore.Send(isGoogle ? "domiServer.LoginForGoogle" : "domiServer.Login", SelectToken);
    }
    void ConnectFailed(string why) {
        print("[LoginManager] 서버 로그인 실패 : "+why);
        LoginLoadingSystem.HideUI();

        // 서버에서 세션이 만료되었으니 토큰을 삭제하라는 요청함
        if (why == "domi.session_remove") {
            why = "세션이 만료되었습니다.\n로그아웃 후 다시 로그인해주세요.";
        } else if (why == "TimeOut") {
            why = "연결시간이 초과되었습니다.";
        }

        LoginAlertWindow.ShowUI("서버 연결에 실패하였습니다.", why);

        // 처리 할거....
        _disable = false;
    }
    void ServerReady(LitJson.JsonData data) {
        print("[LoginManager] 계정정보 불러오기 성공! - 로비로 이동");
        SceneManager.LoadScene("Menu");
    }

}
