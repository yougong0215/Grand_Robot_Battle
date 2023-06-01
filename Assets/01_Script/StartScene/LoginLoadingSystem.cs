using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class LoginLoadingSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextUI;
    [SerializeField] GameObject ScreenUI;
    CanvasGroup CanVa;

    static LoginLoadingSystem instance;

    private void Awake() {
        if (instance == null)
            instance = this;

        CanVa = ScreenUI.GetComponent<CanvasGroup>();
    }

    static void ShowUI(string text) {
        instance.TextUI.text = text;
        instance.ScreenUI.SetActive(true);
        instance.CanVa.DOFade(1, .2f);
    }

    static void HideUI() {
        instance.CanVa.DOFade(0, .2f).OnComplete(() => instance.ScreenUI.SetActive(false));
    }
}
