using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

class LoginPacketForm {
    public string ID;
    public string password;

    public LoginPacketForm(string _ID, string _pass) {
        ID = _ID;
        password = _pass;
    }
}

public class LoginWindowSystem : MonoBehaviour
{
        // 로그인 창
    [Header("로그인 창")]
    [SerializeField] RectTransform LoginWindow;
    [SerializeField] TextMeshProUGUI ErrorText;
    [SerializeField] TMP_InputField ID_Input;
    [SerializeField] TMP_InputField Pass_Input;

    public void OpenLoginUI(bool close) {
        var _CanvasGroup = LoginWindow.GetComponent<CanvasGroup>();
        if (close) {
            _CanvasGroup.DOFade(0, .2f).OnComplete(() => LoginWindow.gameObject.SetActive(false));
            return;
        }

        // 오류 텍스트 초기화
        ErrorText.text = "";

        // input 초기화
        ID_Input.text = "";
        Pass_Input.text = "";

        LoginWindow.gameObject.SetActive(true);
        _CanvasGroup.DOFade(1, 0.2f);
    }

    public void LoginButtonDown() {
        // 오류 텍스트 초기화
        ErrorText.text = "";

        // 입력값 처리
        string ErrorValue = "";
        if (ID_Input.text.Length == 0)
            ErrorValue += "아이디";
        if (Pass_Input.text.Length == 0)
            ErrorValue += (ErrorValue.Length > 0 ? "와 " : "") + "비밀번호";
        if (ErrorValue.Length > 0) {
            ErrorText.text = ErrorValue + "를 입력해야합니다.";
            return; // 먼가 있구나
        }

        // 이미 있으면 그냥 로그인해
        if (LoginManager.instance.FindID_Login(ID_Input.text)) return;

        // 로딩 표시할 코드 넣을껑미


        HTTP_manager.RequestPOST("login", new LoginPacketForm(ID_Input.text, Pass_Input.text), HTTPLoginResult);
    }

    void HTTPLoginResult(int statusCode, LitJson.JsonData data) {
        if (statusCode != 200) {
            ErrorText.text = (statusCode == 0 ? "네트워크 상태를 확인하세요." : "로그인에 실패하였습니다. ("+statusCode+")");
            return;
        }

        if (!(bool)data["success"]) {
            ErrorText.text = (string)data["why"];
            return;
        }

        LoginManager.instance.SaveAccount((string)data["name"], (string)data["id"], (string)data["token"]);
    }
}