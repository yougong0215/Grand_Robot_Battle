using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillAttack : SkillScriptBase
{
    public override int Skill(ref Stat st, ref VSPlayer Enemy)
    {
        Debug.Log($" :���� :=>{ Enemy} | {st.ATK + SkillValue.ATK}");

        Enemy.GetDamage(st.ATK + SkillValue.ATK);
        Enemy.StartCoroutine(corutine(st, Enemy));

        return st.ATK + SkillValue.ATK;
    }

    public override IEnumerator corutine(Stat st, VSPlayer Enemy)
    {
        VSGameController.Instance.TextPanel.SetActive(true);
        VSGameController.Instance.TMPPanel.text = $"{transform.parent.name}����! �Ⱑ�帱 �극��Ŀ!";
        yield return new WaitForSeconds(1.5f);

        VSGameController.Instance.TMPPanel.text = $"{st.ATK + SkillValue.ATK}�������� {Enemy.gameObject.name}���� ������!";
        yield return new WaitForSeconds(1.5f);

        _skillMotionEnd = true;
    }
}
