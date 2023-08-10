using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class ServerPVPRobotInput : MonoBehaviour
{
    public string Name = null;
    public PartSO Left = null;
    public PartSO Right = null;
    public PartSO Head = null;
    public PartSO Body = null;
    public PartSO Leg = null;
    public Stat stat;

    
    public IEnumerator FindAndSet()
    {
        yield return StartCoroutine(Setting(Left));
        yield return StartCoroutine(Setting(Right));
        yield return StartCoroutine(Setting(Head));
        yield return StartCoroutine(Setting(Body));
        yield return StartCoroutine(Setting(Leg));

        RobotSettingAndSOList _robot = GetComponent<RobotSettingAndSOList>();
        //_robot.SetStatues(stat);
        Debug.LogWarning("나중에 고쳐야됨2");
        Destroy(this);
    }

    IEnumerator Setting(PartSO so)
    {
        RobotSettingAndSOList _robot = GetComponent<RobotSettingAndSOList>();

        yield return new WaitUntil(()=>_robot.SetingRealPart(so));

        /////// 이건 서버에서 다 처리할꺼
        // if (so)
        //     stat += so.Statues;

        // _robot.SetStatues(stat);
    }

    
}