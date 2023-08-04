using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class RotationRect : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private bool isTouching;
    private Vector2 inputDirection;
    Vector2 pos;
    JoyStick joy;
    int pointID = -1;

    private void Awake()
    {
        joy = GetComponent<JoyStick>();
    }
    public Vector2 GetInputDirection()
    {
        Debug.Log(inputDirection);


        return inputDirection.normalized * 5;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDirection = Vector2.zero;
        isTouching = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isTouching && pointID == eventData.pointerId)
        {
            inputDirection = Input.GetTouch(pointID).position - pos;

            if(Vector2.Distance( inputDirection, pos) < 0.3)
            {
                inputDirection = Vector2.zero;
            }
            pos = Input.GetTouch(pointID).position;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointID = eventData.pointerId;

        isTouching = true;
        OnDrag(eventData);


    }
}
