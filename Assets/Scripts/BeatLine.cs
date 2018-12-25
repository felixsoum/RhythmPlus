using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLine : MonoBehaviour
{
    private const float BeatRandomSpacing = 0.1f;
    [SerializeField] Renderer beatObject;
    [SerializeField] Renderer successEffect;
    public bool containsBeat = true;
    Material successMaterial;
    float successValue = 1.0f;
    float successEffectSpeed = 10.0f;
    float leftBound = -4.5f;
    float rightBound = 4.5f;
    public float BeatValue { get; set; }

    void OnValidate()
    {
        if (gameObject != null)
        {
            beatObject.gameObject.SetActive(containsBeat);
            successEffect.gameObject.SetActive(containsBeat);
        }
    }

    void Update()
    {
        if (successMaterial != null)
        {
            successValue -= Time.deltaTime * successEffectSpeed;
            Color color = successMaterial.color;
            color.a = successValue;
            successMaterial.color = color;
        }
    }

    public void Clear(bool isSuccess)
    {
        successMaterial = successEffect.material;
        if (!isSuccess)
        {
            successMaterial.color = Color.red;
        }
        beatObject.enabled = false;
    }

    public float GetBeatPosX()
    {
        return beatObject.transform.position.x;
    }

    public void PlaceBeat(float value)
    {
        float randomValue = Random.value;
        if (randomValue < 0.25f)
        {
            value -= BeatRandomSpacing;
        }
        else if (randomValue < 0.5f)
        {
            value += BeatRandomSpacing;
        }

        value = Mathf.Clamp01(value);
        Vector3 pos = beatObject.transform.localPosition;
        pos.x = Mathf.Lerp(leftBound, rightBound, value);
        beatObject.transform.localPosition = pos;
        BeatValue = value;
    }
}
