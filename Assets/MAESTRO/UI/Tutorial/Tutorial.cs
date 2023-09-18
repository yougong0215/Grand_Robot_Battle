using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _imagePanel;
    private Button _LArrowBtn;
    private Button _RArrowBtn;
    private Button _exitBtn;

    [SerializeField] private Texture2D[] _guides = new Texture2D[2];
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

        _LArrowBtn.clicked += () => ChangePage(false);
        _RArrowBtn.clicked += () => ChangePage(true);
        _exitBtn.clicked += () => ActiveTuto(false);
        Debug.Log(_exitBtn);
    }

    public void ActiveTuto(bool isActive)
    {
        if (isActive)
            _root.style.display = DisplayStyle.Flex;
        else
            _root.style.display = DisplayStyle.None;
    }

    private void ChangePage(bool isNext)
    {
        if (_page + 1 > _guides.Length || _page - 1 < 0)
            return;
        _page = isNext ? _page++ : _page--;
        _imagePanel.style.backgroundImage = _guides[_page];
    }
}
