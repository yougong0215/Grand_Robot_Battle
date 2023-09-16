using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillSO/Normal/NormalHead")]

public class NormalHead : PartSkillSO
{
    protected override void Start()
    {

    }

    protected override IEnumerator UseingSKill()
    {
        yield return new WaitForSeconds(1f);
        _me._statues.SPEED = _me._statues.SPEED + 20;

        _pvp.SetText($"스피드가 10 만큼 빨라졌다.");
        isEnd = true;
    }
}
