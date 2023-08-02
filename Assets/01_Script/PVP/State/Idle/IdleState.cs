using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : CommonState
{
    private void Start()
    {
        fsm.ChangeState(FSMState.Idle);
    }
    public override void EnterState()
    {
        Init?.Invoke();
        _animator.OnAnimationEventTrigger += EventAction;
        _animator.OnAnimationEndTrigger += EndAction;

    }

    public override void ExitState()
    {
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;
    }

    public override void UpdateState()
    {
        UpdateAction?.Invoke();
    }

}
