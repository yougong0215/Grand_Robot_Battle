using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : CommonState
{
    float _time = 0f;
    [SerializeField] AnimationClip c1;
    [SerializeField] AnimationClip c2;
    bool b = false;
    public override void EnterState()
    {
        if(b)
        {
            b = false;
            _animator.ChangeAnimationClip(FSMState.Hit, c1);
        }
        else
        {
            b = true;
            _animator.ChangeAnimationClip(FSMState.Hit, c2);
        }

        Init?.Invoke();
        EndAction += AnimationEndTRG;
        _animator.OnAnimationEventTrigger += EventAction;
        _animator.OnAnimationEndTrigger += EndAction;
        _animator.SetHitAnimation(true);
        _time = 0.2f;
    }

    public override void ExitState()
    {
        EndAction -= AnimationEndTRG;
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;

    }
    public override void UpdateState()
    {
        if(_time > 0)
        {
            _time -= Time.deltaTime;
            FSMMain.Move(FSMMain.transform.forward * -10);
        }
        UpdateAction?.Invoke();
    }
    public override void AnimationEndTRG()
    {
        _animator.SetHitAnimation(false);
        fsm.ChangeState(FSMState.Idle);
    }
}
