using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] BeatLines beatLines;
    [SerializeField] Slider slider;
    [SerializeField] AudioSource music;
    double startTime;
    bool isPlaying;
    public const double DistancePerSec = 10.0;
    BeatLine nextBeatLine;
    const float SuccessDistance = 0.5f;

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

    }

    void Play()
    {
        isPlaying = true;
        music.Play();
        startTime = AudioSettings.dspTime;
        nextBeatLine = beatLines.GetNext();
    }
}
