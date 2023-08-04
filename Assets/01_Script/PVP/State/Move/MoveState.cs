using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : CommonState
{


    public override void EnterState()
    {
        Init?.Invoke();
        _animator.OnAnimationEventTrigger += EventAction;
        _animator.OnAnimationEndTrigger += EndAction;
        _animator.SetMoveAnimation(true);

    }

    public override void ExitState()
    {
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;
        _animator.SetMoveAnimation(false);
    }
    public override void AnimationEndTRG()
    {
    }
    public override void UpdateState()
    {
        UpdateAction?.Invoke();
    }
}
