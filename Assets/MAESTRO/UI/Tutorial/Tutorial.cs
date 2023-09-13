using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _imagePanel;
    private VisualElement _bgPanel;
    private Button _LArrowBtn;
    private Button _RArrowBtn;
    private Button _exitBtn;

    [SerializeField] private Texture2D[] _guides = new Texture2D[4];
    private int _page;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _imagePanel = _root.Q<VisualElement>("image-panel");
        _LArrowBtn = _root.Q<Button>("left-arrow-btn");
        _RArrowBtn = _root.Q<Button>("right-arrow-btn");
        _exitBtn = _root.Q<Button>("exit-btn");
        _bgPanel = _root.Q<VisualElement>("bg-panel");

        _LArrowBtn.clicked += () => ChangePage(false);
        _RArrowBtn.clicked += () => ChangePage(true);
        _exitBtn.clicked += () => ActiveTuto(false);
        Debug.Log(_exitBtn);
    }

    public void ActiveTuto(bool isActive)
    {
        if (isActive)
            _bgPanel.RemoveFromClassList("off");
        else
            _bgPanel.AddToClassList("off");
    }

    private void ChangePage(bool isNext)
    {
        _page = isNext ? _page++ : _page--;
        _imagePanel.style.backgroundImage = _guides[_page];
    }
}
