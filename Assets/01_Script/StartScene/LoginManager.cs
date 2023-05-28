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

    private void Awake() {
        if (instance == null)
            instance = this;

        for (int i = 0; i < Account_List.childCount; i++)
            Destroy(Account_List.GetChild(i).gameObject);

        var Accounts = GetSaveAccount();

        foreach (var Account in Accounts)
            ButtonAdd(Account.name, Account.id, Account.token);
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
    }
}
