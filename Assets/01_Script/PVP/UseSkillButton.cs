using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


[System.Serializable]
public struct LivePVPStructure
{
    [SerializeField] public CommonAction Act;
    [SerializeField] public AnimationClip Anim;
}


public class UseSkillButton : MonoBehaviour
{
    [SerializeField] FSM fsm;
    [SerializeField] FSMState state;
    [SerializeField] LivePVPStructure std;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);

        fsm = FindManager.Instance.FindObject("PlayerBot").GetComponent<FSM>();
    }
    public void OnClick()
    {
        if(fsm.NowState() == FSMState.Idle || fsm.NowState() == FSMState.Move)
        {
            if(std.Act != null)
                fsm.AddAction(state, std.Act);

            if (std.Anim != null)
                Debug.LogWarning("ø¨∞· «ÿ¡‡æﬂµ ");



            fsm.ChangeState(state);
        }
    }
}
