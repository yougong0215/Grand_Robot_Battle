using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillSO/Left/DXD9")]
public class DXD9Skill : PartSkillSO
{
    protected override void Start()
    {
        
    }

    protected override IEnumerator UseingSKill()
    {
        for (int i = 0; i < EventTime.Count; i++)
        {
            if (i < 0)
                yield return new WaitForSeconds(EventTime[i]);
            else
                yield return new WaitForSeconds(EventTime[i]);
            _pvp.SetHPValue(_enemy, (_me._statues.ATK * _enemy._statues.PercentDef) * 0.3f);

        }
        _pvp.SetText($"총 {(_me._statues.ATK  * _enemy._statues.PercentDef) * 0.3f * EventTime.Count}의 데미지를 주었다.");
        isEnd = true;
    }
}
