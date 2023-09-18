using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : WareBase
{
    public override void LookCanMoveBlock()
    {
        Transform trm = MapManager.Instance.MapDataParent;

        for (int i = 0; i < trm.childCount; i++)
        {
            string map = trm.GetChild(i).name;
            if((map.Contains(CurrentPos[0]) ||
                map.Contains(CurrentPos[1])) && map != CurrentPos)
            {
                _blockMarkSpawner.MarkSpawn(transform, trm.GetChild(i), true, map);
            }
        }
        
    }
}
