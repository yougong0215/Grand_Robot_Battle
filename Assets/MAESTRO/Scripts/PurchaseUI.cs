using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PurchaseUI : MonoBehaviour
{
    private UIDocument _doc;
    private VisualElement _root;
    private Button[] _purchasebtns = new Button[6];
    private string[] krws = { "990", "4900", "9900", "19000", "29000", "49000" };

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        for (int i = 0; i < _purchasebtns.Length; i++)
        {
            _purchasebtns[i] = _root.Q<VisualElement>($"{krws[i]}krw").Q<Button>("purchase-btn");
            _purchasebtns[i].clicked += () => PurchaseCrystal(krws[i]);
        }
    }

    private void PurchaseCrystal(string value)
    {

    }
}
