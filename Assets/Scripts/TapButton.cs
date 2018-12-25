using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TapButton : MonoBehaviour, IPointerDownHandler
{
    Image image;
    float alpha;
    const float PassiveAlpha = 0.1f;

    void Awake()
    {
        image = GetComponent<Image>();
        alpha = PassiveAlpha;
    }

    void Update()
    {
        alpha = Mathf.Max(alpha - 5.0f * Time.deltaTime, PassiveAlpha);
        UpdateColor();
    }

    private void UpdateColor()
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        alpha = 1;
        UpdateColor();
    }


}
