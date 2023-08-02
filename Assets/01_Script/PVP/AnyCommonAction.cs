using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnyCommonAction : MonoBehaviour
{

    protected FSM fsm;
    public FSM FSMMain => fsm;
    protected AnimationController _animator;
    public AnimationController AnimationCon => _animator;
    protected Transform _parent;


    protected void Awake()
    {
        _parent = transform.parent;

        fsm = _parent.GetComponent<FSM>();

        _animator = transform.parent.Find("Visual").GetComponent<AnimationController>();
    }
    private void Start()
    {
        fsm.AnyFixedState += FixedState;
        fsm.AnyUpdateState += UpdateState;
    }

    protected abstract void UpdateState();
    protected abstract void FixedState();

    public virtual void Destroy()
    {
        fsm.AnyFixedState -= FixedState;
        fsm.AnyUpdateState -= UpdateState;

        Destroy(this.gameObject);
    }

}
