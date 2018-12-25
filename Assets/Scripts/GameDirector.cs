using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] GameObject slider;
    [SerializeField] AudioSource music;
    double startTime;
    bool isPlaying;
    public const double DistancePerSec = 10.0;

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
    }

    void Play()
    {
        isPlaying = true;
        music.Play();
        startTime = AudioSettings.dspTime;
    }
}
