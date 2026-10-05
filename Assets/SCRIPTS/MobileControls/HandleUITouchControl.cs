using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

public class CustomSwipeController : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [InputControl(layout = "Vector2")]
    [SerializeField] private string m_ControlPath;

    [SerializeField] private float startSwipeSpeed = 1000f;
    [SerializeField] private float stopSwipeSpeed = 10f;

    private Vector2 referencePoint;
    private Vector2 currentPointerPosition;
    private Vector2 lastPointerPosition;

    private bool isDragging;
    private bool isSwiping;
    private bool needsZeroReset;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        isSwiping = false;
        needsZeroReset = false;

        referencePoint = eventData.position;
        currentPointerPosition = eventData.position;
        lastPointerPosition = eventData.position;

        SendValueToControl(Vector2.zero);
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentPointerPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isSwiping)
        {
            EvaluateSwipe();
            needsZeroReset = true;
        }
        else
        {
            SendValueToControl(Vector2.zero);
        }

        isDragging = false;
        isSwiping = false;
    }

    private void Update()
    {
        if (needsZeroReset)
        {
            SendValueToControl(Vector2.zero);
            needsZeroReset = false;
        }

        if (!isDragging) return;

        float speed = Vector2.Distance(currentPointerPosition, lastPointerPosition) / Time.deltaTime;

        if (!isSwiping)
        {
            if (speed >= startSwipeSpeed)
            {
                isSwiping = true;
            }
            else
            {
                referencePoint = currentPointerPosition;
            }
        }
        else
        {
            if (speed < stopSwipeSpeed)
            {
                EvaluateSwipe();

                isSwiping = false;
                referencePoint = currentPointerPosition;
                needsZeroReset = true;
            }
        }

        lastPointerPosition = currentPointerPosition;
    }

    private void EvaluateSwipe()
    {
        Vector2 delta = currentPointerPosition - referencePoint;
        Vector2 output = Vector2.zero;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            output = new Vector2(Mathf.Sign(delta.x) * 1, 0);
        }
        else if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
        {
            output = new Vector2(0, Mathf.Sign(delta.y) * 1);
        }
        else if (delta != Vector2.zero)
        {
            output = new Vector2(0, Mathf.Sign(delta.y) * 1);
        }
        //Debug.Log("value " + output);

        SendValueToControl(output);
    }
}