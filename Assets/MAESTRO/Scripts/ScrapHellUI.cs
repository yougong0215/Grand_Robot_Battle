using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrapHellUI : MonoBehaviour
{
    UIDocument _doc;
    VisualElement _root;
    bool onPanel;
    VisualElement _enemyInfoPanel;
    VisualElement _nextEnemyRobotProfile;
    VisualElement _myRobotHPValue;
    Button _runBtn;
    Button[] _enemyMark = new Button[5];
    VisualElement _myRobotImage;

    VisualElement _enemyRobotImage;
    Button _backBtn;
    VisualElement _enemyRobotHPValue;
    Button _startFightBtn;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void EnemyInfoPanelSetting()
    {
        if (onPanel)
            _enemyInfoPanel.AddToClassList("off");
        else
            _enemyInfoPanel.RemoveFromClassList("off");
        onPanel = !onPanel;

        // 패널 세팅
    }

    private void ShffleEnemyMark() // 적 순서 셔플
    {
        for(int i = 0; i < 5; i++)
        {
            _enemyMark[i] = _root.Q<Button>($"EnemyMark_{i + 1}");
            _enemyMark[i].clicked += EnemyInfoPanelSetting;
        }

        for(int i = 0; i < _enemyMark.Length; i++)
        {
            int random1 = Random.Range(0, _enemyMark.Length);
            int random2 = Random.Range(0, _enemyMark.Length);

            Button temp = _enemyMark[random1];
            _enemyMark[random1] = _enemyMark[random2];
            _enemyMark[random2] = temp;
        }
    }

    private void RunLogic()
    {

    }

    private void BattleStartLogic()
    {

    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _enemyInfoPanel = _root.Q<VisualElement>("EnemyInfoPanel");
        _myRobotImage = _root.Q<VisualElement>("MyRobotImage");
        _nextEnemyRobotProfile = _root.Q<VisualElement>("NextEnemyRobotProfile");
        _runBtn = _root.Q<Button>("RunBtn");
        _runBtn.clicked += RunLogic;
        _myRobotHPValue = _root.Q<VisualElement>("HPValue");
        _enemyRobotImage = _root.Q<VisualElement>("EnemyProfile");
        _backBtn = _root.Q<Button>("ExitBtn");
        _backBtn.clicked += EnemyInfoPanelSetting;
        _enemyRobotHPValue = _root.Q<VisualElement>("EnemyHPValue");
        _startFightBtn = _root.Q<Button>("FightStartbtn");
        _startFightBtn.clicked += BattleStartLogic;
    }
}
