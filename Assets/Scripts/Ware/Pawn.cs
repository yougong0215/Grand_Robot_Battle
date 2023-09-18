using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pawn : WareBase
{
    private bool isFirst = true;

    public override void LookCanMoveBlock()
    {
        int max = 1;
        if (isFirst)
        {
            max = 2;
            isFirst = false;
        }
            

        int mark = CurrentPos[1] - '0';

        for(int i = 0; i < max; i++)
        {
            if (MapManager.Instance.MapDataParent.Find($"{CurrentPos[0]}{mark + i + 1}") == null)
                continue;
            Transform selectTrm =
            MapManager.Instance.MapDataParent.Find($"{CurrentPos[0]}{mark + i + 1}");
            _blockMarkSpawner.MarkSpawn(transform, selectTrm, true, selectTrm.name);
        }
    }
}
