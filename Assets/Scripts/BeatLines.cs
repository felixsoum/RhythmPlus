using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BeatLines : MonoBehaviour
{
    [SerializeField] int beatPerMin = 100;
    [SerializeField] int beatLineCount = 0;
    [SerializeField] float startOffset = 0;
    [SerializeField] GameObject beatLinePrefab = null;
    [SerializeField] List<BeatLine> beatLines = new List<BeatLine>();

    int beatLineIndex = -1;

    void OnValidate()
    {
        float beatPerSec = beatPerMin / 60.0f;
        float distancePerBeat = (float)GameDirector.DistancePerSec / beatPerSec;

        if (beatLineCount > 0)
        {
            bool isBeatAdded = false;
            while (beatLines.Count < beatLineCount)
            {
                isBeatAdded = true;
                GameObject beatLineObject = Instantiate(beatLinePrefab, transform);
                beatLines.Add(beatLineObject.GetComponent<BeatLine>());
            }

            for (int i = 0; i < beatLineCount; i++)
            {
                beatLines[i].transform.localPosition = new Vector3(0, 0, i * distancePerBeat + startOffset);
                beatLines[i].gameObject.name = "BeatLine" + i;

                if (isBeatAdded)
                {
                    float previousValue = 0.5f;
                    if (i > 0)
                    {
                        previousValue = beatLines[i - 1].BeatValue;
                    }
                    beatLines[i].PlaceBeat(previousValue);
                }
            }
        }
        else if (beatLines.Count > 0)
        {
            EditorApplication.delayCall += () =>
            {
                foreach (var beatLine in beatLines)
                {
                    DestroyImmediate(beatLine.gameObject);
                }
                beatLines.Clear();
            };
        }
    }

    internal BeatLine GetNext()
    {
        while (++beatLineIndex < beatLines.Count)
        {
            if (beatLines[beatLineIndex].containsBeat)
            {
                return beatLines[beatLineIndex];
            }
        }
        return null;
    }
}
