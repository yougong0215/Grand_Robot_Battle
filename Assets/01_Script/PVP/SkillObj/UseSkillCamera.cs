using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraEaseing
{
    public CinemachineVirtualCamera cam;
    public CinemachineBlendDefinition.Style blend =CinemachineBlendDefinition.Style.EaseInOut;
    public float nextTime = 0f;
    public float untilTIme = 0f;
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
    CinemachineBrain cam;

    public void Init(Quaternion rot)
    {
        cam = FindManager.Instance.FindObject("MainCamera").gameObject.GetComponent<CinemachineBrain>();
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
            cam.m_DefaultBlend.m_Style = cams[i].blend;
            cam.m_DefaultBlend.m_Time = cams[i].nextTime;
            yield return new WaitForSeconds(cams[i].untilTIme);
            cams[i].cam.Priority = -1;
        }

        yield return new WaitForSeconds(1f);
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
