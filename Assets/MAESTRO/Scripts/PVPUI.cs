using System;
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
    private Label _panel;
    //private Label _panel;
    private VisualElement _warning;
    private GetServerToSO _SOserver;

    private Label _playerNickname;
    private VisualElement _playerHpBar;
    private VisualElement _settingPanel;
    private Label _playerHpText;

    private Label _enemtNickname;
    private VisualElement _enemyHpBar;
    private Label _enemyHpText;

    //private Button _atkBtn;
    //private Button //_skipBtn;
    //private Button //_surrenBtn;

    VisualElement _partBtnGroup;

    private Label _text;
    private Label _wText;

    private Button _yesBtn;
    private Button _noBtn;

    private VisualElement partsbtnGroup;
    private VisualElement _hpPanel;
    private VisualElement _enemyHpPanel;
    private Button[] partsbtns = new Button[5];
    private float[] partbtncools;
    private float[] maxPartcools;
    private string[] partsClass = { "LA", "RA", "LL", "RL", "H" };

    private bool onPartsPanel;
    private bool onPanel;
    private bool onwarning;

    private RobotSettingAndSOList _robot;       // 임시방편
    private RobotSettingAndSOList _enemyRobot;  // 임시방편
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
        _SOserver = FindObjectOfType<GetServerToSO>();
        

        NetworkCore.EventListener["ingame.AttackControl"] = ActiveControl;
        NetworkCore.EventListener["ingame.gameresult"] = ServerGameResult;
        NetworkCore.EventListener["ingame.destory"] = ServerGameDestory;
    }

    private void OnDestroy() {
        NetworkCore.EventListener.Remove("ingame.AttackControl");
        NetworkCore.EventListener.Remove("ingame.gameresult");
        NetworkCore.EventListener.Remove("ingame.destory");
        
    }

    private void Start()
    {
        _panel.text = "다른 플레이어를 기다리고 있습니다.";
        SetPanel();
        //_atkBtn.RemoveFromClassList("on");
        //_surrenBtn.RemoveFromClassList("on");
        //_skipBtn.RemoveFromClassList("on");
        
        // 서버에게 준비가 되었다고 알림
        NetworkCore.Send("ingame.ready", null);
        PartsBtnSetting(true);
    }

    private void OnEnable()
    {
        #region 영수증
        _root = _uiDoc.rootVisualElement;
        _panel = _root.Q<Label>("Panel");
        _warning = _root.Q<VisualElement>("WarningPanel");
        //_panel = _root.Q<Label>("Text");
        _wText = _root.Q<Label>("warningText");
        _yesBtn = _root.Q<Button>("Yesbtn");
        _noBtn = _root.Q<Button>("Nobtn");
        _playerNickname = _root.Q<Label>("NickName");
        _playerHpBar = _root.Q<VisualElement>("PlayerHPBar");
        _playerHpText = _root.Q<Label>("PlayerCurrentHP");
        _enemtNickname = _root.Q<Label>("EnemyNickName");
        _enemyHpBar = _root.Q<VisualElement>("EnemyHPBar");
        _enemyHpText = _root.Q<Label>("EnemyCurrentHP");
        partsbtnGroup = _root.Q<VisualElement>("PartsBtnGroup");
        _hpPanel = _root.Q<VisualElement>("HPPanel");
        _enemyHpPanel = _root.Q<VisualElement>("EnemyHPPanel");
        _settingPanel = _root.Q<VisualElement>("SettingPanel");
        #endregion

        partbtncools = new float[5];
        /* -- 서버에서 처리함
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
        */
        //_settingPanel.RemoveFromClassList("off");
        #region 구독

        _yesBtn.clicked += OnWarning;
        _yesBtn.clicked += YesLogic;

        _noBtn.clicked += OnWarning;
        
        #endregion

    }

    public void SetNameText(bool isPlayer, string name)
    {
        if(isPlayer)
        {
            _playerNickname.text = name;
        }
        else
        {
            _enemtNickname.text = name;
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
            if(_playerCurrentHP - damage < 0)
            {
                _playerCurrentHP -= damage;
                
            }
            else
            {
                _playerCurrentHP = 0;
            }
            _playerHpBar.style.width = _playerCurrentHP / _playerMaxHP;
            _playerHpText.text = $"{_playerCurrentHP / _playerMaxHP}";
        }
        else
        {
            if(_enemyCurrentHP - damage < 0)
            {
                _enemyCurrentHP -= damage;
                
            }
            else
            {
                _enemyCurrentHP = 0;
            }
            _enemyHpBar.style.width = _enemyCurrentHP / _enemyMaxHP;
            _enemyHpText.text = $"{_enemyCurrentHP} / {_enemyMaxHP}";
        }
    }


    public void OnButton(PartSO so)
    {
        StartCoroutine(Corutine(so));
    }

    public IEnumerator Corutine(PartSO so)
    {
        // 이거 다 서버로 바꿔야됨
        SetPanel(); // 켜짐
        PartsBtnSetting(true);
        yield return new WaitForSeconds(0.1f);

        int rand = UnityEngine.Random.Range(0, 5);

        _panel.text = "대기중";
        yield return new WaitForSeconds(0.3f);
        bool t = SpeedReturn();
        yield return StartCoroutine(Fight(t, so, rand));
        yield return StartCoroutine(Fight(!t, so, rand));

        _panel.text = "대기중";
        yield return new WaitForSeconds(0.3f);
        SetPanel();
        //_atkBtn.AddToClassList("on");
        //_surrenBtn.AddToClassList("on");
        //_skipBtn.AddToClassList("on");
    }

    public IEnumerator Fight(bool t, PartSO so, int rand)
    {
        if (t == true)
        {
            _panel.text = $"나의 턴!!!";
            yield return new WaitForSeconds(0.5f);
            _panel.text = so.Daesa;
            _enemyRobot._statues.HP -= (int)(_robot._statues.ATK * so.Count);

            _enemyHpBar.style.scale = new StyleScale(new Scale(new Vector3(Mathf.Lerp(0f,1f, _enemyRobot._statues.HP/ _enemyRobot.MaxHP), 1, 0)));
            _enemyHpText.text = $"{_enemyRobot._statues.HP} / {_enemyRobot.MaxHP}";
            yield return new WaitForSeconds(1f);
            //if(so.clips != null)
            {
                SetPanel(); // 켜짐
                _robot.GetComponent<AnimationBind>().AnimationChange(so.clips);



                yield return new WaitUntil(() => _robot.GetComponent<AnimationBind>().EndAnim());

                SetPanel();
            }
            //else
            //{
            //    _panel.text = $"지정된 에니메이션이 없습니다";
            //    yield return new WaitForSeconds(1f);
            //}
           
            _panel.text =
                $"{_robot.name}은 {_enemyRobot.name}에게 {_robot._statues.ATK * so.Count}의 피해를 입혔다. ( 적HP : { _enemyRobot._statues.HP} )";

            if (_enemyRobot._statues.HP <= 0)
            {
                yield return new WaitForSeconds(1.5f);
                _panel.text = $"나의 승리..!";
                yield return new WaitForSeconds(1.5f);
                LoadManager.LoadScene(SceneEnum.Menu);
            }
        }
        else
        {
            if (_enemyRobot.ReturnParts((PartBaseEnum)rand) != null)
            {
                _panel.text = $"적의 턴!!!";
                yield return new WaitForSeconds(0.5f);
                _panel.text = _enemyRobot.ReturnParts((PartBaseEnum)rand).Daesa;
                _robot._statues.HP -= (int)(_enemyRobot._statues.ATK * _enemyRobot.ReturnParts((PartBaseEnum)rand).Count);
                _playerHpBar.style.scale = new StyleScale(new Scale(new Vector3(Mathf.Lerp(0f, 1f, _robot._statues.HP / _robot.MaxHP), 1, 0)));
                _playerHpText.text = $"{_robot._statues.HP} / {_robot.MaxHP}";
                yield return new WaitForSeconds(1f);
                //if (so.clips != null)
                {
                    SetPanel(); // 켜짐
                    _enemyRobot.GetComponent<AnimationBind>().AnimationChange(so.clips);



                    yield return new WaitUntil(() => _enemyRobot.GetComponent<AnimationBind>().EndAnim());

                    SetPanel();
                }
                //else
                //{
                //    _panel.text = $"지정된 에니메이션이 없습니다";
                //    yield return new WaitForSeconds(1f);
                //}


                _panel.text =
                    $"{_enemyRobot.name}은 {_robot.name}에게 {_enemyRobot._statues.ATK * _enemyRobot.ReturnParts((PartBaseEnum)rand).Count}의 피해를 입혔다. ( 나의HP : {_robot._statues.HP} )";
            }
            else
            {
                _panel.text = $"적의 턴!!!";
                yield return new WaitForSeconds(0.5f);
                _panel.text = "적이 아직 데이터를 가지고 있지 않습니다.";
                _robot._statues.HP -= (int)(_enemyRobot._statues.ATK * 1);

                _playerHpBar.style.scale = new StyleScale(new Scale(new Vector3(Mathf.Lerp(0f, 1f, _robot._statues.HP / _robot.MaxHP), 1, 0)));
                _playerHpText.text = $"{_robot._statues.HP} / {_robot.MaxHP}";
                yield return new WaitForSeconds(1f);
                //if (so.clips != null)
                {
                    SetPanel(); // 켜짐
                    _enemyRobot.GetComponent<AnimationBind>().AnimationChange(so.clips);



                    yield return new WaitUntil(() => _enemyRobot.GetComponent<AnimationBind>().EndAnim());

                    SetPanel();

                }
                //else
                //{
                //    _panel.text = $"지정된 에니메이션이 없습니다";
                //    yield return new WaitForSeconds(1f);
                //}


                _panel.text =
                    $"{_enemyRobot.name}은 {_robot.name}에게 {_enemyRobot._statues.ATK}의 피해를 입혔다. ( 나의HP : {_robot._statues.HP} )";
            }

            if (_robot._statues.HP <= 0)
            {
                yield return new WaitForSeconds(1.5f);
                _panel.text = $"HP : {_robot._statues.HP} 적의 승리..";
                yield return new WaitForSeconds(1.5f);
                LoadManager.LoadScene(SceneEnum.Menu);

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

    /**스킬이 등장해야 한다면 true 사라져야 한다면 false**/
    public void PartsBtnSetting(bool isAppear)
    {
        if(isAppear)
        {
            
            partsbtnGroup.RemoveFromClassList("off");
        }
        else
        {
            partsbtnGroup.AddToClassList("off");
        }
    }

    /**HP가 등장해야 한다면 true 사라져야 한다면 false**/
    public void HPSetting(bool isAppear)
    {
        if(isAppear)
        {
            _hpPanel.RemoveFromClassList("off");
            _enemyHpPanel.RemoveFromClassList("off");
        }
        else
        {
            _hpPanel.AddToClassList("off");
            _enemyHpPanel.AddToClassList("off");
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
        // 스킵
        SelectSkillForServer(-1); // -1은 스킵임
        // StartCoroutine(Skip());
    }

    IEnumerator Skip()
    {
        //OnWarning();
        SetPanel(); // 꺼짐
        //SetPartsBtn();
        //_atkBtn.RemoveFromClassList("on");
        //_surrenBtn.RemoveFromClassList("on");
        //_skipBtn.RemoveFromClassList("on");
        yield return new WaitForSeconds(0.1f);

        int rand = UnityEngine.Random.Range(0, 5);

        _panel.text = "로딩중..";
        yield return new WaitForSeconds(0.3f);
        _panel.text = "로딩중....";
        yield return new WaitForSeconds(0.3f);
        _panel.text = "로딩중......";
        yield return new WaitForSeconds(0.3f);
        _panel.text = $"나의 턴은 스킵되었다  ( 적HP : { _enemyRobot._statues.HP} )";
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Fight(false, null, rand));

        _panel.text = "로딩중....";
        yield return new WaitForSeconds(0.3f);
        _panel.text = "로딩중....";
        yield return new WaitForSeconds(0.3f);
        _panel.text = "로딩중..";
        yield return new WaitForSeconds(0.3f);
        SetPanel();
        //_atkBtn.AddToClassList("on");
        //_surrenBtn.AddToClassList("on");
        //_skipBtn.AddToClassList("on");


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

    

    ///////////// 서버 //////////////
    private void ActiveControl(LitJson.JsonData _ = null) {
        //_atkBtn.AddToClassList("on");
        //_surrenBtn.AddToClassList("on");
        //_skipBtn.AddToClassList("on");

        if (!onPanel) return;
        SetPanel();
        PartsBtnSetting(true);
    }
    private void SelectSkillForServer(int part) {
        print("part");
        print(part);
        NetworkCore.Send("ingame.selectSkill", part);

        _panel.text = "스킬을 선택했습니다. 다른 플레이어 기다리는중...";
        //_atkBtn.RemoveFromClassList("on");
        //_surrenBtn.RemoveFromClassList("on");
        //_skipBtn.RemoveFromClassList("on");
        SetPanel();
        PartsBtnSetting(false);
    }
    
    public class PVP_GameResult {
        public bool my;
        public string attacker;
        public string hitter;
        public string soid;
        public bool answer;
        public int power;
        public int health;
        public string why;
    }
    private void ServerGameResult(LitJson.JsonData data) {
        StartCoroutine(ServerGameResult_Co(data));
    }

    private void ServerGameDestory(LitJson.JsonData name) {
        StartCoroutine(nameof(ServerGameDestory_Co), (string)name);
    }

    IEnumerator ServerGameDestory_Co(string name) {
        _panel.text = name + "님이 탈주하였습니다.";
        //_atkBtn.RemoveFromClassList("on");
        //_surrenBtn.RemoveFromClassList("on");
        //_skipBtn.RemoveFromClassList("on");
        if (!onPanel)
            SetPanel();
        // if (onPartsPanel)
        //     SetPartsBtn();
        
        yield return new WaitForSeconds(1.5f);
        LoadManager.LoadScene(SceneEnum.Menu);
    }

    IEnumerator ServerGameResult_Co(LitJson.JsonData data) {
        bool disableControl = false;
        for (int i = 0; i < 2; i++)
        {
            var result = LitJson.JsonMapper.ToObject<PVP_GameResult>(data[i].ToJson());
            
            if (result.answer == true) {
                _panel.text = result.my ? "나의 턴" : "적의 턴";
                yield return new WaitForSeconds(1f);

                // 애니메이션
                SetPanel();
                var SO = _SOserver.ReturnSO(result.soid);
                (result.my ? _robot : _enemyRobot).GetComponent<AnimationBind>().AnimationChange(SO.clips);
                yield return new WaitUntil(() => (result.my ? _robot : _enemyRobot).GetComponent<AnimationBind>().EndAnim());
                SetPanel();

                SetHPValue(!result.my, result.power);
                _panel.text =
                    $"{result.attacker}은 {result.hitter}에게 {result.power}의 피해를 입혔다. ( {(result.my ? "적" : "나")}의HP : {result.health} )";
                yield return new WaitForSeconds(3f);

                if (result.why == "domiNotHealthEvent") {
                    _panel.text = result.my ? "나의 승리!!" : "적의 승리..";
                    disableControl = true;
                }

            } else if (result.why == "domiNotHealthEvent") {
                _panel.text = result.my ? "적의 승리.." : "나의 승리!!";
                disableControl = true;
            } else if (result.why == "domiSkipEvent") {
                _panel.text = (result.my == true ? "나" : "적") + "의 턴은 스킵되었다.";
                yield return new WaitForSeconds(1f);
            } else {
                _panel.text = (result.my ? "" : "적이 ") + result.why;
                yield return new WaitForSeconds(1.5f);
            }
        }

        if (disableControl) {
            yield return new WaitForSeconds(1.5f);
            LoadManager.LoadScene(SceneEnum.GameEnd);
        }
        else ActiveControl();
    }

    public void SetSkillButton(PartSO[] parts, int[] cools) {
        for (int i = 0; i < 5; i++)
        {
            partsbtns[i] = _root.Q<Button>($"{partsClass[i]}btn");
            int fuckCsharp = i;
            partsbtns[i].clicked += () => {
                if (cools[fuckCsharp] > (Time.time - partbtncools[fuckCsharp])) {
                    Debug.LogWarning( "아직 쿨타임이 지나지 않았습니다. 남은시간: "+ (cools[fuckCsharp] - (Time.time - partbtncools[fuckCsharp])) + "초" );
                    return;
                }
                partbtncools[fuckCsharp] = Time.time;
                SelectSkillForServer(fuckCsharp);
            };
            if (parts[i] != null)
            {
                partsbtns[i].Q<Label>("Text").text = parts[i].names;
                Debug.Log(parts[i].names);
                partsbtns[i].Q<VisualElement>("Image").style.backgroundImage = new StyleBackground(parts[i].SkillImage);
            }
        }
    }
}
