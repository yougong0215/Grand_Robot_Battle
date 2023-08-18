using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private Button _exitBtn;

    private VisualElement _gachaPanel;
    private VisualElement UP;
    private VisualElement DOWN;
    private VisualElement MIDDLE;

    private Button _okResult;
    private Button _moreResult;

    Coroutine GachaCo;
    bool _isRendering;
    bool _is_10;

    GetServerToSO DomiSo;

    LitJson.JsonData ItemResult;
    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        NetworkCore.EventListener["Gacha.error"] = ErrorWindow;
        NetworkCore.EventListener["Gacha.Result_1"] = Result_1;
        NetworkCore.EventListener["Gacha.Result_10"] = Result_10;
        DomiSo = GetComponent<GetServerToSO>();
    }
    private void OnDestroy() {
        NetworkCore.EventListener.Remove("Gacha.error");
        NetworkCore.EventListener.Remove("Gacha.Result_1");
        NetworkCore.EventListener.Remove("Gacha.Result_10");
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

        _exitBtn = _root.Q<Button>("ExitBtn");

        _bg.clicked += SkipLoading;
        _1_button.clicked += StartGacha_1;
        _10_button.clicked += StartGacha_10;
        _okResult.clicked += () => Exit(false);
        _moreResult.clicked += () => Exit(true);
        _exitBtn.clicked += () => SceneManager.LoadScene("SelectStoreScene");
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


        if(_is_10)
        {
            StartGacha_10();
        }
        else
        {
            StartGacha_1();
        }

        _okResult.RemoveFromClassList("on");
        _moreResult.RemoveFromClassList("on");
    }

    private void SkipLoading()
    {
        if(_isRendering)
        {
            _isRendering = false;
            StopCoroutine(GachaCo);
            GachaRender();
        }
    }

    private void StartGacha_1()
    {
        // GachaCo = StartCoroutine(GachaCoroutine());
        NetworkCore.Send("Gacha.Start_1", null);
    }

    private void StartGacha_10()
    {
        NetworkCore.Send("Gacha.Start_10", null);
        // GachaCo = StartCoroutine(GachaCoroutine());
        _is_10 = true;
    }

    private void GachaRender()
    {
        _gachaPanel.AddToClassList("on");

        if (!_is_10)
        {
            VisualElement ele = _randEle.Instantiate();
            // ele ������ �ֱ�
            string ItemCode = (string)ItemResult;

            PartSO so = DomiSo.ReturnSO(ItemCode);


            ele.Q<Label>("RatingTxt").text = so.SOname;
            if (so.Sprite != null)
                ele.Q<VisualElement>("Imaged").style.backgroundImage = new StyleBackground(so.Sprite);

            MIDDLE.Add(ele);
            _randEleList.Add(ele);
        }
        else
        {
            string[] ItemList = LitJson.JsonMapper.ToObject<string[]>(ItemResult.ToJson());
            int t = 0;
            for (int i = 0; i < 5; i++)
            {
                VisualElement ele = _randEle.Instantiate();
                // ele������ �ֱ�

                PartSO so = DomiSo.ReturnSO(ItemList[t]);


                ele.Q<Label>("RatingTxt").text = so.SOname;
                if (so.Sprite != null)
                    ele.Q<VisualElement>("Imaged").style.backgroundImage = new StyleBackground(so.Sprite);

                UP.Add(ele);
                _randEleList.Add(ele);
                
                t++;
            }
            for (int i = 0; i < 5; i++)
            {
                VisualElement ele = _randEle.Instantiate();
                //ele������ �ֱ�
                PartSO so = DomiSo.ReturnSO(ItemList[t]);


                ele.Q<Label>("RatingTxt").text = so.SOname;
                if (so.Sprite != null)
                    ele.Q<VisualElement>("Imaged").style.backgroundImage = new StyleBackground(so.Sprite);


                DOWN.Add(ele);
                _randEleList.Add(ele);
                t++;
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

    //////////// ���� ������ //////////// 
    void ErrorWindow(LitJson.JsonData Why) {
        Debug.LogError("�����ϴٰ� �����߾�� : "+Why);
    }
    void Result_1(LitJson.JsonData ItemResult) {

        this.ItemResult = ItemResult;
        string ItemCode = (string)ItemResult;
        Debug.Log("��! ��í�Ǥ��Ҿ�� : "+ ItemCode);

        GachaCo = StartCoroutine(GachaCoroutine());
    }


    void Result_10(LitJson.JsonData ItemResult) {
        this.ItemResult = ItemResult;
        string[] ItemList = LitJson.JsonMapper.ToObject<string[]>(ItemResult.ToJson());

        foreach (var ItemCode in ItemList)
        {
            Debug.Log("��! �������� �̾Ҿ�� : "+ItemCode);
        }

        GachaCo = StartCoroutine(GachaCoroutine());
        _is_10 = true;
    }
}
