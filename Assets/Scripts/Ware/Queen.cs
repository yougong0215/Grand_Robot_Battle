using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : WareBase
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
        for (int i = -4; i <= 4; i++)
        {
            if (charidx + i >= 0)
            {
                if (mapDataP.Find($"{_mapMarkCharData[charidx + i]}{nummark + i}")
                && $"{_mapMarkCharData[charidx + i]}{nummark + i}" != CurrentPos)
                {
                    Transform trm
                    = mapDataP.Find($"{_mapMarkCharData[charidx + i]}{nummark + i}");
                    _blockMarkSpawner.MarkSpawn(transform, trm, true, trm.name);
                }
                if (mapDataP.Find($"{_mapMarkCharData[charidx + i]}{nummark - i}")
                && $"{_mapMarkCharData[charidx + i]}{nummark + i}" != CurrentPos)
                {
                    Transform trm
                    = mapDataP.Find($"{_mapMarkCharData[charidx + i]}{nummark - i}");
                    _blockMarkSpawner.MarkSpawn(transform, trm, true, trm.name);
                }
            }
        }

        for (int i = 0; i < mapDataP.childCount; i++)
        {
            string map = mapDataP.GetChild(i).name;
            if ((map.Contains(CurrentPos[0]) ||
                map.Contains(CurrentPos[1])) && map != CurrentPos)
            {
                _blockMarkSpawner.MarkSpawn(transform, mapDataP.GetChild(i), true, map);
            }
        }
    }
}
