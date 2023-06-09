using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MakeUI : MonoBehaviour
{
    private List<VisualElement> _randEleList = new List<VisualElement>();
    [SerializeField] private VisualTreeAsset _randEle;
    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _panel;
    private Button _bg;
    private Button _1_button;
    private Button _10_button;

    private VisualElement _gachaPanel;
    private VisualElement UP;
    private VisualElement DOWN;
    private VisualElement MIDDLE;

    private Button _okResult;
    private Button _moreResult;

    Coroutine GachaCo;
    bool _isRendering;
    bool _is_10;
    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _panel = _root.Q<VisualElement>("BackPanel");
        _bg = _root.Q<Button>("BG");
        _1_button = _root.Q<Button>("1_Button");
        _10_button = _root.Q<Button>("10_Button");
        _gachaPanel = _root.Q<VisualElement>("GachaPanel");
        UP = _root.Q<VisualElement>("Up");
        DOWN = _root.Q<VisualElement>("Down");
        MIDDLE = _root.Q<VisualElement>("Middle");

        _okResult = _root.Q<Button>("OK");
        _moreResult = _root.Q<Button>("OneMore");

        _bg.clicked += SkipLoading;
        _1_button.clicked += StartGacha_1;
        _10_button.clicked += StartGacha_10;
        _okResult.clicked += () => Exit(false);
        _moreResult.clicked += () => Exit(true);
    }

    IEnumerator ResultTurm()
    {
        
        yield return new WaitForSeconds(1);
        _okResult.AddToClassList("on");
        _moreResult.AddToClassList("on");
    }

    private void Exit(bool isMore)
    {
        foreach(VisualElement ve in _randEleList)
        {
            ve.RemoveFromHierarchy();
        }

        if(!isMore)
        {
            _gachaPanel.RemoveFromClassList("on");
            _panel.RemoveFromClassList("off");
            _is_10 = false;
            return;
        }
        GachaCo = StartCoroutine(GachaCoroutine());

        _okResult.RemoveFromClassList("on");
        _moreResult.RemoveFromClassList("on");
    }

    private void SkipLoading()
    {
        if(_isRendering)
        {
            StopCoroutine(GachaCo);
            GachaRender();
        }
    }

    private void StartGacha_1()
    {
        GachaCo = StartCoroutine(GachaCoroutine());
    }

    private void StartGacha_10()
    {
        GachaCo = StartCoroutine(GachaCoroutine());
        _is_10 = true;
    }

    private void GachaRender()
    {
        _gachaPanel.AddToClassList("on");

        if (!_is_10)
        {
            VisualElement ele = _randEle.Instantiate();
            // ele ������ �ֱ�
            MIDDLE.Add(ele);
            _randEleList.Add(ele);
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                VisualElement ele = _randEle.Instantiate();
                // ele������ �ֱ�
                UP.Add(ele);
                _randEleList.Add(ele);
            }
            for (int i = 0; i < 5; i++)
            {
                VisualElement ele = _randEle.Instantiate();
                //ele������ �ֱ�
                DOWN.Add(ele);
                _randEleList.Add(ele);
            }
        }
        StartCoroutine(ResultTurm());
    }

    IEnumerator GachaCoroutine()
    {
        _isRendering = true;
        if(!_panel.ClassListContains("off"))
            _panel.AddToClassList("off");

        _gachaPanel?.RemoveFromClassList("on");
        yield return new WaitForSeconds(2);
        GachaRender();
    }
}