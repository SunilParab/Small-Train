using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerSpawn : MonoBehaviour
{

    public Image timer;
    public GameObject station;
    public float alpha;
    public bool increasing = true;

    //do this later
    public Sprite filled;
    public Sprite empty;

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //timer visuals
        timer.fillAmount = station.GetComponent<PassengerSpawn>().overCrowdTimer/45;
        alpha = 0.3f + (station.GetComponent<PassengerSpawn>().overCrowdTimer/45);

        //max alpha
        if (alpha >= 0.9f){
            alpha = 0.9f;
        }

        timer.color = new Color(timer.color.r, timer.color.g, timer.color.b, alpha);

        //sprites
        if (increasing){
            timer.GetComponent<Image>().sprite = filled;
        }
        else {
            timer.GetComponent<Image>().sprite = empty;
        }
    }
}
