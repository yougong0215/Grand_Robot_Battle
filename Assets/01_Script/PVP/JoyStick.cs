using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    public float joystickRadius = 50f; // 조이스틱의 반지름 값

    private Vector2 inputDirection;
    private Vector2 joystickStartPos;
    private int pointerID = -1; // 포인터 ID를 저장하는 변수, 기본값은 -1로 설정
    private bool isTouchingJoystick = false; // 조이스틱 배경을 터치 중인지 여부를 저장하는 변수

    private void Start()
    {
        joystickStartPos = joystickBackground.position;
    }

    public bool TouchJoyStick()
    {
        // 포인터 ID가 유효하고, 조이스틱 배경을 터치 중이면 true를 반환
        return (pointerID != -1 && isTouchingJoystick);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 조이스틱 핸들 이동 처리
        if (eventData.pointerId == pointerID) // 현재 드래그하는 포인터 ID와 저장된 포인터 ID가 일치하는지 확인
        {
            if (isTouchingJoystick && RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            {
                localPoint.x = (localPoint.x / joystickBackground.sizeDelta.x);
                localPoint.y = (localPoint.y / joystickBackground.sizeDelta.y);

                float x = (joystickBackground.pivot.x == 1f) ? localPoint.x * 2 + 1 : localPoint.x * 2 - 1;
                float y = (joystickBackground.pivot.y == 1f) ? localPoint.y * 2 + 1 : localPoint.y * 2 - 1;

                inputDirection = new Vector2(x, y);
                inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

                // 조이스틱 핸들의 위치 설정 (최대 반지름 내에서만 이동)
                joystickHandle.anchoredPosition = inputDirection * joystickRadius;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 조이스틱 배경을 터치한 경우에만 이벤트 발생
        if (RectTransformUtility.RectangleContainsScreenPoint(joystickBackground, eventData.position, eventData.pressEventCamera))
        {
            pointerID = eventData.pointerId; // 포인터 ID 저장
            isTouchingJoystick = true;
            OnDrag(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 포인터가 떼질 때 저장된 포인터 ID와 일치하는 경우에만 이벤트 처리
        if (eventData.pointerId == pointerID)
        {
            pointerID = -1; // 포인터 ID 초기화
            isTouchingJoystick = false;
            inputDirection = Vector2.zero;
            joystickHandle.anchoredPosition = Vector2.zero;
        }
    }

    public Vector2 GetInputDirection()
    {
        return inputDirection;
    }
}
