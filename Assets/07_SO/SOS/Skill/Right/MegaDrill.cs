using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SkillSO/Right/MegaDrill")]

public class MegaDrill : PartSkillSO
{
    protected override void Start()
    {
        //throw new System.NotImplementedException();
    }

    protected override IEnumerator UseingSKill()
    {
        for (int i = 0; i < EventTime.Count; i++)
        {
            if (i < 0)
                yield return new WaitForSeconds(EventTime[i].EvtTime);
            else
                yield return new WaitForSeconds(EventTime[i].EvtTime);
            _pvp.SetHPValue(_enemy, (_me._statues.ATK * _enemy._statues.PercentDef) * 0.07f);

        }
        _pvp.SetText($"총 {(_me._statues.ATK * _enemy._statues.PercentDef) * 0.07f * EventTime.Count}의 데미지를 주었다.");
        isEnd = true;
    }

}
