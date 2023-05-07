using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneAttack : SkillScriptBase
{
    public override Stat Skill(ref VSPlayer Enemy)
    {
        Enemy.GetDamage(SkillValue.ATK);


        return SkillValue;
    }
}
