using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PVPUI : MonoBehaviour
{
    #region ������Ʈ
    private UIDocument _uiDoc;
    private VisualElement _root;
    private Button _panel;
    private Label _paneltxt;
    private VisualElement _warning;

    private Label _playerNickname;
    private VisualElement _playerHpBar;
    private Label _playerHpText;

    private Label _enemtNickname;
    private VisualElement _enemyHpBar;
    private Label _enemyHpText;

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

    private RobotSettingAndSOList _robot;       // �ӽù���
    private RobotSettingAndSOList _enemyRobot;  // �ӽù���
    #endregion
    private float _playerMaxHP;
    private float _playerCurrentHP;
    private float _enemyMaxHP;
    private float _enemyCurrentHP;

    private void Awake()
    {
        _uiDoc = GetComponent<UIDocument>();
        _robot = GameObject.Find("MyRobot").GetComponent<RobotSettingAndSOList>();
        _enemyRobot = GameObject.Find("EnemyRobot").GetComponent<RobotSettingAndSOList>();

        NetworkCore.EventListener["ingame.AttackControl"] = ActiveControl;
        NetworkCore.EventListener["ingame.gameresult"] = ServerGameResult;
    }

    private void Start()
    {
        _paneltxt.text = "�ٸ� �÷��̾ ��ٸ��� �ֽ��ϴ�.";
        SetPanel();
        _atkBtn.RemoveFromClassList("on");
        _surrenBtn.RemoveFromClassList("on");
        _skipBtn.RemoveFromClassList("on");
        
        // �������� �غ� �Ǿ��ٰ� �˸�
        NetworkCore.Send("ingame.ready", null);
    }

    private void OnEnable()
    {
        #region ������
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
        _playerNickname = _root.Q<Label>("NickName");
        _playerHpBar = _root.Q<VisualElement>("PlayerHPBar");
        _playerHpText = _root.Q<Label>("PlayerCurrentHP");
        _enemtNickname = _root.Q<Label>("EnemyNickName");
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
            // partsbtns[i].clicked += () => OnButton(so);
            print(i);
            partsbtns[i].clicked += () => SelectSkillForServer(0);

        }
        #region ����
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

    public void SetNameText(bool isPlayer, string name)
    {
        if(isPlayer)
        {
            _playerHpText.text = name;
        }
        else
        {
            _enemyHpText.text = name;
        }
    }

    public void SetMaxHP(float playerHP, float enemyHP)
    {
        _playerMaxHP = _playerCurrentHP = playerHP;
        _enemyMaxHP = _enemyCurrentHP = enemyHP;
    }

    public void SetHPValue(bool isPlayer, float damage)
    {
        if(isPlayer)
        {
            _playerCurrentHP -= damage;
            _playerHpBar.style.width = _playerCurrentHP / _playerMaxHP;
            _playerHpText.text = $"{_playerCurrentHP / _playerMaxHP}";
        }
        else
        {
            _enemyCurrentHP -= damage;
            _enemyHpBar.style.width = _enemyCurrentHP / _enemyMaxHP;
            _enemyHpText.text = $"{_enemyCurrentHP / _enemyMaxHP}";
        }
    }


    public void OnButton(PartSO so)
    {
        StartCoroutine(Corutine(so));
    }

    public IEnumerator Corutine(PartSO so)
    {
        // �̰� �� ������ �ٲ�ߵ�
        SetPanel(); // ����
        SetPartsBtn();
        _atkBtn.RemoveFromClassList("on");
        _surrenBtn.RemoveFromClassList("on");
        _skipBtn.RemoveFromClassList("on");
        yield return new WaitForSeconds(0.1f);

        int rand = UnityEngine.Random.Range(0, 5);

        _paneltxt.text = "�ε���..";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "�ε���....";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "�ε���......";
        yield return new WaitForSeconds(0.3f);
        bool t = SpeedReturn();
        yield return StartCoroutine(Fight(t, so, rand));
        t = !t;
        yield return StartCoroutine(Fight(t, so, rand));

        _paneltxt.text = "�ε���....";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "�ε���....";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "�ε���..";
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
            _paneltxt.text = $"���� ��!!!";
            yield return new WaitForSeconds(0.5f);
            _paneltxt.text = so.Daesa;
            _enemyRobot._statues.HP -= (int)(_robot._statues.ATK * so.Count);

            _enemyHpBar.style.scale = new StyleScale(new Scale(new Vector3(Mathf.Lerp(0f,1f, _enemyRobot._statues.HP/ _enemyRobot.MaxHP), 1, 0)));
            _enemyHpText.text = $"{_enemyRobot._statues.HP} / {_enemyRobot.MaxHP}";
            yield return new WaitForSeconds(1f);
            //if(so.clips != null)
            {
                SetPanel(); // ����
                _robot.GetComponent<AnimationBind>().AnimationChange(so.clips);



                yield return new WaitUntil(() => _robot.GetComponent<AnimationBind>().EndAnim());

                SetPanel();
            }
            //else
            //{
            //    _paneltxt.text = $"������ ���ϸ��̼��� �����ϴ�";
            //    yield return new WaitForSeconds(1f);
            //}
           
            _paneltxt.text =
                $"{_robot.name}�� {_enemyRobot.name}���� {_robot._statues.ATK * so.Count}�� ���ظ� ������. ( ��HP : { _enemyRobot._statues.HP} )";

            if (_enemyRobot._statues.HP <= 0)
            {
                yield return new WaitForSeconds(1.5f);
                _paneltxt.text = $"���� �¸�..!";
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("Menu");
            }
        }
        else
        {
            if (_enemyRobot.ReturnParts((PartBaseEnum)rand) != null)
            {
                _paneltxt.text = $"���� ��!!!";
                yield return new WaitForSeconds(0.5f);
                _paneltxt.text = _enemyRobot.ReturnParts((PartBaseEnum)rand).Daesa;
                _robot._statues.HP -= (int)(_enemyRobot._statues.ATK * _enemyRobot.ReturnParts((PartBaseEnum)rand).Count);
                _playerHpBar.style.scale = new StyleScale(new Scale(new Vector3(Mathf.Lerp(0f, 1f, _robot._statues.HP / _robot.MaxHP), 1, 0)));
                _playerHpText.text = $"{_robot._statues.HP} / {_robot.MaxHP}";
                yield return new WaitForSeconds(1f);
                //if (so.clips != null)
                {
                    SetPanel(); // ����
                    _enemyRobot.GetComponent<AnimationBind>().AnimationChange(so.clips);



                    yield return new WaitUntil(() => _enemyRobot.GetComponent<AnimationBind>().EndAnim());

                    SetPanel();
                }
                //else
                //{
                //    _paneltxt.text = $"������ ���ϸ��̼��� �����ϴ�";
                //    yield return new WaitForSeconds(1f);
                //}


                _paneltxt.text =
                    $"{_enemyRobot.name}�� {_robot.name}���� {_enemyRobot._statues.ATK * _enemyRobot.ReturnParts((PartBaseEnum)rand).Count}�� ���ظ� ������. ( ����HP : {_robot._statues.HP} )";
            }
            else
            {
                _paneltxt.text = $"���� ��!!!";
                yield return new WaitForSeconds(0.5f);
                _panel.text = "���� ���� �����͸� ������ ���� �ʽ��ϴ�.";
                _robot._statues.HP -= (int)(_enemyRobot._statues.ATK * 1);

                _playerHpBar.style.scale = new StyleScale(new Scale(new Vector3(Mathf.Lerp(0f, 1f, _robot._statues.HP / _robot.MaxHP), 1, 0)));
                _playerHpText.text = $"{_robot._statues.HP} / {_robot.MaxHP}";
                yield return new WaitForSeconds(1f);
                //if (so.clips != null)
                {
                    SetPanel(); // ����
                    _enemyRobot.GetComponent<AnimationBind>().AnimationChange(so.clips);



                    yield return new WaitUntil(() => _enemyRobot.GetComponent<AnimationBind>().EndAnim());

                    SetPanel();

                }
                //else
                //{
                //    _paneltxt.text = $"������ ���ϸ��̼��� �����ϴ�";
                //    yield return new WaitForSeconds(1f);
                //}


                _paneltxt.text =
                    $"{_enemyRobot.name}�� {_robot.name}���� {_enemyRobot._statues.ATK}�� ���ظ� ������. ( ����HP : {_robot._statues.HP} )";
            }

            if (_robot._statues.HP <= 0)
            {
                yield return new WaitForSeconds(1.5f);
                _paneltxt.text = $"HP : {_robot._statues.HP} ���� �¸�..";
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
            _warning.AddToClassList("off");
        }
        else
        {
            _warning.RemoveFromClassList("off");
        }
        onwarning = !onwarning;
    }

    private void YesLogic()
    {
        // ��ŵ
        StartCoroutine(Skip());
    }

    IEnumerator Skip()
    {
        //OnWarning();
        SetPanel(); // ����
        //SetPartsBtn();
        _atkBtn.RemoveFromClassList("on");
        _surrenBtn.RemoveFromClassList("on");
        _skipBtn.RemoveFromClassList("on");
        yield return new WaitForSeconds(0.1f);

        int rand = UnityEngine.Random.Range(0, 5);

        _paneltxt.text = "�ε���..";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "�ε���....";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "�ε���......";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = $"���� ���� ��ŵ�Ǿ���  ( ��HP : { _enemyRobot._statues.HP} )";
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Fight(false, null, rand));

        _paneltxt.text = "�ε���....";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "�ε���....";
        yield return new WaitForSeconds(0.3f);
        _paneltxt.text = "�ε���..";
        yield return new WaitForSeconds(0.3f);
        SetPanel();
        _atkBtn.AddToClassList("on");
        _surrenBtn.AddToClassList("on");
        _skipBtn.AddToClassList("on");


    }

    private void SkipLogic()
    {
        _wText.text = "���� ��ŵ�Ͻðڽ��ϱ�?";
    }

    private void SurrenderLogic()
    {
        _wText.text = "���� �׺��Ͻðڽ��ϱ�?";
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

    ///////////// ���� //////////////
    private void ActiveControl(LitJson.JsonData _ = null) {
        _atkBtn.AddToClassList("on");
        _surrenBtn.AddToClassList("on");
        _skipBtn.AddToClassList("on");

        if (!onPanel) return;
        SetPanel();
    }
    private void SelectSkillForServer(int part) {
        print("part");
        print(part);
        NetworkCore.Send("ingame.selectSkill", part);

        _paneltxt.text = "��ų�� �����߽��ϴ�. �ٸ� �÷��̾� ��ٸ�����...";
        _atkBtn.RemoveFromClassList("on");
        _surrenBtn.RemoveFromClassList("on");
        _skipBtn.RemoveFromClassList("on");
        SetPanel();
        SetPartsBtn();
    }
    
    public class PVP_GameResult {
        public bool my;
        public string attacker;
        public string hitter;
        public bool answer;
        public int power;
        public int health;
        public string why;
    }
    private void ServerGameResult(LitJson.JsonData data) {
        StartCoroutine(ServerGameResult_Co(data));
    }

    IEnumerator ServerGameResult_Co(LitJson.JsonData data) {
        bool disableControl = false;
        for (int i = 0; i < 2; i++)
        {
            var result = LitJson.JsonMapper.ToObject<PVP_GameResult>(data[i].ToJson());
            
            if (result.answer == true) {
                _paneltxt.text = result.my ? "���� ��" : "���� ��";
                yield return new WaitForSeconds(1f);

                SetHPValue(!result.my, result.power);
                _paneltxt.text =
                    $"{result.attacker}�� {result.hitter}���� {result.power}�� ���ظ� ������. ( {(result.my ? "��" : "��")}��HP : {result.health} )";
                yield return new WaitForSeconds(3f);

                if (result.why == "domiNotHealthEvent") {
                    _paneltxt.text = result.my ? "���� �¸�!!" : "���� �¸�..";
                    disableControl = true;
                }

            } else if (result.why == "domiNotHealthEvent") {
                _paneltxt.text = result.my ? "���� �¸�.." : "���� �¸�!!";
                disableControl = true;
                yield return new WaitForSeconds(0.5f);
            } else {
                _paneltxt.text = (result.my ? "" : "���� ") + result.why;
                yield return new WaitForSeconds(1.5f);
            }
        }

        if (!disableControl) ActiveControl();
    }
}
