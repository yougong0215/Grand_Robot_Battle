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
    public Stat stat = new();

    
    public IEnumerator FindAndSet(bool server =false)
    {
        yield return StartCoroutine(Setting(Left));
        if(Left != null && server == false) stat += Left?.Statues;
        yield return StartCoroutine(Setting(Right));
        if(Right != null&& server == false) stat += Right?.Statues;
        yield return StartCoroutine(Setting(Head));
         if (Head != null&& server == false)
            stat += Head?.Statues;
        yield return StartCoroutine(Setting(Body));
        
        if (Body != null&& server==false)
            stat += Body?.Statues;
        yield return StartCoroutine(Setting(Leg));
        if (Leg != null&& server==false)
            stat += Leg?.Statues;
        RobotSettingAndSOList _robot = GetComponent<RobotSettingAndSOList>();
        //_robot.SetStatues(stat);
        Debug.LogWarning("나중에 고쳐야됨2");

        _robot._statues = stat;
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