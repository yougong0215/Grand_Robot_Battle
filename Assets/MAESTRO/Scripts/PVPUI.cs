using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PVPUI : MonoBehaviour
{
    #region ÄÄÆ÷³ÍÆ®
    private UIDocument _uiDoc;
    private VisualElement _root;
    private VisualElement _panel;

    private Button _atkBtn;
    private Button _skipBtn;
    private Button _surrenBtn;

    private Button[] partsbtns = new Button[5];
    private string[] partsClass = { "LA", "RA", "LL", "RL", "H" };

    private bool onPartsPanel;
    private bool onPanel;
    #endregion

    private void Awake()
    {
        _uiDoc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _uiDoc.rootVisualElement;
        _panel = _root.Q<VisualElement>("Panel");
        _atkBtn = _root.Q<Button>("AttackBtn");
        _skipBtn = _root.Q<Button>("SkipBtn");
        _surrenBtn = _root.Q<Button>("SurrenderBtn");

        for(int i = 0; i < 4; i++)
        {
            partsbtns[i] = _root.Q<Button>($"{partsClass[i]}btn");
        }

        _atkBtn.clicked += SetPartsBtn;
        _skipBtn.clicked += UpSkipPanle;
        _surrenBtn.clicked += UpSurrenderPanel;
    }

    private void UpSkipPanle()
    {

    }

    private void SkipLogic()
    {

    }

    private void UpSurrenderPanel()
    {

    }

    private void SurrenderLogic()
    {

    }

    private void SetPanel()
    {
        if(!onPanel)
        {
            _panel.RemoveFromClassList("on");
        }
        else
        {
            _panel.AddToClassList("on");
        }

        onPanel = !onPanel;
    }

    private void SetPartsBtn()
    {
        if(!onPartsPanel)
        {
            for(int i = 0; i < 4; i++)
            {
                partsbtns[i].AddToClassList($"{partsClass[i]}");
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                partsbtns[i].RemoveFromClassList($"{partsClass[i]}");
            }

        }
        onPartsPanel = !onPartsPanel;
    }
}
