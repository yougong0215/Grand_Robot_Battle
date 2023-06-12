using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class GarageUI : MonoBehaviour
{
    UIDocument _doc;
    private VisualElement _root;
    private Button[] partsbtns = new Button[6];
    private string[] partstring = { "All", "Head", "Body", "LA", "RA", "Leg" };
    private VisualElement _backPanel;

    [SerializeField] private UnityEvent<string> partsBtnClicked;

    bool isEnforce;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _backPanel = _root.Q<VisualElement>("BackPanel");
        #region 존나 사연이 있습니다. 하...
        for (int i = 0; i < partsbtns.Length; i++)
        {
            partsbtns[i] = _root.Q<Button>($"{partstring[i]}Btn");
        }
        partsbtns[0].clicked += () => SubEvent("All");
        partsbtns[1].clicked += () => SubEvent("Head");
        partsbtns[2].clicked += () => SubEvent("Body");
        partsbtns[3].clicked += () => SubEvent("LA");
        partsbtns[4].clicked += () => SubEvent("RA");
        partsbtns[5].clicked += () => SubEvent("Leg");
        #endregion
    }

    private void SubEvent(string parts)
    {
        partsBtnClicked?.Invoke(parts);
    }
    
    public void ChangeMode()
    {
        if(!isEnforce)
        {
            _backPanel.AddToClassList("off");
        }
        else
        {
            _backPanel.RemoveFromClassList("off");
        }
        isEnforce = !isEnforce;
    }
}
