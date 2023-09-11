using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ClickChecking : MonoBehaviour
{
    private Canvas _can;
    private UIDocument _doc;
    private VisualElement _root;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _doc = GameObject.Find("UITOOLKIT").GetComponent<UIDocument>();
            _can = GameObject.Find("Canvas").GetComponent<Canvas>();
            if (_doc != null)
            {
                _root = _doc.rootVisualElement;
                _root.RegisterCallback<ClickEvent>(OnClickChecking);
            }
            else if (_can != null)
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current)
                {
                    position = Input.mousePosition
                };

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.GetComponent<UnityEngine.UI.Button>() != null)
                    {
                        SoundManager.Instance.PlaySFX(SFXSoundType.BtnClick);
                        break;
                    }
                }
            }
        }
    }

    private void OnClickChecking(ClickEvent evt)
    {
        Button btn = evt.target as Button;

        if (btn != null)
            SoundManager.Instance.PlaySFX(SFXSoundType.BtnClick);
    }
}
