using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBind : MonoBehaviour
{
    [SerializeField] AnimatorOverrideController aoc;
    [SerializeField] Animator _animator;
    bool ani = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void AnimationChange(AnimationClip clip)
    {
        aoc["Attack"] = clip;

        _animator.SetTrigger("_attack");
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
            ani = false;
            return true;
        }
    }

}
