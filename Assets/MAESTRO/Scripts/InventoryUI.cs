using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _haveItemList;
    private VisualElement[] _itemBases = new VisualElement[12];
    private VisualElement _selectPartsImage;
    private Label _selectPartsName;
    private Label _partsInfo;
    private string[] _statuses = { "Atk", "HP", "Def", "Speed", "Normal", "Unique", "MasterPiece"};
    private Label[] _statusTxt = new Label[4];
    private Button[] _selectbtn = new Button[3]; 

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        _haveItemList = _root.Q<VisualElement>("HaveItemList");
        for(int i = 0; i < 12; i++)
        {
            _itemBases[i] = _haveItemList[i];
        }
        _selectPartsImage = _root.Q<VisualElement>("PartsImage");
        _selectPartsName = _root.Q<Label>("SelectPartsName");
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
