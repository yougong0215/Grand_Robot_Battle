using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAction : AnyCommonAction
{
    
    protected override void FixedState()
    {
        fsm.Move(new Vector3(0, - 9.8f, 0));
        Debug.LogWarning("���߿� stat���� ��ü");
    }

    protected override void UpdateState()
    {
        
    }

}
