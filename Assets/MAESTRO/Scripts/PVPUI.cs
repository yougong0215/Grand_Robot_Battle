using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PVPUI : MonoBehaviour
{
    #region 컴포넌트
    private UIDocument _uiDoc;
    private VisualElement _root;
    private Button _panel;
    private VisualElement _warning;

    private Button _atkBtn;
    private Button _skipBtn;
    private Button _surrenBtn;

    private Label _text;
    private Label _wText;

    private Button _yesBtn;
    private Button _noBtn;

    private Button[] partsbtns = new Button[5];
    private string[] partsClass = { "LA", "RA", "LL", "RL", "H" };

    private bool onPartsPanel;
    private bool onPanel;
    private bool onwarning;

    private RobotSettingAndSOList _robot;       // 임시방편
    private RobotSettingAndSOList _enemyRobot;  // 임시방편
    #endregion

    private void Awake()
    {
        _uiDoc = GetComponent<UIDocument>();
        _robot = GameObject.Find("MyRobot").GetComponent<RobotSettingAndSOList>();
        _enemyRobot = GameObject.Find("EnemyRobot").GetComponent<RobotSettingAndSOList>();
    }

    private void OnEnable()
    {
        #region 영수증
        _root = _uiDoc.rootVisualElement;
        _panel = _root.Q<Button>("Panel");
        _atkBtn = _root.Q<Button>("AttackBtn");
        _skipBtn = _root.Q<Button>("SkipBtn");
        _surrenBtn = _root.Q<Button>("SurrenderBtn");
        _warning = _root.Q<VisualElement>("WarningPanel");
        _text = _root.Q<Label>("Text");
        _wText = _root.Q<Label>("warningText");
        _yesBtn = _root.Q<Button>("Yesbtn");
        _noBtn = _root.Q<Button>("Nobtn");
        #endregion
        for (int i = 0; i < 5; i++)
        {
            partsbtns[i] = _root.Q<Button>($"{partsClass[i]}btn");
            partsbtns[i].clicked +=  () => OnButton(_robot.ReturnParts((PartBaseEnum)i));
        }
        #region 구독
        _atkBtn.clicked += SetPartsBtn;

        _skipBtn.clicked += OnWarning;
        _skipBtn.clicked += SkipLogic;

        _surrenBtn.clicked += OnWarning;
        _surrenBtn.clicked += SurrenderLogic;

        _yesBtn.clicked += OnWarning;
        _yesBtn.clicked += YesLogic;

        _noBtn.clicked += OnWarning;
        #endregion
    }


    public void OnButton(PartSO so)
    {
        StartCoroutine(Corutine(so));
    }

    public IEnumerator Corutine(PartSO so)
    {
        // 이거 다 서버로 바꿔야됨
        SetPanel(); // 꺼짐
        yield return new WaitForSeconds(0.1f);

        int rand = UnityEngine.Random.Range(0, 5);

        _panel.text = "로딩중..";
        yield return new WaitForSeconds(0.1f);
        _panel.text = "로딩중....";
        yield return new WaitForSeconds(0.1f);
        _panel.text = "로딩중......";
        yield return new WaitForSeconds(0.1f);
        bool t = SpeedReturn();
        yield return StartCoroutine(Fight(t, so, rand));
        t = !t;
        yield return StartCoroutine(Fight(t, so, rand));

        _panel.text = "로딩중....";
        yield return new WaitForSeconds(0.1f);
        _panel.text = "로딩중....";
        yield return new WaitForSeconds(0.1f);
        _panel.text = "로딩중..";
        yield return new WaitForSeconds(0.1f);
        SetPanel();


    }

    public IEnumerator Fight(bool t, PartSO so, int rand)
    {
        if (t == true)
        {
            _panel.text = so.Daesa;
            _enemyRobot._statues.HP -= (int)(_robot._statues.ATK * so.Count);
            yield return new WaitForSeconds(1f);
            _panel.text =
                $"{_robot.name}은 {_enemyRobot.name}에게 {_robot._statues.ATK * so.Count}의 피해를 입혔다.";
        }
        else
        {
            _panel.text = _enemyRobot.ReturnParts((PartBaseEnum)rand).Daesa;
            _robot._statues.HP -= (int)(_enemyRobot._statues.ATK * _enemyRobot.ReturnParts((PartBaseEnum)rand).Count);
            yield return new WaitForSeconds(1f);
            _panel.text =
                $"{_enemyRobot.name}은 {_robot.name}에게 {_enemyRobot._statues.ATK * _enemyRobot.ReturnParts((PartBaseEnum)rand).Count}의 피해를 입혔다.";
        }

        yield return new WaitForSeconds(2f);
    }


    public bool SpeedReturn()
    {
        if(_robot._statues.SPEED >= _enemyRobot._statues.SPEED)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnWarning()
    {
        if(onwarning)
        {
            _warning.AddToClassList("on");
        }
        else
        {
            _warning.RemoveFromClassList("off");
        }
        onwarning = !onwarning;
    }

    private void YesLogic()
    {
        // 스킵
    }
    
    private void SkipLogic()
    {
        _wText.text = "정말 스킵하시겠습니까?";
    }

    private void SurrenderLogic()
    {
        _wText.text = "정말 항복하시겠습니까?";
    }

    public void SetText(string txt)
    {
        _text.text = txt;
    }

    private void SetPanel()
    {
        if(!onPanel)
        {
            _panel.RemoveFromClassList("off");
        }
        else
        {
            _panel.AddToClassList("off");
        }

        onPanel = !onPanel;
    }

    private void SetPartsBtn()
    {
        if(!onPartsPanel)
        {
            for(int i = 0; i < 5; i++)
            {
                partsbtns[i].AddToClassList($"{partsClass[i]}");
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                partsbtns[i].RemoveFromClassList($"{partsClass[i]}");
            }

        }
        onPartsPanel = !onPartsPanel;
    }
}
