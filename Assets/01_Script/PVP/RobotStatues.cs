using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RobotStatues : MonoBehaviour
{
    bool server = false;
    [SerializeField]
    Stat st;

    float HP = 0;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        //yield return new WaitUntil(() => server);
        Debug.LogWarning("º≠πˆ ø‰√ª «ÿ¡‡æﬂµ ");

        if (transform.Find("Visual").TryGetComponent<RobotSettingAndSOList>(out RobotSettingAndSOList rt))
        {
            st = rt._statues;
            HP = rt._statues.HP;
        }
    }

    public void AddBuff(Stat st)
    {
        this.st += st;
    }

    public void RemoveBuff(Stat st)
    {
        st.HP = 0;
        this.st -= st;
    }


    
}
