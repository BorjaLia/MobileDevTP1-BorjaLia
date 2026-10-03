using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
public class CustomHorizontalController : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [InputControl(layout = "Vector2")]
    [SerializeField] private string m_ControlPath;

    [SerializeField] private RectTransform leftTarget;
    [SerializeField] private RectTransform rightTarget;

    [SerializeField] private RectTransform leftLimit;
    [SerializeField] private RectTransform rightLimit;
    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    public void OnPointerDown(PointerEventData eventData) => EvaluatePosition(eventData);

    public void OnDrag(PointerEventData eventData) => EvaluatePosition(eventData);

    public void OnPointerUp(PointerEventData eventData)
    {
        SendValueToControl(Vector2.zero);
    }

    private void EvaluatePosition(PointerEventData eventData)
    {
        if (leftLimit == null || rightLimit == null || leftTarget == null || rightTarget == null) return;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            (RectTransform)transform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector3 worldPoint);

        float pointerX = worldPoint.x;
        float leftX = leftTarget.position.x;
        float rightX = rightTarget.position.x;
        float outputX = 0f;

        if (pointerX >= leftLimit.position.x && pointerX <= rightLimit.position.x)
        {
            float normalizedX = Mathf.InverseLerp(leftX, rightX, pointerX);

            outputX = Mathf.Lerp(-1f, 1f, normalizedX);
        }
        else
        {
            outputX = 0f;
        }

        SendValueToControl(new Vector2(outputX, 0f));

        Debug.Log(m_ControlPath + ": " + outputX);
    }
}