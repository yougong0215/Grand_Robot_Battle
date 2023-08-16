using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum ModuleList
{
    sample,

}


public class ContentSelectUI : MonoBehaviour
{
    [SerializeField] private Vector2 pos;
    [SerializeField] private ModuleList _moduleList;
    UIDocument _doc;
    VisualElement _root;

    Button _sample;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        _root = _doc.rootVisualElement;
    }

    private void OnEnable()
    {
        _sample = _root.Q<Button>("Sample");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _sample.AddToClassList(_moduleList.ToString());
            _sample.transform.position = pos;
        }
    }
}
