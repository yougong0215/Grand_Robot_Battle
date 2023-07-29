using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonAction : MonoBehaviour
{
    protected CommonState com;

    protected virtual void OnEnable()
    {
        com = transform.parent.GetComponent<CommonState>();

        com.Init += Init;
        
        com.EventAction += OnEventFunc;
        
        com.UpdateAction += OnUpdateFunc;

        com.EndAction += OnEndFunc;
    }

    protected abstract void Init();
    protected abstract void OnEventFunc();
    protected abstract void OnEndFunc();
    protected abstract void OnUpdateFunc();
}
