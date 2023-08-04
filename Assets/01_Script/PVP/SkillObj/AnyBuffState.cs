using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnyBuffState : AnyCommonAction
{
    [SerializeField]
    Stat NormalBuff;
    float BuffTime = 0f;

    

    protected override void FixedState()
    {
    }

    protected override void UpdateState()
    {
    }
    private IEnumerator Start()
    {
        if(fsm.TryGetComponent<RobotStatues>(out RobotStatues rt))
        {
            Debug.LogWarning("���⼭ ���� UI�߻�");
            rt.AddBuff(NormalBuff);
            yield return new WaitForSeconds(BuffTime);
            rt.RemoveBuff(NormalBuff);
        }
        else
        {
            yield return null;
            Debug.LogError("�κ��� �ƴ�");
        }


        Destroy();
    }
}
