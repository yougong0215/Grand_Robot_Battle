using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleToMoveAction : CommonAction
{
    Vector3 vec;
    JoyStick _joyStick;

    
    protected override void Init()
    {
        if(_joyStick==null)
        {
            _joyStick = FindManager.Instance.FindObject("PVPUI").GetComponent<JoyStick>();
        }
    }

    protected override void OnEndFunc()
    {
    }

    protected override void OnEventFunc()
    {
    }

    protected override void OnUpdateFunc()
    {
        if (_joyStick == null)
            return;

        vec = _joyStick.GetInputDirection();
        if (vec != Vector3.zero)
        {
            FSMMain.ChangeState(FSMState.Move);
        }
    }

}
