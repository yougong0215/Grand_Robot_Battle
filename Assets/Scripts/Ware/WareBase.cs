using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WareBase : MonoBehaviour
{
    private ClickObserver _wareClickObserver;
    public bool isSelected; 
    public string CurrentPos;
    [SerializeField] protected WareType _wareType;
    [SerializeField] protected bool _isBlack;
    protected BlockMarkSpawner _blockMarkSpawner;

    protected string _mapMarkCharData = "ABCDEFGHXXXXXX";
    protected int[] _mapMarkIntData = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

    [SerializeField] private Material[] _activeMatArr = new Material[2];
    [SerializeField] private Material[] _removeMatArr = new Material[2];

    public UnityAction ClickThisWareActiveEvent;
    public UnityAction ClickThisWareRemoveEvent;

    private void Awake()
    {
        _blockMarkSpawner = GameObject.Find("BlockMarkSpawner").GetComponent<BlockMarkSpawner>();
        _wareClickObserver = GameObject.Find("WareClickObserver").GetComponent<ClickObserver>();
    }

    private void Start()
    {
        ClickThisWareActiveEvent += LookCanMoveBlock;
        ClickThisWareActiveEvent += () => LookOutLine(false);

        ClickThisWareRemoveEvent += RemoveCanMoveBlock;
        ClickThisWareRemoveEvent += () => LookOutLine(true);
    }

    public void ClickEvent()
    {
        if (!isSelected)
            ClickThisWareActiveEvent?.Invoke();
        else
            ClickThisWareRemoveEvent?.Invoke();

        isSelected = !isSelected;
        _wareClickObserver.IsMoveWare = true;
    }

    public void LookOutLine(bool isRemove)
    {
        MeshRenderer _selectMR;
        for(int i = 0; i < transform.childCount; i++)
        {
            _selectMR = (MeshRenderer)transform.GetChild(i).GetComponent("MeshRenderer");
            _selectMR.materials = isRemove ? _removeMatArr : _activeMatArr;
        }

    }
    public abstract void LookCanMoveBlock();

    private void RemoveCanMoveBlock()
    {
        _blockMarkSpawner.RemoveCanMoveBlockMark(transform.position);
    }
}
