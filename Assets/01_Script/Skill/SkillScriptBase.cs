using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;


//[CreateAssetMenu(menuName = "SO/SkillScriptBase,")]
public abstract class SkillScriptBase : MonoBehaviour
{
    [SerializeField] Statues _statues;
    [SerializeField] protected Stat SkillValue = new Stat();

    protected bool _skillMotionEnd = false;


    /// <summary>
    /// ����� �ɲ�
    /// 1.�ִϸ��̼�����
    /// 2.���ݷ�����
    /// </summary>
    public abstract int Skill(ref Stat st, ref VSPlayer Enemy);

    public abstract IEnumerator corutine(Stat st, VSPlayer Enemy);

    public bool SkillMotionEnd()
    {
        return _skillMotionEnd;
    }
    public void Set()
    {
        _skillMotionEnd = false;
    }

}
