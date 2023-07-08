using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        
    }

    public void PlusWinningCount(int value)
    {
        _winningCount += value;
        _winningCountTxt.text = _winningCount.ToString();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        _myRobotImage = _root.Q<VisualElement>("MyRobotImage");
        _myRobotChangeBtn = _root.Q<Button>("MyRobotChangeBtm");
        _winningCountTxt = _root.Q<Label>("Count");
        _matchingStartBtn = _root.Q<Button>("MatchingBtn");
    }
}
