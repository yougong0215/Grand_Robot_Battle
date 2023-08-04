using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickMoveAction : CommonAction
{
    JoyStick _joyStick;
    protected override void Init()
    {
        _joyStick = FindManager.Instance.FindObject("PVPUI").GetComponent<JoyStick>();
        
    }

    protected override void OnEndFunc()
    {
    }

    protected override void OnEventFunc()
    {
    }

    protected override void OnUpdateFunc()
    {
        Vector2 vec = _joyStick.GetInputDirection();
        com.AnimationCon.SetMoveDirection(vec.normalized);

        if(vec == Vector2.zero)
        {
            FSMMain.ChangeState(FSMState.Idle);
        }


        FSMMain.Move(new Vector3(vec.x, 0, vec.y) * 50);
        Debug.LogWarning("나중에 stat으로 교체");
    }

}
