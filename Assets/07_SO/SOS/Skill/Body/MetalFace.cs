using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillSO/Body/MetalFace")]
public class MetalFace : PartSkillSO
{
    protected override void Start()
    {

    }

    protected override IEnumerator UseingSKill()
    {
        _me._statues.ATK = _me._statues.ATK * 1.1f;
        _pvp.SetText("강력해졌다 ( 공격력 10%증가 )");
        yield return new WaitForSeconds(1f);
        isEnd = true;
    }

}
