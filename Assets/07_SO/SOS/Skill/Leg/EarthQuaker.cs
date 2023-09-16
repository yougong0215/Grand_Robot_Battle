using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillSO/Leg/EarthQuaker")]
public class EarthQuaker : PartSkillSO
{
    protected override void Start()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator UseingSKill()
    {
        yield return new WaitForSeconds(0.7f);
        if(_result != null)
        {
            _pvp.SetText($"{(_result.my == true ? "적" : "나")}의 방어력{(_result.my == true ? "의" : "이")} 30% {(_result.my == true ? "깎았다" : "깎였다")}.");
        }
        else
        {
            _pvp.SetText($"적의 방어력을 30% 깎았다.");
        }
        isEnd = true;
    }

}
