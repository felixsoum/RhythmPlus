using UnityEngine;

public class Slider : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    float leftBound = -4;
    float rightBound = 4;
    float spinSpeed;
    float currentPointerValue = 0.5f;
    Vector3 pointerPos;

    void Start()
    {
        spinSpeed = 1 / (360.0f);
        pointerPos = pointer.transform.localPosition;
    }

    public void Spin(float angle)
    {
        currentPointerValue -= angle * spinSpeed;
        currentPointerValue = Mathf.Clamp01(currentPointerValue);
        pointerPos.x = Mathf.Lerp(leftBound, rightBound, currentPointerValue);
        pointer.transform.localPosition = pointerPos;
    }
}
