using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MakeUI : MonoBehaviour
{
    private UIDocument _doc;
    private VisualElement _root;
    private Button _1_button;
    private Button _10_button;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        _1_button = _root.Q<Button>("1_Button");
        _10_button = _root.Q<Button>("10_Button");


        _1_button.clicked += () => GachaStart(1);
        _10_button.clicked += () => GachaStart(10);
        
    }

    private void GachaStart(int count)
    {

    }
}
