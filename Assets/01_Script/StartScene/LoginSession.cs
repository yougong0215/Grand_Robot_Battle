using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSession : MonoBehaviour
{
    public static string SAVE_KEY = "AccountToken";

    [SerializeField] GameObject PressText;
    [SerializeField] GameObject LogoutBtn;
    [SerializeField] GameObject LoginBtn;
    void Start()
    {
        string sessionToken = PlayerPrefs.GetString(SAVE_KEY);
        ChangeLayout(sessionToken.Length > 0);
    }

    public void ChangeLayout(bool logined) {
        PressText.SetActive(logined);
        LogoutBtn.SetActive(logined);
        LoginBtn.SetActive(!logined);
    }
}
