using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLines : MonoBehaviour
{

    [SerializeField] int beatPerMin = 100;
    [SerializeField] int beatLineCount;
    [SerializeField] float startOffset;
    [SerializeField] GameObject beatLinePrefab;
    [SerializeField] List<GameObject> beatLines = new List<GameObject>();

    void OnValidate()
    {
        float beatPerSec = beatPerMin / 60.0f;
        float distancePerBeat = (float)GameDirector.DistancePerSec / beatPerSec;

        if (beatLineCount > 0)
        {
            while (beatLines.Count < beatLineCount)
            {
                beatLines.Add(Instantiate(beatLinePrefab, transform));
            }

            for (int i = 0; i < beatLineCount; i++)
            {
                beatLines[i].transform.localPosition = new Vector3(0, 0, i * distancePerBeat + startOffset);
            }
        }
    }
}
