using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static LineList lineScript;

    public AudioSource[] audioSources = new AudioSource[7];

    // Start is called before the first frame update
    void Start()
    {
        lineScript = LineList.reference;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lineScript.lineList.Length; i++) {
            if (lineScript.lineList[i].LineSegments.Count > 0 && !audioSources[i].isPlaying)
            {
                audioSources[i].Play();
            } else if (lineScript.lineList[i].LineSegments.Count == 0)
            {
                audioSources[i].Stop();
            }
        }
    }
}
