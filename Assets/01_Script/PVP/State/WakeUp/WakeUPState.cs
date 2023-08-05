using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class WakeUPState : CommonState
{

    public override void AnimationEndTRG()
    {

    }

    public override void EnterState()
    {
        Init?.Invoke();
        EndAction += AnimationEndTRG;
        _animator.OnAnimationEventTrigger += EventAction;
        _animator.OnAnimationEndTrigger += EndAction;
        _animator.SetWakeAnimation(true);
        StartCoroutine(WakeUP());
        
    }
    public override void ExitState()
    {
        EndAction -= AnimationEndTRG;
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;

    }

    IEnumerator WakeUP()
    {
        yield return new WaitForSeconds(2.2f);
        _animator.SetWakeAnimation(false);
        FSMMain.ChangeState(FSMState.Idle);
    }

    public override void UpdateState()
    {

    }
}
