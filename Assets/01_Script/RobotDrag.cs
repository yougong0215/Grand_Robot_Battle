using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RobotDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Transform _ts;
    private float rotationSpeed = 25.0f; // 회전 속도 조절 매개변수
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시 실행되는 코드
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중일 때 실행되는 코드
        Debug.LogWarning("돌아가느중");
        float rotationAmount = eventData.delta.x * rotationSpeed * Time.deltaTime;
        _ts.Rotate(Vector3.up, rotationAmount);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 종료 시 실행되는 코드
    }
}