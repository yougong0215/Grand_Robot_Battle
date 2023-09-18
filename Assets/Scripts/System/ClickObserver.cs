using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObserver : MonoBehaviour
{
    public string SelectID;
    public bool IsMoveWare;
    public BlockMark SelectMark;
    private Camera _mainCam;
    BlockMarkSpawner _blockMarkSpawner;

    private void Awake()
    {
        _mainCam = Camera.main;
        _blockMarkSpawner = GameObject.Find("BlockMarkSpawner").GetComponent<BlockMarkSpawner>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.TryGetComponent<WareBase>(out WareBase wb))
                {
                    wb.ClickEvent();
                }
            }

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.TryGetComponent<BlockMark>(out BlockMark bm))
                {
                    SelectMark = bm;
                    SelectID = bm.MarkingID;
                    bm.MarkCickEvent?.Invoke();
                    _blockMarkSpawner.RemoveALLBlockMark();
                }
            }
        }
    }
}
