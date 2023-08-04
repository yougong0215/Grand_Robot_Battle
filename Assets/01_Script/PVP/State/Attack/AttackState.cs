using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CommonState
{
    public override void EnterState()
    {
        Init?.Invoke();
        EndAction += AnimationEndTRG;
        _animator.OnAnimationEventTrigger += EventAction;
        _animator.OnAnimationEndTrigger += EndAction;
        _animator.SetAttackAnimation(true);
    }

    public override void ExitState()
    {
        EndAction -= AnimationEndTRG;
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;

    }
    public override void AnimationEndTRG()
    {
        _animator.SetAttackAnimation(false);
        fsm.ChangeState(FSMState.Idle);
    }

    public override void UpdateState()
    {
        UpdateAction?.Invoke();
    }

    

}
