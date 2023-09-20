using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameExit : MonoBehaviour
{
    private UIDocument _ui;
    private Button Exitbtn;
    private Button NoBtn;
    private VisualElement _Window;

    private VisualElement _root;
    private void Awake()
    {
        _ui = GetComponent<UIDocument>();
        _root = _ui.rootVisualElement;
        Exitbtn = _root.Q<Button>("YesBtn");

        NoBtn = _root.Q<Button>("NoBtn");
        _Window = _root.Q<VisualElement>("Window");
        NoBtn.clicked += () => _Window.AddToClassList("off");
        Exitbtn.clicked += () => Application.Quit();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Application.Quit();
        }
    }
}
