using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static LineList lineScript;

    public AudioSource[] basicAudioSources = new AudioSource[7]; //The one Ethan made
    public AudioSource[] connectAudioSources = new AudioSource[7];

    public static SoundManager reference;
    public AudioSource lineDragSound;

    private void Awake()
    {
        reference = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lineScript = LineList.reference;
    }

    // Update is called once per frame
    void Update()
    {

        //Train Notes

        for (int i = 0; i < lineScript.lineList.Length; i++) {
            if (lineScript.lineList[i].LineSegments.Count > 0 && !basicAudioSources[i].isPlaying)
            {
                basicAudioSources[i].Play();
            } else if (lineScript.lineList[i].LineSegments.Count == 0)
            {
                basicAudioSources[i].Stop();
            }
        }

    }
}
