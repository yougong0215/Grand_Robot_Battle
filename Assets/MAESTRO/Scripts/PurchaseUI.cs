using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PurchaseUI : MonoBehaviour
{
    private UIDocument _doc;
    private VisualElement _root;
    private Button _exitBtn;
    private Button[] _purchasebtns = new Button[6];
    private string[] krws = { "990", "4900", "9900", "19000", "29000", "49000" };

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _exitBtn = _root.Q<Button>("exit-btn");
        _exitBtn.clicked += () => ActivePanel(false);
        for (int i = 0; i < _purchasebtns.Length; i++)
        {
            _purchasebtns[i] = _root.Q<VisualElement>($"{krws[i]}krw").Q<Button>("purchase-btn");
            _purchasebtns[i].clicked += () => PurchaseCrystal(krws[i]);
        }
    }

    public void ActivePanel(bool isActive)
    {
        _root.style.display = isActive ? DisplayStyle.Flex : DisplayStyle.None;
    }

    private void PurchaseCrystal(string value)
    {
        //서버에 연결
    }
}
