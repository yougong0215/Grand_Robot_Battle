using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillSO/Body/MegaAnchor")]
public class MegaAnchor : PartSkillSO
{
    protected override void Start()
    {
        //throw new System.NotImplementedException();
    }

    protected override IEnumerator UseingSKill()
    {
        yield return null;
       //_me._statues.HP = (int)(_me._statues.HP + );
        _pvp.SetHPValue(true, -(_me.MaxHP * 0.2f));
        _pvp.SetText("더 단단해 졌다 ( HP 20% 회복 )");
        isEnd = true;
    }

}
