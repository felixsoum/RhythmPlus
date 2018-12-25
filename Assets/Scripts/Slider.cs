using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    [SerializeField] GameObject pointer = null;
    float leftBound = -4;
    float rightBound = 4;
    float spinSpeed;
    public float CurrentPointerValue = 0.5f;
    Vector3 pointerPos;
    public float? FullSpinBound { get; private set; }
    List<float> pointerValues = new List<float>();
    const int PointerValueCount = 50;

    void Start()
    {
        spinSpeed = 1 / (360.0f);
        pointerPos = pointer.transform.localPosition;
    }

    public void Spin(float angle)
    {
        CurrentPointerValue -= angle * spinSpeed;
        CurrentPointerValue = Mathf.Clamp01(CurrentPointerValue);
        pointerValues.Add(CurrentPointerValue);
        if (pointerValues.Count > PointerValueCount)
        {
            pointerValues.RemoveAt(0);
        }

        bool leftBoundDetected = false;
        bool rightBoundDetected = false;
        FullSpinBound = null;
        foreach (float pointerValue in pointerValues)
        {
            if (!FullSpinBound.HasValue && (pointerValue == 0 || pointerValue == 1))
            {
                FullSpinBound = pointerValue;
            }

            leftBoundDetected |= pointerValue == 0;
            rightBoundDetected |= pointerValue == 1;
            if (leftBoundDetected && rightBoundDetected)
            {
                break;
            }
        }

        if (!leftBoundDetected || !rightBoundDetected)
        {
            FullSpinBound = null;
        }
        else
        {
            pointerValues.Clear();
        }
        pointerPos.x = Mathf.Lerp(leftBound, rightBound, CurrentPointerValue);
        pointer.transform.localPosition = pointerPos;
    }

    public float GetPointerX()
    {
        return pointer.transform.position.x;
    }
}
