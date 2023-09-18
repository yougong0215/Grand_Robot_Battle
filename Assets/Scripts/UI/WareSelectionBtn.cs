using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WareSelectionBtn : MonoBehaviour
{
    [SerializeField] private Image _wareImage;
    [SerializeField] private TextMeshProUGUI _wareName;
    public WareType WType;
    public bool IsBlack;

    public void SettingValue()
    {
        _wareImage.sprite = 
        IsBlack ? WareManager.Instance.WareSprites[(int)WType] : WareManager.Instance.WareSprites[(int)WType + 6];
        _wareName.text = IsBlack ? $"Black {WType}" : $"White {WType}";
    }

    public void DetectRangeConnector()
    {
        GameObject wcm = GameObject.Find("WareCollocateMaster");
        RangeSelecter rs = wcm.GetComponent<RangeSelecter>();
        WareInfoRemember wc = wcm.GetComponent<WareInfoRemember>();
        ClickObserver wo = GameObject.Find("WareClickObserver").GetComponent<ClickObserver>();

        rs.DetectRange();
        wc.SelectWare(WType, IsBlack);
        wo.IsMoveWare = false;
        UIManager.Instance.ActiveSelectionGroup(false);
    }
}
