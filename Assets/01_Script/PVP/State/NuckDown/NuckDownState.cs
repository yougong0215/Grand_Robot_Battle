using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class NuckDownState : CommonState
{

    float _time = 0f;
    float _initTime = 0f;
    [SerializeField] EscapeCircleTurn ball;

    bool wakeUp = false;

    public override void AnimationEndTRG()
    {

    }

    public override void EnterState()
    {
        Init?.Invoke();
        EndAction += AnimationEndTRG;
        _animator.OnAnimationEventTrigger += EventAction;
        _animator.OnAnimationEndTrigger += EndAction;
        _animator.SetNuckDownAnimation(true);
        _time = 10f; // HP랑 Speed값에 비례해서 패배시간 만들어야될듯?
        _initTime = _time;
    }

    public override void ExitState()
    {
        EndAction -= AnimationEndTRG;
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;

    }

    public override void UpdateState()
    {
        _time -= Time.deltaTime;
        Debug.Log(_time);
        UpdateAction?.Invoke();
        if (ball.IsOK() && (_initTime - _time > 3 || _time< 3) )
        {
            _animator.SetNuckDownAnimation(false);
            FSMMain.ChangeState(FSMState.WakeUP);
            return;
        }
        if (_time < 0f)
        {
            FSMMain.ChangeState(FSMState.Die);
        }

    }

}
