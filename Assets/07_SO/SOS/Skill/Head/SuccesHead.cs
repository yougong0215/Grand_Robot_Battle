using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillSO/Head/SuccesHead")]
public class SuccesHead : PartSkillSO
{
    protected override void Start()
    {
        
    }

    protected override IEnumerator UseingSKill()
    {
        //throw new System.NotImplementedException();
        _pvp.SetText($"공격격력과 방어력이 5% 상승했다");
        yield return new WaitForSeconds(1f);
        isEnd = true;
    }

}
