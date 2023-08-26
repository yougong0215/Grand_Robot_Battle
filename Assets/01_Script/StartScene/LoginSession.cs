using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSession : MonoBehaviour
{
    public static string SAVE_KEY = "AccountToken";

    [SerializeField] GameObject PressText;
    [SerializeField] GameObject LogoutBtn;
    [SerializeField] GameObject LoginBtn;
    [SerializeField] GameObject LoginBtn2;
    [SerializeField] GameObject ToutchBtn;
    void Start()
    {
        string sessionToken = PlayerPrefs.GetString(SAVE_KEY);
        ChangeLayout(sessionToken.Length > 0);
    }

    public void ChangeLayout(bool logined) {
        PressText.SetActive(logined);
        LogoutBtn.SetActive(logined);
        ToutchBtn.SetActive(logined);
        LoginBtn.SetActive(!logined);
        LoginBtn2.SetActive(!logined);
    }
}
