using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    [Header("계정 관련")]
    [SerializeField] RectTransform Account_List;
    [SerializeField] GameObject Account_Button;

    private void Awake() {
        for (int i = 0; i < Account_List.childCount; i++)
            Destroy(Account_List.GetChild(i).gameObject);
    }

}
