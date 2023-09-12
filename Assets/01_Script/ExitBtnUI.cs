using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ExitBtnUI : MonoBehaviour
{
    UIDocument _ui;    private VisualElement _root;
    Button _exit;
    void OnEnable()
    {
        _ui = GetComponent<UIDocument>();
        _root = _ui.rootVisualElement;


        _exit = _root.Q<Button>("ExitBtn");
        _exit.clicked += () => LoadManager.ReturnBack();
    }

    void Update()
    {
        Debug.Log("Eeee");
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            LoadManager.ReturnBack();
        }
    }

}
