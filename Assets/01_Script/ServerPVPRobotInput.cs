using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class ServerPVPRobotInput : MonoBehaviour
{
    [SerializeField] PartSO Left = null;
    [SerializeField] PartSO Right = null;
    [SerializeField] PartSO Head = null;
    [SerializeField] PartSO Body = null;
    [SerializeField] PartSO Leg = null;
    [SerializeField] Stat stat;

    
    public IEnumerator FindAndSet()
    {
        yield return StartCoroutine(Setting(Left));
        yield return StartCoroutine(Setting(Right));
        yield return StartCoroutine(Setting(Head));
        yield return StartCoroutine(Setting(Body));
        yield return StartCoroutine(Setting(Leg));



        Destroy(this);
    }

    IEnumerator Setting(PartSO so)
    {
        RobotSettingAndSOList _robot = GetComponent<RobotSettingAndSOList>();

        yield return new WaitUntil(()=>_robot.SetingRealPart(so));

        if (so)
            stat += so.Statues;

        _robot.SetStatues(stat);
    }

    
}