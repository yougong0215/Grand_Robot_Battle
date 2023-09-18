using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : ObjectManager
{
    public static UIManager Instance;
    [SerializeField] private GameObject _wareSelectionGroup;
    public override void SetInstance()
    {
        Instance = this;
    }

    public void ActiveSelectionGroup(bool isActive)
    {
        if (isActive)
            _wareSelectionGroup.transform.DOLocalMoveY(0, 0.5f);
        else
            _wareSelectionGroup.transform.DOLocalMoveY(-900, 0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ActiveSelectionGroup(true);
    }
}
