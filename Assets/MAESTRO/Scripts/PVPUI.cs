using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Rendering;

public class PVP_GameResult
{
    public bool my;
    public string attacker;
    public string hitter;
    public string soid;
    public bool answer;
    public int power;
    public int health;
    public string why;
}
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
    int[] _playerCools;
    int[] _originCools;
    private PartSO[] soList;
    private bool onPartsPanel;
    private bool onPanel;
    private bool onwarning;

    [SerializeField]private RobotSettingAndSOList _robot;       // 임시방편
    [SerializeField] private RobotSettingAndSOList _enemyRobot;  // 임시방편
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
        
        
        _panel.text = "로딩중";
        SetPanel(true);
        //_atkBtn.RemoveFromClassList("on");
        //_surrenBtn.RemoveFromClassList("on");
        //_skipBtn.RemoveFromClassList("on");
        
        // 서버에게 준비가 되었다고 알림
        NetworkCore.Send("ingame.ready", StoryLoadResource.Instance.isIthave());
        if(!StoryLoadResource.Instance.isIthave())
        {
           
            PartsBtnSetting(true);
        }
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
    float _playerWid, _enemyWid;
    public void SetMaxHP(float playerHP, float enemyHP)
    {
        _playerMaxHP = _playerCurrentHP = playerHP;
        _enemyMaxHP = _enemyCurrentHP = enemyHP;
        _playerHpText.text = $"{_playerCurrentHP} / {_playerMaxHP}";
        _enemyHpText.text = $"{_enemyCurrentHP} / {_enemyMaxHP}";
        _playerWid = 400;//_playerHpBar.resolvedStyle.width;
        _enemyWid = 400;//_enemyHpBar.resolvedStyle.width;
        Debug.Log($"WIND : {_playerHpBar.resolvedStyle.width}");
    }

    public void SetHPValue(RobotSettingAndSOList _rot, float damage)
    {
        
        if(_rot == _robot)
        {
            

            _playerCurrentHP -= damage;
                
            
            if(_playerCurrentHP < 0 )
            {
                _playerCurrentHP = 0;
            }
            if(_playerCurrentHP > _playerMaxHP)
            {
                _playerCurrentHP = _playerMaxHP;
            }
            float t = _playerCurrentHP / _playerMaxHP;
            Debug.Log(t);
            _playerHpBar.style.width = Mathf.Lerp(0, _playerWid,_playerCurrentHP / _playerMaxHP);
            Debug.LogWarning(_playerWid);
                

            _playerHpText.text = $"{_playerCurrentHP} / {_playerMaxHP}";
            //Debug.Log($"DMG : {Mathf.Lerp(0, _playerWid,_playerCurrentHP / _playerMaxHP)}");
        }
        else
        {

                _enemyCurrentHP -= damage;
                
            
            if(_enemyCurrentHP < 0)
            {
                _enemyCurrentHP = 0;
            }
            _enemyHpBar.style.width = Mathf.Lerp( 0,_enemyWid, _enemyCurrentHP / _enemyMaxHP);
            _enemyHpText.text = $"{_enemyCurrentHP} / {_enemyMaxHP}";
        }
    }



    public IEnumerator AIGameLogic(PartSO so)
    {
        // 이거 다 서버로 바꿔야됨
        SetPanel(false);
        PartsBtnSetting(false);
        Debug.Log("로직 시작");
        bool t = SpeedReturn();


        yield return StartCoroutine(Fight(t, so));

        SetPanel(false);
        yield return StartCoroutine(Fight(!t, so));
        SetPanel(false);
        //SetPanel();
        Debug.Log("끝");
        //_atkBtn.AddToClassList("on");
        //_surrenBtn.AddToClassList("on");
        //_skipBtn.AddToClassList("on");
        //yield return new WaitForSeconds(1f);
        
        PartsBtnSetting(true);
    }

    public IEnumerator Fight(bool t, PartSO so = null)
    {

        yield return new WaitForSeconds(1f);
        if (t == true)
        {
            _panel.text = $"나의 턴!!!";
            SetPanel(true); // 켜짐
            yield return new WaitForSeconds(2f);
            _panel.text = so.Daesa;
            yield return new WaitForSeconds(2f);
            SetPanel(false); // 꺼짐

            so.skillSo.Init(this, _robot, _enemyRobot, so, _robot.GetComponent<AnimationBind>());
            so.skillSo._act?.Invoke();
            yield return new WaitUntil(() => so.skillSo.IsEnd());
            SetPanel(true);

            yield return new WaitForSeconds(2f);
            if (_enemyCurrentHP <= 0)
            {
                yield return new WaitForSeconds(1.5f);
                _panel.text = $"나의 승리..!";
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene((int)StoryLoadResource.Instance.NextScene());
            }
        }
        else
        {
            int rand = UnityEngine.Random.Range(0, 5);

            if (_enemyRobot.ReturnParts((PartBaseEnum)rand) != null)
            {
                
                PartSO _part = _enemyRobot.ReturnParts((PartBaseEnum)rand);
                _panel.text = $"적의 턴!!!";
                SetPanel(true); // 켜짐
                yield return new WaitForSeconds(2f);
                _panel.text = _part.Daesa;
                yield return new WaitForSeconds(2f);

                SetPanel(false); // 꺼짐

                _part.skillSo.Init(this, _enemyRobot, _robot, _part, _enemyRobot.GetComponent<AnimationBind>());
                _part.skillSo._act?.Invoke();
                yield return new WaitUntil(() => _part.skillSo.IsEnd());

                SetPanel(true);
                yield return new WaitForSeconds(2f);
            }
            else
            {
                Debug.LogWarning("Error : NO Data");
                yield return new WaitForSeconds(2f);

            }

            if (_playerCurrentHP <= 0)
            {
                yield return new WaitForSeconds(1.5f);
                _panel.text = $"적의 승리..";
                yield return new WaitForSeconds(1.5f);
                LoadManager.LoadScene(SceneEnum.Menu);

            }
        }

        //yield return new WaitForSeconds(2f);
    }

    public void PartCoolRemove()
    {
        _playerCools.ToList().ForEach((a) => { if (a > 0) a = 0; });
    }
    public bool SpeedReturn()
    {
        Debug.Log(_enemyRobot._statues);
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
        SetPanel(true); // 꺼짐
        //SetPartsBtn();
        //_atkBtn.RemoveFromClassList("on");
        //_surrenBtn.RemoveFromClassList("on");
        //_skipBtn.RemoveFromClassList("on");
        yield return new WaitForSeconds(0.1f);

        //int rand = UnityEngine.Random.Range(0, 5);
        _panel.text = $"나의 턴은 스킵되었다.";
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Fight(false));
        SetPanel(false);
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
        _panel.text = txt;
    }

    public void SetPanel(bool t)
    {
        if (t)
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

        //if (!onPanel) return;
        SetPanel(false);
        PartsBtnSetting(true);
    }
    private void SelectSkillForServer(int part) {
        print(part);
        NetworkCore.Send("ingame.selectSkill", part);

        _panel.text = "스킬을 선택했습니다. 다른 플레이어 기다리는중...";

        _playerCools.ToList().ForEach((a)=> {if(a>0) a--;});
        //_atkBtn.RemoveFromClassList("on");
        //_surrenBtn.RemoveFromClassList("on");
        //_skipBtn.RemoveFromClassList("on");
        SetPanel(false);
        PartsBtnSetting(false);
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
        //if (!onPanel)
            SetPanel(false);
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
            
            if (result.answer == true) 
            {
                _panel.text = result.my ? "나의 턴" : "적의 턴";
                SetPanel(true);
                yield return new WaitForSeconds(1f);

                // 애니메이션
                var SO = _SOserver.ReturnSO(result.soid);
                _panel.text = SO.Daesa;
                //(result.my ? _robot : _enemyRobot).GetComponent<AnimationBind>().AnimationChange(SO.clips);
                yield return new WaitForSeconds(2f);
                SetPanel(false);
                SO.skillSo.Init(this, (result.my ? _robot : _enemyRobot)
                , (result.my ?  _enemyRobot : _robot), SO, (result.my ? _robot : _enemyRobot).GetComponent<AnimationBind>(), result);

                SO.skillSo._act?.Invoke();
                yield return new WaitUntil(() => SO.skillSo.IsEnd());
                SetPanel(true);

                //SetHPValue(!result.my, result.power);
                //_panel.text =  $"{result.attacker}은 {result.hitter}에게 {result.power}의 피해를 입혔다. ( {(result.my ? "적" : "나")}의HP : {result.health} )";
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

        for(int i=0; i < soList.Count(); i++)
        {
            soList[i].skillSo.TurnEnd();

        }


        if (disableControl) {
            yield return new WaitForSeconds(1.5f);
            LoadManager.LoadScene(SceneEnum.GameEnd);
        }
        else ActiveControl();
    }

    public void SetSkillButton(PartSO[] parts, int[] cools) 
    {
        _playerCools = new int[5]{0,0,0,0,0};
        _originCools = cools;
        soList = parts;

        for (int i = 0; i < 5; i++)
        {
            
            partsbtns[i] = _root.Q<Button>($"{partsClass[i]}btn");
            int fuckCsharp = i;
            partsbtns[i].clicked += () => {
                Debug.Log("버튼 클릭!!!");
                if (_playerCools[fuckCsharp] > 0) {
                    Debug.LogWarning( $"아직 쿨타임이 지나지 않았습니다. 남은턴: {_playerCools[fuckCsharp]}" );
                    return;
                }
                partbtncools[fuckCsharp] = parts[fuckCsharp].Count;
                if(!StoryLoadResource.Instance.isIthave())
                {
                    Debug.Log("Selected 1");
                    SelectSkillForServer(fuckCsharp);
                }
                else
                {
                    Debug.Log("Selected 2");
                    StartCoroutine(AIGameLogic(parts[fuckCsharp]));
                }
            };
            if (parts[i] != null)
            {
                partsbtns[i].Q<Label>("Text").text = parts[i].names;
                Debug.Log(parts[i].names);
                partsbtns[i].Q<VisualElement>("Image").style.backgroundImage = new StyleBackground(parts[i].SkillImage);
            }
            else{
                partsbtns[i].Q<Label>("미장착").text = parts[i].names;
            }
        }
    }
}
