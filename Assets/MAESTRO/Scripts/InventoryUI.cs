using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public enum RatingType
{
    Normal,
    Unique,
    MasterPiece
}

class PuzzleItem
{
    public Texture2D texture;
    public int count;
    public string ID;
    public PuzzleItem(Texture2D tex, int num, string id)
    {
        texture = tex;
        count = num;
        ID = id;
    }
}

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset _partsPrefab;
    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _haveItemListElement;
    private VisualElement _partsImage;
    private Label _partsName;

    private string[] _statuses = { "Atk", "HP", "Def", "Speed", "Normal", "Unique", "MasterPiece"};
    private Label[] _statusTxt = new Label[4];
    private Button[] _selectbtn = new Button[3];

    [SerializeField] private List<PuzzleItem> _puzzleItemList = new List<PuzzleItem>();
    [SerializeField] private List<PartSO> _partsItemList = new List<PartSO>();

    private VisualElement _errorPanel;
    private Label _errorTxt;
    private Button _errorAcceptBtn;

    private PartSO _selectPartItem;
    GetServerToSO _SO;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        _SO = GetComponent<GetServerToSO>();
    }

    public void AddPuzzleItem(string id, int count)
    {
        Texture2D tex = new Texture2D(0, 0);
        string path = $"Assets/MAESTRO/PartsPuzzleItem/{id}.png";
        byte[] byteTex = File.ReadAllBytes(path);
        tex.LoadImage(byteTex);
        PuzzleItem pz = new PuzzleItem(tex, count, id);
        _puzzleItemList.Add(pz);

        RefreshList();
    }

    public void ClearList() {
        _puzzleItemList.Clear();
    }

    public void RefreshList()
    {
        _haveItemListElement.Clear();

        foreach(PuzzleItem pi in _puzzleItemList)
        {
            VisualElement ve = _partsPrefab.Instantiate().Q<VisualElement>("ItemBase");
            ve.RegisterCallback<ClickEvent>(PartsSelect);
            Label count = ve.Q<Label>("Count");
            ve.name = pi.ID;
            ve.style.backgroundImage = pi.texture;
            count.text = pi.count.ToString();

            _haveItemListElement.Add(ve);
        }
    }

    private void PartsSelect(ClickEvent evt)
    {
        VisualElement ve = evt.target as VisualElement;
        string id = ve.name;

        PartSO SO_data = _SO.ReturnSO(id);
        
        _selectPartItem = SO_data;
        _partsImage.style.backgroundImage = _selectPartItem.Sprite.texture;
        _partsName.text = _selectPartItem.SOname;
        _statusTxt[0].text = _selectPartItem.Statues.ATK.ToString();
        _statusTxt[1].text = _selectPartItem.Statues.HP.ToString();
        _statusTxt[2].text = _selectPartItem.Statues.DEF.ToString();
        _statusTxt[3].text = _selectPartItem.Statues.SPEED.ToString();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _haveItemListElement = _root.Q<ScrollView>("HaveItemList");
        _partsImage = _root.Q<VisualElement>("PartsImage");
        _partsName = _root.Q<Label>("SelectPartsName");

        _errorPanel = _root.Q<VisualElement>("ErrorPanel");
        _errorAcceptBtn = _root.Q<Button>("ErrorAcceptBtn");
        _errorTxt = _root.Q<Label>("ErrorTxt");
        _errorAcceptBtn.clicked += () => ActiveErrorPanel(false, null);

        for(int i = 0; i < _statuses.Length; i++)
        {
            if(i < 4)
            {
                _statusTxt[i] = _root.Q<Label>($"{_statuses[i]}Status");
            }
            else
            {
                _selectbtn[i-4] = _root.Q<Button>($"{_statuses[i]}SelectBtn");
            }
        }

        _selectbtn[0].clicked += () => ClickMakeBtn(RatingType.Normal);
        _selectbtn[1].clicked += () => ClickMakeBtn(RatingType.Unique);
        _selectbtn[2].clicked += () => ClickMakeBtn(RatingType.MasterPiece);
    }

    public void ActiveErrorPanel(bool active, string errorInfo)
    {
        _errorTxt.text = errorInfo;
        if(active)
        {
            _errorPanel.RemoveFromClassList("off");
        }
        else
        {
            _errorPanel.AddToClassList("off");
        }
    }

    struct MakePuzzel {
        public string part;
        public int grade;
    }
    private void ClickMakeBtn(RatingType ratingType)
    {
        NetworkCore.Send("puzzel.createParts", new MakePuzzel() {
            part = _selectPartItem.ToString().Replace(" (PartSO)", ""),
            grade = (int)ratingType
        });
    }
}
