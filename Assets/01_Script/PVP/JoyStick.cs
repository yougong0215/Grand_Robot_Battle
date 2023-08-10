using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    Camera cam;
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    public float joystickRadius = 50f; // ���̽�ƽ�� ������ ��

    private Vector2 inputDirection;
    private Vector2 joystickStartPos;
    public int pointerID = -1; // ������ ID�� �����ϴ� ����, �⺻���� -1�� ����

    int tch;

    private void Start()
    {
        cam = FindManager.Instance.FindObject("UICam").GetComponent<Camera>();
        joystickStartPos = joystickBackground.position;
    }

    public bool TouchJoyStick(Touch eventData)
    {
        tch = eventData.fingerId;
        
        if (RectTransformUtility.RectangleContainsScreenPoint(joystickBackground, eventData.position, cam))
        {
            return true;
        }
        return false;
    }

    public void UpdatePointer(int eventData)
    {
        // ���̽�ƽ �ڵ� �̵� ó��



        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, Input.GetTouch(eventData).position, cam, out Vector2 localPoint))
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
        if (Input.GetTouch(eventData).phase == TouchPhase.Ended)
        {
            tch = -1;
            inputDirection = new Vector2(0, 0);
        }
    }

    public Vector2 GetInputDirection()
    {
        return inputDirection;
    }
}
