using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SkillSO/Head/AIBrain")]

public class AIBrain : PartSkillSO
{
    protected override void Start()
    {
        
    }

    protected override IEnumerator UseingSKill()
    {
        yield return new WaitForSeconds(1f);
        _pvp.SetText($"모든 스킬이 초기화 되었다.");
        _pvp.PartCoolRemove();
        isEnd = true;
    }

}
