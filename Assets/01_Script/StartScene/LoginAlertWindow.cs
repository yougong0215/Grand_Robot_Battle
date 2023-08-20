using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginAlertWindow : MonoBehaviour
{
    static LoginAlertWindow instance;

    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _content;
    
    private void Awake() {
        if (instance == null)
            instance = this;

        gameObject.SetActive(false);
    }

    public static void ShowUI(string title, string content) {
        instance.gameObject.SetActive(true);
        instance._title.text = title;
        instance._content.text = content;
    }
    
    public static void HideUI() {
        instance.gameObject.SetActive(false);
    }
    public void HideUI2() {
        gameObject.SetActive(false);
    }
}
