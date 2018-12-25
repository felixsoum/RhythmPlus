using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiscView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Slider slider;
    RectTransform rectTransform;
    Vector2 center;
    Vector2 previousDragPos;
    const float spinAngleMax = 45.0f;
    const float spinDistanceMin = 10.0f;
    float angle;
    float autoSpinSpeed = 100.0f;
    float autoSpinDecay = 50.0f;
    bool isDragging;
    const int LastAngleCount = 5;
    Queue<float> lastAngles = new Queue<float>();

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        center = RectTransformUtility.WorldToScreenPoint(null, rectTransform.position);
    }

    void Update()
    {
        if (!isDragging)
        {
            Spin(angle * Time.deltaTime * autoSpinSpeed);
            if (Mathf.Abs(angle) > 0)
            {
                float magnitude = Mathf.Abs(angle) - Time.deltaTime * autoSpinDecay;
                magnitude = Mathf.Max(magnitude, 0);
                angle = magnitude * Mathf.Sign(angle);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousDragPos = eventData.position;
        isDragging = true;
        lastAngles.Clear();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Spin(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Spin(eventData);
        foreach (var previousAngle in lastAngles)
        {
            if (Mathf.Abs(previousAngle) > Mathf.Abs(angle))
            {
                angle = previousAngle;
            }
        }
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
        angle = Vector2.SignedAngle(previousDragPos - center, currentDragPos - center);
        angle = Mathf.Clamp(angle, -spinAngleMax, spinAngleMax);
        lastAngles.Enqueue(angle);
        if (lastAngles.Count > LastAngleCount)
        {
            lastAngles.Dequeue();
        }
        previousDragPos = currentDragPos;
        Spin(angle);
    }

    void Spin(float spin)
    {
        spin = Mathf.Clamp(spin, -spinAngleMax, spinAngleMax);
        rectTransform.Rotate(0, 0, spin);
        slider.Spin(spin);
    }
}
