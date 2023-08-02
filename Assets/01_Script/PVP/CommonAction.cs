using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonAction : MonoBehaviour
{
    [SerializeField] protected CommonState com;

    protected FSM FSMMain => com.FSMMain;


    protected virtual void Start()
    {
        com = transform.parent.GetComponent<CommonState>();

        com.Init += Init;

        com.EventAction += OnEventFunc;

        com.UpdateAction += OnUpdateFunc;

        com.EndAction += OnEndFunc;
    }




    public virtual void Destroy()
    {
        com.Init -= Init;

        com.EventAction -= OnEventFunc;

        com.UpdateAction -= OnUpdateFunc;

        com.EndAction -= OnEndFunc;


        Destroy(this.gameObject);
    }

    protected abstract void Init();
    protected abstract void OnEventFunc();
    protected abstract void OnEndFunc();
    protected abstract void OnUpdateFunc();

    
}
