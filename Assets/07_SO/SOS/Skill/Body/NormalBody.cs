using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillSO/Normal/NormalBody")]
public class NormalBody : PartSkillSO
{
    protected override void Start()
    {
        //throw new System.NotImplementedException();
    }

    protected override IEnumerator UseingSKill()
    {
        yield return null;
        _pvp.SetHPValue(_me, -(_me.MaxHP * 0.1f));
        _pvp.SetText("단단해 졌다 ( HP 10% 회복 )");
        isEnd = true;
    }
}
