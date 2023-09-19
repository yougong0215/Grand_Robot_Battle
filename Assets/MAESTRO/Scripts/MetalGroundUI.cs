using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MetalGroundUI : MonoBehaviour
{
    [SerializeField] private int _winningCount;
    UIDocument _doc;
    VisualElement _root;

    VisualElement _myRobotImage;
    Button _myRobotChangeBtn;

    Label _winningCountTxt;

    Button _matchingStartBtn;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        NetworkCore.EventListener["ingame.load"] = MathFinded;
    }

    private void OnDestroy() {
        NetworkCore.EventListener.Remove("ingame.load");
    }

    public void PlusWinningCount(int value)
    {
        _winningCount += value;
        _winningCountTxt.text = _winningCount.ToString();
    }

    public void MatchingStart()
    {
        NetworkCore.Send("Match.Add", null);
        print("[Match] 플레이어 찾는중...");
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        _myRobotImage = _root.Q<VisualElement>("MyRobotImage");
        _myRobotChangeBtn = _root.Q<Button>("MyRobotChangeBtm");
        _winningCountTxt = _root.Q<Label>("Count");
        _matchingStartBtn = _root.Q<Button>("MatchingBtn");
        _matchingStartBtn.clicked += MatchingStart;
        _myRobotChangeBtn.clicked += () => LoadManager.LoadScene(SceneEnum.MakeRobot);
    }

    void MathFinded(LitJson.JsonData data) {
        print("[Match] 플레이어를 찾음. 배틀로 이동");
        SceneManager.LoadScene("PVP");
    }
}
