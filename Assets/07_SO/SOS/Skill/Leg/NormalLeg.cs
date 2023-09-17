using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SkillSO/Normal/NormalLeg")]
public class NormalLeg : PartSkillSO
{
    protected override void Start()
    {
    }

    protected override IEnumerator UseingSKill()
    {
        _me._statues.SPEED = _me._statues.SPEED + 10;
        _pvp.SetHPValue(_enemy, (_me._statues.ATK * _enemy._statues.PercentDef) * 1.2f);
        _pvp.SetText($"{(_me._statues.ATK * _enemy._statues.PercentDef) * 1.2f}의 데미지를 주었다.");
        yield return new WaitForSeconds(0.8f);
        isEnd = true;
    }
}
