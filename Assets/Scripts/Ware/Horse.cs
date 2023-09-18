using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : WareBase
{
    int[] dx = { 1, -1, 1, -1, 2, -2, 2, -2 };
    int[] dy = { 2, 2, -2, -2, 1, 1, -1, -1 };

    public override void LookCanMoveBlock()
    {
        int nummark = CurrentPos[1] - '0';

        int charidx = 0; // 앞 글자 인덱스

        Transform mapDataP = MapManager.Instance.MapDataParent;

        for (int i = 0; i < 8; i++)
        {
            if (CurrentPos[0] == _mapMarkCharData[i])
            {
                charidx = i;
                break;
            }
        }
        for (int i = 0; i < 8; i++)
        {
            if(charidx + dx[i] > 7 || charidx + dx[i] < 0 || nummark + dy[i] > 8 || nummark + dy[i] < 1)
            {
                continue;
            }

            Transform selectTrans =
            mapDataP.Find($"{_mapMarkCharData[charidx + dx[i]]}{nummark + dy[i]}");
            _blockMarkSpawner.MarkSpawn(transform, selectTrans, true, selectTrans.name);
        }
    }
}
