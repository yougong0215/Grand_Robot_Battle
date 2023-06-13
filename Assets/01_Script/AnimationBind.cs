using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBind : MonoBehaviour
{
    [SerializeField] AnimatorOverrideController aoc;
    [SerializeField] Animator _animator;

    private readonly int _isAttackHash = Animator.StringToHash("is_Attack");
    private readonly int _AttackTriggerhash = Animator.StringToHash("_attack");

    bool ani = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void AnimationChange(AnimationClip clip)
    {
        aoc["Attack"] = clip;
        _animator.runtimeAnimatorController = aoc;

        _animator.SetTrigger(_AttackTriggerhash);
        _animator.SetBool(_isAttackHash, true);
    }

    public void OnAnimationEnd()
    {
        ani = true;
    }

    public bool EndAnim()
    {
        if(ani == false)
        {
            return false;
        }
        else
        {
            _animator.ResetTrigger(_AttackTriggerhash);
            _animator.SetBool(_isAttackHash, false);
            ani = false;
            return true;
        }
    }

}
