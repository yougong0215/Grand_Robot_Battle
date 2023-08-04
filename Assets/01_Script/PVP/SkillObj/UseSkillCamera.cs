using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

[System.Serializable]
public class CameraEaseing
{
    public CinemachineVirtualCamera cam;
    public float nextTime = 0f;
}

public class UseSkillCamera : MonoBehaviour
{
    public List<CameraEaseing> cams = new();

    [Header("Animation Time")]
    [SerializeField] float _endTime = 1f;
    float _camsTime = 0f;
    FSM _fsm;

    bool init = false;

    Coroutine co;

    public void Init(Quaternion rot)
    {
        transform.rotation = rot;

        co = StartCoroutine(camsCol());

        _camsTime = 0f;
        init = true;
    }

    IEnumerator camsCol()
    {
        for(int i =0; i < cams.Count; i++)
        {
            cams[i].cam.Priority = 1001;
            yield return new WaitForSeconds(cams[i].nextTime);
            cams[i].cam.Priority = -1;
        }
        
    }

    public void Hits()
    {
        if(co!=null)
            StopCoroutine(co);
    }

    private void Update()
    {
        _camsTime += Time.deltaTime;
        if (_camsTime > _endTime && init)
        {
            init = false;
            if (co != null)
                StopCoroutine(co);
            for (int i = 0; i < cams.Count; i++)
                cams[i].cam.Priority = -1;

            
        }
    }



}
