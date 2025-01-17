using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum FSMState
{
    Any = 0,
    Idle = 1,
    Move = 10, // ������
    Attack = 11,
    Hit = 12,
    NuckDown =13,
    Die = 14,
    WakeUP = 15
}



public class FSM : MonoBehaviour
{
    [SerializeField]
    private Dictionary<FSMState, CommonState> stateMap = new Dictionary<FSMState, CommonState>();
    [SerializeField] private CommonState currentState;
    [SerializeField] private CharacterController _chara;

    public Action AnyUpdateState = null;
    public Action AnyFixedState = null;
    public CharacterController Character => _chara;
    [SerializeField] GameObject AnyState;
    Vector3 moveDirection = Vector3.zero;

    bool isChangeing = false;



    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
        
    }

    //public void LookRotations(Vector3 dir)
    //{
    //    dir.y = transform.position.y;
    //    transform.rotation = Quaternion.LookRotation(dir - transform.position);

    //    transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    //}

    public void AddState(FSMState state, CommonState stateObject)
    {
        stateMap[state] = stateObject;
        Debug.Log(stateMap[state]);
    }

    public void AddAction(FSMState state, CommonAction action)
    {
        Instantiate(action, stateMap[state].transform);
    }

    public void AnyStateAdd(AnyCommonAction any)
    {
        Instantiate(any, AnyState.transform);
    }

    public void SetInitialState(FSMState initialState)
    {
        if (stateMap.TryGetValue(initialState, out CommonState stateObject))
        {
            currentState = stateObject;
            currentState.EnterState();
        }
        else
        {
            Debug.LogError("Invalid initial state: " + initialState);
        }
    }

    public FSMState NowState()
    {
        return currentState._myState;
    }

    public CommonState CurrentState => currentState;

    public void ChangeState(FSMState newState)
    {
        if (isChangeing)
            return;

        isChangeing = true;
        if (currentState != null)
        {
            currentState.ExitState();
        }

        if (stateMap.TryGetValue(newState, out CommonState stateObject))
        {
            
            currentState = stateObject;
            currentState.EnterState();
        }
        else
        {
            Debug.LogError("Invalid state: " + newState);
        }
        isChangeing = false;
    }

    private void Update()
    {
        if (isChangeing)
            return;
        if(Input.GetKeyDown(KeyCode.T))
        {
            ChangeState(FSMState.Hit);
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            ChangeState(FSMState.NuckDown);
        }
        currentState?.UpdateState();
        AnyUpdateState?.Invoke();
    }

    public void Move(Vector3 vec)
    {
        moveDirection += vec;
    }

    private void FixedUpdate()
    {
        if (isChangeing)
            return;

        AnyFixedState?.Invoke();
        Character.Move(transform.TransformDirection(moveDirection * Time.deltaTime));
        moveDirection = Vector3.zero;
    }

    
}