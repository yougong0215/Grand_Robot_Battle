using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddActionButton : MonoBehaviour
{
    [SerializeField] FSM fsm;
    [SerializeField] FSMState state;
    [SerializeField] CommonAction Act;

    public void OnClick()
    {
        fsm.AddAction(state, Act);
    }
}
