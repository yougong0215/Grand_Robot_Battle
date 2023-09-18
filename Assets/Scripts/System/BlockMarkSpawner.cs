using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockMarkSpawner : MonoBehaviour
{
    [SerializeField] private List<BlockMark> _markList = new List<BlockMark>();
    [SerializeField] private BlockMark _blockMarkPrefab;
    private BlockMark _selectBlockMark;
    private WareInfoRemember _wInfo;
    private ClickObserver _wareCObserver;

    private void Awake()
    {
        _wInfo = GameObject.Find("WareCollocateMaster").GetComponent<WareInfoRemember>();
        _wareCObserver = GameObject.Find("WareClickObserver").GetComponent<ClickObserver>();
    }

    public void MarkSpawn(Transform wareTrans, Transform toMoveTrm, bool isUsingWare, string mapID)
    {
        _selectBlockMark = Instantiate(_blockMarkPrefab);
        _selectBlockMark.MarkingID = mapID;
        _selectBlockMark.transform.position = new Vector3(wareTrans.position.x, 10, wareTrans.position.z);
        _selectBlockMark.transform.DOMove(new Vector3(toMoveTrm.position.x, 10, toMoveTrm.position.z), 0.3f);

        _selectBlockMark.MarkCickEvent = null;
        _selectBlockMark.MarkCickEvent += isUsingWare ? MoveWare : SelectCollocationPos;

        _markList.Add(_selectBlockMark);
    }

    public void RemoveALLBlockMark()
    {
        foreach(BlockMark bm in _markList)
        {
            Destroy(bm.gameObject);
        }
        _markList = new List<BlockMark>();
    }

    public void RemoveCanMoveBlockMark(Vector3 wareTrans)
    {
        foreach (BlockMark bm in _markList)
        {
            bm.transform.DOMove(new Vector3(wareTrans.x, 10, wareTrans.z), 0.3f);
            Destroy(bm.gameObject, 0.3f);
        }
        _markList = new List<BlockMark>();
    }

    private void MoveWare()
    {
        Transform trm = _wareCObserver.SelectMark.transform;
        WareManager.Instance.SelectWare.transform.
        DOMove(new Vector3(trm.position.x, 10, 
                           trm.transform.position.z), 0.4f);

        WareManager.Instance.SelectWare.CurrentPos = _wareCObserver.SelectID;
        WareManager.Instance.SelectWare.LookOutLine(true);
        WareManager.Instance.SelectWare.isSelected = false;
    }

    private void SelectCollocationPos()
    {
        WareManager.Instance.CreateWare(_wInfo.WType, _wInfo.IsBlack, _wareCObserver.SelectID);
    }
}
