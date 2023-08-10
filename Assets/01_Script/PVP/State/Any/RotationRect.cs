using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class RotationRect : MonoBehaviour
{
    private Vector2 inputDirection;
    Vector2 pos;



    public Vector2 GetInputDirection()
    {
        Debug.Log(inputDirection);
        Vector2 vec = inputDirection;
        inputDirection = new Vector2(0, 0);

        return vec.normalized * 5;
    }


    public void UpdatePointer(int pnt)
    {


        if (Vector2.Distance(inputDirection, pos) < 0.3)
        {
            inputDirection = Vector2.zero;
        }
        try
        {
            inputDirection = Input.GetTouch(pnt).position - pos;
            pos = Input.GetTouch(pnt).position;

            if (Input.GetTouch(pnt).phase == TouchPhase.Ended)
            {
                pos = new Vector2(0, 0);
                inputDirection = Vector3.zero;
            }
        }
        catch
        {
            Debug.Log($"{this} : {pnt}");
        }


    }

}
