using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PVPUI : MonoBehaviour
{
    #region 컴포넌트
    private UIDocument _uiDoc;
    private VisualElement _root;
    private Button _panel;
    private Label _paneltxt;
    private VisualElement _warning;

    private VisualElement _playerHpBar;
    private Label _playerHpText;
    private Button _playerInfobtn;

    private VisualElement _enemyHpBar;
    private Label _enemyHpText;
    private Button _enemyInfoBtn;

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
        _paneltxt = _root.Q<Label>("Text");
        _wText = _root.Q<Label>("warningText");
        _yesBtn = _root.Q<Button>("Yesbtn");
        _noBtn = _root.Q<Button>("Nobtn");
        _playerHpBar = _root.Q<VisualElement>("PlayerHPBar");
        _playerHpText = _root.Q<Label>("PlayerCurrentHP");
        _enemyHpBar = _root.Q<VisualElement>("EnemyHPBar");
        _enemyHpText = _root.Q<Label>("EnemyCurrentHP");
        #endregion
        for (int i = 0; i < 5; i++)
        {
            partsbtns[i] = _root.Q<Button>($"{partsClass[i]}btn");

            PartSO so;
            if (_robot.ReturnParts((PartBaseEnum)i) != null)
            {
                so = _robot.ReturnParts((PartBaseEnum)i);
                partsbtns[i].style.backgroundImage = new StyleBackground(so.SkillImage);
            }
            else
            {
                so = new PartSO();
                so.PartBase = (PartBaseEnum)i;

            }
            partsbtns[i].clicked += () => OnButton(so);

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
        SetPanel(); // 켜짐
        SetPartsBtn();
        _atkBtn.RemoveFromClassList("on");
        _surrenBtn.RemoveFromClassList("on");
        _skipBtn.RemoveFromClassList("on");
        yield return new WaitForSeconds(0.1f);

        int rand = UnityEngine.Random.Range(0, 5);

        _paneltxt.text = "로딩중..";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "로딩중....";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "로딩중......";
        yield return new WaitForSeconds(0.3f);
        bool t = SpeedReturn();
        yield return StartCoroutine(Fight(t, so, rand));
        t = !t;
        yield return StartCoroutine(Fight(t, so, rand));

        _paneltxt.text = "로딩중....";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "로딩중....";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "로딩중..";
        yield return new WaitForSeconds(0.3f);
        SetPanel();
        _atkBtn.AddToClassList("on");
        _surrenBtn.AddToClassList("on");
        _skipBtn.AddToClassList("on");
    }


    

    public IEnumerator Fight(bool t, PartSO so, int rand)
    {
        if (t == true)
        {
            _paneltxt.text = $"나의 턴!!!";
            yield return new WaitForSeconds(0.5f);
            _paneltxt.text = so.Daesa;
            _enemyRobot._statues.HP -= (int)(_robot._statues.ATK * so.Count);


            yield return new WaitForSeconds(1f);
            if(so.clips != null)
            {
                SetPanel(); // 켜짐
                _robot.GetComponent<AnimationBind>().AnimationChange(so.clips);



                yield return new WaitUntil(() => _robot.GetComponent<AnimationBind>().EndAnim());

                SetPanel();
            }
            else
            {
                _paneltxt.text = $"지정된 에니메이션이 없습니다";
                yield return new WaitForSeconds(1f);
            }
           
            _paneltxt.text =
                $"{_robot.name}은 {_enemyRobot.name}에게 {_robot._statues.ATK * so.Count}의 피해를 입혔다. ( 적HP : { _enemyRobot._statues.HP} )";

            if (_enemyRobot._statues.HP <= 0)
            {
                yield return new WaitForSeconds(1.5f);
                _paneltxt.text = $"HP : {_robot._statues.HP} 나의 승리..!";
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("Menu");
            }
        }
        else
        {
            if (_enemyRobot.ReturnParts((PartBaseEnum)rand) != null)
            {
                _paneltxt.text = $"적의 턴!!!";
                yield return new WaitForSeconds(0.5f);
                _paneltxt.text = _enemyRobot.ReturnParts((PartBaseEnum)rand).Daesa;
                _robot._statues.HP -= (int)(_enemyRobot._statues.ATK * _enemyRobot.ReturnParts((PartBaseEnum)rand).Count);

                yield return new WaitForSeconds(1f);
                if (so.clips != null)
                {
                    SetPanel(); // 켜짐
                    _enemyRobot.GetComponent<AnimationBind>().AnimationChange(so.clips);



                    yield return new WaitUntil(() => _enemyRobot.GetComponent<AnimationBind>().EndAnim());

                    SetPanel();
                }
                else
                {
                    _paneltxt.text = $"지정된 에니메이션이 없습니다";
                    yield return new WaitForSeconds(1f);
                }


                _paneltxt.text =
                    $"{_enemyRobot.name}은 {_robot.name}에게 {_enemyRobot._statues.ATK * _enemyRobot.ReturnParts((PartBaseEnum)rand).Count}의 피해를 입혔다. ( 나의HP : {_robot._statues.HP} )";
            }
            else
            {
                _paneltxt.text = $"적의 턴!!!";
                yield return new WaitForSeconds(0.5f);
                _panel.text = "적이 아직 데이터를 가지고 있지 않습니다.";
                _robot._statues.HP -= (int)(_enemyRobot._statues.ATK * 1);
                yield return new WaitForSeconds(1f);
                if (so.clips != null)
                {
                    SetPanel(); // 켜짐
                    _enemyRobot.GetComponent<AnimationBind>().AnimationChange(so.clips);



                    yield return new WaitUntil(() => _enemyRobot.GetComponent<AnimationBind>().EndAnim());

                    SetPanel();

                }
                else
                {
                    _paneltxt.text = $"지정된 에니메이션이 없습니다";
                    yield return new WaitForSeconds(1f);
                }


                _paneltxt.text =
                    $"{_enemyRobot.name}은 {_robot.name}에게 {_enemyRobot._statues.ATK}의 피해를 입혔다. ( 나의HP : {_robot._statues.HP} )";
            }

            if (_robot._statues.HP <= 0)
            {
                yield return new WaitForSeconds(1.5f);
                _paneltxt.text = $"HP : {_robot._statues.HP} 적의 승리..";
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("Menu");

            }


        }

        yield return new WaitForSeconds(2.5f);
    }


    public bool SpeedReturn()
    {
        if (_robot._statues.SPEED >= _enemyRobot._statues.SPEED)
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
        if (onwarning)
        {
            _warning.RemoveFromClassList("off");
        }
        else
        {
            _warning.AddToClassList("off");
        }
        onwarning = !onwarning;
    }

    private void YesLogic()
    {
        // 스킵
        StartCoroutine(Skip());
    }

    IEnumerator Skip()
    {
        OnWarning();
        SetPanel(); // 꺼짐
        SetPartsBtn();
        _atkBtn.RemoveFromClassList("on");
        _surrenBtn.RemoveFromClassList("on");
        _skipBtn.RemoveFromClassList("on");
        yield return new WaitForSeconds(0.1f);

        int rand = UnityEngine.Random.Range(0, 5);

        _paneltxt.text = "로딩중..";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "로딩중....";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "로딩중......";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = $"나의 턴은 스킵되었다  ( 적HP : { _enemyRobot._statues.HP} )";
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Fight(false, null, rand));

        _paneltxt.text = "로딩중....";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "로딩중....";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "로딩중..";
        yield return new WaitForSeconds(0.3f);
        SetPanel();
        _atkBtn.AddToClassList("on");
        _surrenBtn.AddToClassList("on");
        _skipBtn.AddToClassList("on");


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
        if (!onPanel)
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
        if (!onPartsPanel)
        {
            for (int i = 0; i < 5; i++)
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
