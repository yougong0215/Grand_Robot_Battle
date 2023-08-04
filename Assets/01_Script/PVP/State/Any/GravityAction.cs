using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAction : AnyCommonAction
{
    
    protected override void FixedState()
    {
        fsm.Move(new Vector3(0, - 9.8f, 0));
        Debug.LogWarning("나중에 stat으로 교체");
    }

    protected override void UpdateState()
    {
        
    }

}
