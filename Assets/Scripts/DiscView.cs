using UnityEngine;
using UnityEngine.EventSystems;

public class DiscView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] SliderView sliderView;
    RectTransform rectTransform;
    Vector2 center;
    Vector2 previousDragPos;
    const float spinAngleMax = 10.0f;
    const float spinDistanceMin = 3.0f;
    float angle;
    bool isDragging;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        center = RectTransformUtility.WorldToScreenPoint(null, rectTransform.position);
    }

    void Update()
    {
        if (isDragging)
        {

        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousDragPos = eventData.position;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Spin(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Spin(eventData);
        isDragging = false;
    }

    void Spin(PointerEventData eventData)
    {
        Vector2 currentDragPos = eventData.position;
        float distanceToCenter = Vector2.Distance(currentDragPos, center);
        if (distanceToCenter < spinDistanceMin)
        {
            previousDragPos = currentDragPos;
            return;
        }
        float distance = Vector2.Distance(currentDragPos, previousDragPos);
        Debug.Log(distanceToCenter);
        angle = Vector2.SignedAngle(previousDragPos - center, currentDragPos - center);
        angle = Mathf.Clamp(-spinAngleMax, angle, spinAngleMax);
        previousDragPos = currentDragPos;
        rectTransform.Rotate(0, 0, angle);
        sliderView.Spin(angle);
    }
}
