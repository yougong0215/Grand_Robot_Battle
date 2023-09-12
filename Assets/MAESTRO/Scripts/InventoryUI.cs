using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

class PuzzleItem
{
    public Texture2D texture;
    public int count;
    public PuzzleItem(Texture2D tex, int num)
    {
        texture = tex;
        count = num;
    }
}

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset _partsPrefab;
    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _haveItemListElement;
    private string[] _statuses = { "Atk", "HP", "Def", "Speed", "Normal", "Unique", "MasterPiece"};
    private Label[] _statusTxt = new Label[4];
    private Button[] _selectbtn = new Button[3];

    [SerializeField] private List<PuzzleItem> _puzzleItemList = new List<PuzzleItem>();

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    public void SetPuzzleList(string id, int count)
    {
        Texture2D tex = new Texture2D(0, 0);
        string path = $"Assets/MAESTRO/PartsPuzzleItem/{id}";
        byte[] byteTex = File.ReadAllBytes(path);
        tex.LoadImage(byteTex);
        PuzzleItem pz = new PuzzleItem(tex, count);
        _puzzleItemList.Add(pz);

        RefreshList();
    }

    public void RefreshList()
    {
        _haveItemListElement.Clear();

        foreach(PuzzleItem pi in _puzzleItemList)
        {
            VisualElement ve = _partsPrefab.Instantiate().Q<VisualElement>("ItemBase");
            ve.RegisterCallback<ClickEvent>(PartsSelect);
            Label count = ve.Q<Label>("Count");

            ve.style.backgroundImage = pi.texture;
            count.text = pi.count.ToString();

            _haveItemListElement.Add(ve);
        }
    }

    private void PartsSelect(ClickEvent evt)
    {

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
