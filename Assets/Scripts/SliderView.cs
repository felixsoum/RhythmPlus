using UnityEngine;

public class SliderView : MonoBehaviour
{
    [SerializeField] RectTransform pointerRectTransform = null;
    RectTransform rectTransform;
    float sliderWidth;
    float pointerWidth;
    float leftBound;
    float rightBound;
    float spinSpeed;
    float currentPointerValue = 0.5f;
    Vector2 pointerAnchoredPosition;
    void Start()
    {
        spinSpeed = 1 / (360.0f);
        rectTransform = GetComponent<RectTransform>();
        sliderWidth = rectTransform.rect.size.x;
        pointerWidth = pointerRectTransform.rect.width;
        leftBound = (pointerWidth - sliderWidth) / 2.0f;
        rightBound = (sliderWidth - pointerWidth) / 2.0f;
        pointerAnchoredPosition = pointerRectTransform.anchoredPosition;
    }

    public void Spin(float angle)
    {
        currentPointerValue -= angle * spinSpeed;
        currentPointerValue = Mathf.Clamp01(currentPointerValue);
        pointerAnchoredPosition.x = Mathf.Lerp(leftBound, rightBound, currentPointerValue);
        pointerRectTransform.anchoredPosition = pointerAnchoredPosition;
    }
}
