using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareInfoRemember : MonoBehaviour
{
    [SerializeField] private GameObject _wsbPrefabs;
    WareSelectionBtn _wareSelectionBtn;
    private Transform _content;
    #region 기물 정보 저장

    [Header("기물 정보")]
    public WareType WType;
    public bool IsBlack;

    public void SelectWare(WareType wt, bool isBlack)
    {
        WType = wt;
        IsBlack = isBlack;
    }
    #endregion

    private void Awake()
    {
        _content = GameObject.Find("UICAN/WareSelectionGroup/SelectionBtnScroll/Viewport/Content").transform;
    }

    private void Start()
    {
        for(int i = 0; i < WareManager.Instance.WarePrefabs.Count; i++)
        {
            _wareSelectionBtn = (WareSelectionBtn)Instantiate(_wsbPrefabs, _content).GetComponent("WareSelectionBtn");
            if(i <= 5)
            {
                _wareSelectionBtn.WType = (WareType)i;
            }
            else
            {
                _wareSelectionBtn.WType = (WareType)i - 6;
                _wareSelectionBtn.IsBlack = true;
            }
            _wareSelectionBtn.name = $"{(WareType)i} SelectionBtn";
            _wareSelectionBtn.SettingValue();
        }
        UIManager.Instance.ActiveSelectionGroup(false);
    }
}
