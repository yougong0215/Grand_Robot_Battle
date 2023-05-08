using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneAttack : SkillScriptBase
{
    public override Stat Skill(ref VSPlayer Enemy)
    {
        Debug.Log(transform.parent.gameObject.name);
        Debug.Log(Enemy);
        Enemy.GetDamage(SkillValue.ATK);
        _skillMotionEnd = true;

        return SkillValue;
    }
}
