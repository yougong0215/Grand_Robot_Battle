using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

class AccountForm {
    public string name;
    public string id;
    public string token;

    // public AccountForm(string _name, string _id, string _token) {
    //     name = _name;
    //     id = _id;
    //     token = _token;
    // }
}

public class LoginManager : MonoBehaviour
{
    [Header("계정 관련")]
    [SerializeField] RectTransform Account_List;
    [SerializeField] GameObject Account_Button;

    public static LoginManager instance;

    // 선택한 토큰
    string SelectToken = null;

    private void Awake() {
        if (instance == null)
            instance = this;

        // 서버 리스너 연결
        NetworkCore.EventConnect += ConnectOK;
        NetworkCore.EventDisconnect += ConnectFailed;

        for (int i = 0; i < Account_List.childCount; i++)
            Destroy(Account_List.GetChild(i).gameObject);

        var Accounts = GetSaveAccount();

        foreach (var Account in Accounts)
            ButtonAdd(Account.name, Account.id, Account.token);
    }

    private void OnDestroy() {
        SelectToken = null;
        NetworkCore.EventConnect -= ConnectOK;
        NetworkCore.EventDisconnect -= ConnectFailed;
    }

    public void ButtonAdd(string name, string id, string token) {
        GameObject domiButton = Instantiate(Account_Button, Vector3.zero, Quaternion.identity, Account_List);
        domiButton.GetComponent<Button>().onClick.AddListener(() => TryLogin(token));
        
        domiButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        domiButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = id;
    }
    public void SaveAccount(string name, string id, string token) {
        var Accounts = GetSaveAccount();

        AccountForm Account = new AccountForm();
        Account.name = name;
        Account.id = id;
        Account.token = token;

        // 계정 추가
        Accounts.Add(Account);

        // 계정 저장
        PlayerPrefs.SetString("Accounts", LitJson.JsonMapper.ToJson(Accounts));

        // 버튼 추가
        ButtonAdd(name, id, token);
    }
    public void RemoveAccount(int index) {
        var Accounts = GetSaveAccount();

        // 인덱스 범위 벗어남!!
        if (Accounts.Count <= index) return;

        // 계정 삭제
        Accounts.RemoveAt(index);
        
        // 계정 저장
        PlayerPrefs.SetString("Accounts", LitJson.JsonMapper.ToJson(Accounts));

        // 버튼 인덱스 범위 벗어남
        if (Account_List.childCount <= index) return;

        // 버튼 삭제
        Destroy(Account_List.GetChild(index).gameObject);
    }

    // 저장된 계정들 불러옴
    List<AccountForm> GetSaveAccount() {
        return LitJson.JsonMapper.ToObject<List<AccountForm>>(PlayerPrefs.GetString("Accounts", "[]"));
    }

    // 아이디가 있는지 찾고 있으면 로그인
    public bool FindID_Login(string id) {
        var Accounts = GetSaveAccount();
        for (int i = 0; i < Accounts.Count; i++)
        {
            if (Accounts[i].id == id && Account_List.childCount > i) {
                Account_List.GetChild(i).GetComponent<Button>().onClick.Invoke();
                return true;
            }
        }

        return false;
    }

    public void TryLogin(string token) {
        print("로그인! : "+token);

        SelectToken = token;

        // 로딩 띄움
        LoginLoadingSystem.ShowUI("서버와 연결중 입니다.");
        
        // 서버 로그인!!!
        NetworkCore.instance.ServerConnect();
    }

    ///////////////////////////////// 자동 로그인 /////////////////////////////////
    private void Start() {
        var Accounts = GetSaveAccount();
        if (Accounts.Count == 1) { // 만약 계정이 하나밖에 추가가 되어있지 않음.
            LoginLoadingSystem.ShowUI("로그인 초기화하고 있습니다.");
            StartCoroutine(AutoLoginWait());
        }
    }

    IEnumerator AutoLoginWait() {
        // 버튼이 1개가 될때까지 기다림..
        yield return new WaitUntil(() => Account_List.childCount == 1);

        // 이제 로그인 하자! (자동으로 버튼 눌리게 함)
        Account_List.GetChild(0).GetComponent<Button>().onClick.Invoke();
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

            // 토큰 삭제
            var Accounts = GetSaveAccount();
            for (int index = 0; index < Accounts.Count; index++)
                RemoveAccount(index);
        }

        // 처리 할거....
    }
}
