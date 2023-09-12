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
    private string[] _statuses = { "Atk", "HP", "Def", "Speed", "Normal", "Unique", "MasterPiece"};
    private Label[] _statusTxt = new Label[4];
    private Button[] _selectbtn = new Button[3];

    private HavingItemList _havingItemList;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        _havingItemList = GameObject.Find("ItemList").GetComponent<HavingItemList>();
    }

    public void MakeParts(RatingType rt)
    {
        _havingItemList.MakeParts(rt);
        RefreshList();
    }

    public void RefreshList()
    {
        _haveItemListElement.Clear();
        foreach(PartsPieceItem ppi in _havingItemList.HavingPPIList)
        {
            VisualElement ve = _partsPrefab.Instantiate().Q<VisualElement>("ItemBase");
            Label count = ve.Q<Label>("Count");
            ve.style.backgroundImage = ppi.PartSo.Sprite.texture;
            count.text = ppi.count.ToString();
            _haveItemListElement.Add(ve);
        }
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _haveItemListElement = _root.Q<ScrollView>("HaveItemList");

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
