using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class RewardItem
{
    public string ItemID;
    public int Count;
}

public class DailyCheck : MonoBehaviour
{
    bool canExit;
    [SerializeField] private VisualTreeAsset _checkMark;
    [SerializeField] private RewardItem[] rewardItems = new RewardItem[7];
    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _rewardsGroup;
    private VisualElement[] _rewardsArray = new VisualElement[7];
    private Button _acceptBtn;

    private Dictionary<int, RewardItem> _rewardDic = new Dictionary<int, RewardItem>();
    private RewardItem _selectItem;

    private void Awake()
    {
        canExit = false;
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _acceptBtn = _root.Q<Button>("accept-btn");
        _rewardsGroup = _root.Q<VisualElement>("reward-group");
        for(int i = 0; i < _rewardsArray.Length; i++)
        {
            _rewardsArray[i] = _rewardsGroup[i];
            _rewardDic.Add(i, rewardItems[i]);
        }

        _acceptBtn.clicked += () => StartCoroutine(ActivePanel(false, 0));
    }

    public void OnPanel(int dailyCount)
    {
        for(int i = 0; i < dailyCount - 1; i++)
        {
            VisualElement mark = _checkMark.Instantiate().Q<VisualElement>("check-mark");
            mark.style.width = new Length(100, LengthUnit.Percent);
            mark.style.height = new Length(100, LengthUnit.Percent);
            _rewardsArray[i].Add(mark);
        }
        _selectItem = rewardItems[dailyCount];
    }

    IEnumerator ActivePanel(bool isActive, int date)
    {
        if (isActive)
        {
            _root.style.display = DisplayStyle.Flex;
            yield return new WaitForSeconds(1);
            VisualElement mark = _checkMark.Instantiate().Q<VisualElement>("check-mark");
            _rewardsArray[date].Add(mark);
            mark.AddToClassList("down");
            yield return new WaitForSeconds(0.3f);
            InputReward();
            canExit = true;
        }
        else
        {
            if(canExit)
                _root.style.display = DisplayStyle.None;
        }
        

    }

    //서버를 호출
    public void InputReward()
    {

    }
}
