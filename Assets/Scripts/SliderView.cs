using UnityEngine;

public class SliderView : MonoBehaviour
{
    [SerializeField] RectTransform pointerRectTransform;
    RectTransform rectTransform;
    float sliderWidth;
    float pointerWidth;
    float leftBound;
    float rightBound;
    float spinSpeed;
    float currentPointerValue = 0.5f;

    void Start()
    {
        spinSpeed = 1 / (360.0f);
        rectTransform = GetComponent<RectTransform>();
        sliderWidth = rectTransform.rect.size.x;
        pointerWidth = pointerRectTransform.rect.width;
        leftBound = (pointerWidth - sliderWidth) / 2.0f;
        rightBound = (sliderWidth - pointerWidth) / 2.0f;
    }

    public void Spin(float angle)
    {
        currentPointerValue -= angle * spinSpeed;
        currentPointerValue = Mathf.Clamp01(currentPointerValue);
        Vector2 anchoredPosition = new Vector2(Mathf.Lerp(leftBound, rightBound, currentPointerValue), 0);
        pointerRectTransform.anchoredPosition = anchoredPosition;
    }
}
