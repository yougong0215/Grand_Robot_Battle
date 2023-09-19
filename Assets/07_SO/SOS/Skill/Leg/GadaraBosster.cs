using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SkillSO/Leg/GadraBooster")]
public class GadaraBosster : PartSkillSO
{
    protected override void Start()
    {
        
    }

    protected override IEnumerator UseingSKill()
    {
        _me._statues.SPEED = _me._statues.SPEED + 20;
        _pvp.SetText($"스피드가 20 빨라졌다.");
        yield return new WaitForSeconds(0.8f);
        isEnd = true;
    }

}
