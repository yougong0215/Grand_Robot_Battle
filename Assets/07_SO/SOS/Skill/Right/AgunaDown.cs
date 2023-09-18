using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillSO/Right/AgunaDown")]

public class AgunaDown : PartSkillSO
{
    protected override void Start()
    {

    }

    protected override IEnumerator UseingSKill()
    {
        for (int i = 0; i < EventTime.Count; i++)
        {
            if (i < 0)
                yield return new WaitForSeconds(EventTime[i].EvtTime);
            else
                yield return new WaitForSeconds(EventTime[i].EvtTime - EventTime[i - 1].EvtTime);
            _pvp.SetHPValue(_enemy, _me._statues.ATK * _enemy._statues.PercentDef * 1.8f);
            _pvp.SetText($"콱! 적에게 {_me._statues.ATK * _enemy._statues.PercentDef * 1.8f}의 데미지를 주었다.");
        }
        isEnd = true;
    }

}
