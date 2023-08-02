using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    public float joystickRadius = 50f; // ���̽�ƽ�� ������ ��

    private Vector2 inputDirection;
    private Vector2 joystickStartPos;
    private int pointerID = -1; // ������ ID�� �����ϴ� ����, �⺻���� -1�� ����
    private bool isTouchingJoystick = false; // ���̽�ƽ ����� ��ġ ������ ���θ� �����ϴ� ����

    private void Start()
    {
        joystickStartPos = joystickBackground.position;
    }

    public bool TouchJoyStick()
    {
        // ������ ID�� ��ȿ�ϰ�, ���̽�ƽ ����� ��ġ ���̸� true�� ��ȯ
        return (pointerID != -1 && isTouchingJoystick);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ���̽�ƽ �ڵ� �̵� ó��
        if (eventData.pointerId == pointerID) // ���� �巡���ϴ� ������ ID�� ����� ������ ID�� ��ġ�ϴ��� Ȯ��
        {
            if (isTouchingJoystick && RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            {
                localPoint.x = (localPoint.x / joystickBackground.sizeDelta.x);
                localPoint.y = (localPoint.y / joystickBackground.sizeDelta.y);

                float x = (joystickBackground.pivot.x == 1f) ? localPoint.x * 2 + 1 : localPoint.x * 2 - 1;
                float y = (joystickBackground.pivot.y == 1f) ? localPoint.y * 2 + 1 : localPoint.y * 2 - 1;

                inputDirection = new Vector2(x, y);
                inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

                // ���̽�ƽ �ڵ��� ��ġ ���� (�ִ� ������ �������� �̵�)
                joystickHandle.anchoredPosition = inputDirection * joystickRadius;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // ���̽�ƽ ����� ��ġ�� ��쿡�� �̺�Ʈ �߻�
        if (RectTransformUtility.RectangleContainsScreenPoint(joystickBackground, eventData.position, eventData.pressEventCamera))
        {
            pointerID = eventData.pointerId; // ������ ID ����
            isTouchingJoystick = true;
            OnDrag(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // �����Ͱ� ���� �� ����� ������ ID�� ��ġ�ϴ� ��쿡�� �̺�Ʈ ó��
        if (eventData.pointerId == pointerID)
        {
            pointerID = -1; // ������ ID �ʱ�ȭ
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
