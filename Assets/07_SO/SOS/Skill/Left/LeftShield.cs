using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillSO/Left/Shield")]
public class LeftShield : PartSkillSO
{
    protected override void Start()
    {
        //throw new System.NotImplementedException();
    }

    protected override IEnumerator UseingSKill()
    {
        _pvp.SetText("체력이 200 증가했다.");
        
        yield return new WaitForSeconds(0.8f);
        
        _pvp.SetHPValue(_me, -200);
        isEnd = true;
        //throw new System.NotImplementedException();
    }

}
