using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillSO/Right/NormalRightSword")]
public class NormalRightSword : PartSkillSO
{
    protected override void Start()
    {
    }

    protected override IEnumerator UseingSKill()
    {
        for (int i = 0; i < EventTime.Count; i++)
        {
            if (i == 0)
                yield return new WaitForSeconds(EventTime[i]);
            else
                yield return new WaitForSeconds(EventTime[i] - EventTime[i - 1]);
            _pvp.SetHPValue(_enemy, _me._statues.ATK * _enemy._statues.PercentDef * 1.2f);
            _pvp.SetText($"콱! 적에게 {_me._statues.ATK * _enemy._statues.PercentDef * 1.2f}의 데미지를 주었다.");
        }
        isEnd = true;
    }
}
