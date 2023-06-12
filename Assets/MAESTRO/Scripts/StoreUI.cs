using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StoreUI : MonoBehaviour
{
    UIDocument _doc;

    private List<VisualElement> _itemList = new List<VisualElement>();
    [SerializeField] private VisualTreeAsset _item;
    private Label _name;
    private Label _price;
    private VisualElement _image;
    
    private VisualElement _root;
    private VisualElement _up;
    private VisualElement _down;
    private Label _timeTxt;
    private Button _resetBtn;

    private VisualElement _resetWarnPanel;
    private Button _yesBtn;
    private Button _noBtn;
    private Label _warnText;

    private VisualElement _errorPanel;
    private Button _okBtn;
    private Label _errorText;

    private float _necessaryGem = 20;
    private int _resetCount = 3;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();

        /* 이 스크립트에서 해줘야 하는 추가적인 작업 : 
         * 1. 아이템 SO정보 랜덤으로 받아서 넣기
         * 
         * 2. 현실 시간 타이머에 반영해서 날짜가 변경됐다면 상점 리셋해주기 (Resetitem 함수 사용)
         *      + _resetCount 변수도 3으로 초기화 해주기
         *      
         * 3. 현재 젬의 상태를 받아서 젬이 부족하면 젬이 부족하다고 메세지 패널을 띄우기
         *    (OnErrorPanel 함수 사용)
          */
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _up = _root.Q<VisualElement>("UP");
        _down = _root.Q<VisualElement>("DOWN");
        _timeTxt = _root.Q<Label>("Time");
        _resetBtn = _root.Q<Button>("ResetBtn");
        _warnText = _root.Q<Label>("txt");

        _resetWarnPanel = _root.Q<VisualElement>("ResetWarnPanel");
        _yesBtn = _root.Q<Button>("YesBtn");
        _noBtn = _root.Q<Button>("NoBtn");

        _errorPanel = _root.Q<VisualElement>("ErrorPanel");
        _errorText = _root.Q<Label>("Errortxt");
        _okBtn = _root.Q<Button>("OkBtn");

        _resetBtn.clicked += UseGemOkPanel;
        _yesBtn.clicked += ResetItem;
        _noBtn.clicked += Cancle;
        _okBtn.clicked += ExitErrorPanel;
    }

    private void OnErrorPanel(string sentence)
    {
        _errorPanel.AddToClassList("on");
        _errorText.text = sentence;
    }

    private void ExitErrorPanel()
    {
        _errorPanel.RemoveFromClassList("on");
    }

    private void UseGemOkPanel()
    {
        if(!_resetWarnPanel.ClassListContains("on"))
        {
            _resetWarnPanel.AddToClassList("on");
        }
    }

    private void Cancle()
    {
        _resetWarnPanel.RemoveFromClassList("on");
    }

    private void ResetItem()
    {
        if(_resetCount != 0)
        {
            foreach (VisualElement item in _itemList)
            {
                item.RemoveFromHierarchy();
            }
            _necessaryGem += 20;
            _resetCount--;
            Cancle();
            OrderItem();
            return;
        }
        OnErrorPanel("금일 갱신 횟수를 모두 사용했습니다.");
        Cancle();
    }

    private void OrderItem()
    {
        for(int i = 0; i < 6; i++)
        {
            VisualElement eleItem = _item.Instantiate();
            _name = eleItem.Q<Label>("Name");
            _price = eleItem.Q<Label>("Value");
            _image = eleItem.Q<VisualElement>("Image");

            // so 데이터로 이름, 가격, 이미지 셋팅하기

            if(i < 3)
            {
                _up.Add(eleItem);
            }
            else
            {
                _down.Add(eleItem);
            }
            _itemList.Add(eleItem);
        }
    }
}
