using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset _partsPrefab;
    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _haveItemListElement;
    private Label _partsInfo;
    private string[] _statuses = { "Atk", "HP", "Def", "Speed", "Normal", "Unique", "MasterPiece"};
    private Label[] _statusTxt = new Label[4];
    private Button[] _selectbtn = new Button[3];


    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    public void RefreshList()
    {
        
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _haveItemListElement = _root.Q<ScrollView>("HaveItemList");
        _partsInfo = _root.Q<Label>("PartsInfo");

        for(int i = 0; i < _statuses.Length; i++)
        {
            if(i < 4)
            {
                _statusTxt[i] = _root.Q<Label>($"{_statuses[i]}Status");
            }
            else
            {
                _selectbtn[i] = _root.Q<Button>($"{_statuses[i]}SelectBtn");
            }
        }
    }
}
