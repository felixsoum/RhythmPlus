using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] BeatLines beatLines = null;
    [SerializeField] Slider slider = null;
    [SerializeField] AudioSource music = null;
    [SerializeField] GameObject cameraObject = null;

    double startTime;
    bool isPlaying;
    public const double DistancePerSec = 10.0;
    BeatLine nextBeatLine;
    const float SuccessDistance = 0.5f;
    float fullSpinSign;
    float fullSpinCurrent;
    float fullSpinTarget;
    float fullSpinProgress;
    float fullSpinSpeed = 2.0f;
    const float CameraAngleMax = 10;

    void Awake()
    {
        Invoke("Play", 2.0f);
    }

    void Update()
    {
        if (!isPlaying)
        {
            return;
        }
        double currentTime = AudioSettings.dspTime - startTime;
        Vector3 pos = slider.transform.position;
        pos.z = (float)(currentTime * DistancePerSec);
        slider.transform.position = pos;

        if (nextBeatLine != null && pos.z + 1.0f >= nextBeatLine.transform.position.z)
        {

            nextBeatLine.Clear(Mathf.Abs(slider.GetPointerX() - nextBeatLine.GetBeatPosX()) <= SuccessDistance);
            nextBeatLine = beatLines.GetNext();
        }

        if (fullSpinSign == 0)
        {
            if (!slider.FullSpinBound.HasValue)
            {
                Vector3 cameraAngles = cameraObject.transform.eulerAngles;
                cameraAngles.z = Mathf.Lerp(-CameraAngleMax, CameraAngleMax, slider.CurrentPointerValue);
                cameraObject.transform.eulerAngles = cameraAngles;
            }
            else
            {
                fullSpinSign = slider.FullSpinBound == 0 ? 1 : -1;
                fullSpinCurrent = cameraObject.transform.eulerAngles.z;
                if (fullSpinCurrent > CameraAngleMax)
                {
                    fullSpinCurrent -= 360;
                }
                else if (fullSpinCurrent < -CameraAngleMax)
                {
                    fullSpinCurrent += 360;
                }
                fullSpinTarget = fullSpinSign == -1 ? -360 : 360;
                fullSpinProgress = 0;
            }
        }
        else
        {
            fullSpinProgress += Time.deltaTime * fullSpinSpeed;
            fullSpinProgress = Mathf.Min(fullSpinProgress, 1);
            Vector3 cameraAngles = cameraObject.transform.eulerAngles;
            cameraAngles.z = Mathf.Lerp(fullSpinCurrent, fullSpinTarget, fullSpinProgress);
            if (fullSpinProgress >= 1)
            {
                fullSpinSign = 0;
            }
            cameraObject.transform.eulerAngles = cameraAngles;
        }
    }

    void Play()
    {
        isPlaying = true;
        music.Play();
        startTime = AudioSettings.dspTime;
        nextBeatLine = beatLines.GetNext();
    }
}
