using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : WareBase
{
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

        int[] dx = { 0, 0, 1, -1, 1, 1, -1, -1 };
        int[] dy = { 1, -1, 0, 0, 1, -1, 1, -1 };

        for(int i = 0; i < dx.Length; i++)
        {
            Transform trm = mapDataP.Find($"{_mapMarkCharData[charidx + dx[i]]}{nummark + dy[i]}");
            if(trm != null)
            {
                _blockMarkSpawner.MarkSpawn(transform, trm, true, trm.name);
            }
        }
    }
} 
