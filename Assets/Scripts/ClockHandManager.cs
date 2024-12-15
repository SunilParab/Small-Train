using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockHandManager : MonoBehaviour
{

    public WeeklyUpgradeManager weeklyUpgradeManager;

    public TextMeshProUGUI dayCounter;

    public float weekDuration;
    public float timer;




    // Start is called before the first frame update
    void Start()
    {
        weeklyUpgradeManager = WeeklyUpgradeManager.reference;
        weekDuration = weeklyUpgradeManager.weekDuration;
        timer = weeklyUpgradeManager.timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer = weeklyUpgradeManager.timer;
        //Flip timer so that it's counting from 0 up instead
        timer = weekDuration - timer;

        UpdateWeekText();

        //Change the rotation, which is divided by the total time in the week by 7
        var z = (timer / (weekDuration / 7 / 2) * 360) - 90;

        this.transform.rotation = Quaternion.Euler(0, 0, -z);
    }

    void UpdateWeekText()
    {
        if (timer < weekDuration / 7 * 1)
        {
            dayCounter.text = "SUN";
        }
        else if (timer < weekDuration / 7 * 2)
        {
            dayCounter.text = "MON";
        }
        else if (timer < weekDuration / 7 * 3)
        {
            dayCounter.text = "TUE";
        }
        else if (timer < weekDuration / 7 * 4)
        {
            dayCounter.text = "WED";
        }
        else if (timer < weekDuration / 7 * 5)
        {
            dayCounter.text = "THU";
        }
        else if (timer < weekDuration / 7 * 6)
        {
            dayCounter.text = "FRI";
        }
        else if (timer < weekDuration / 7 * 7)
        {
            dayCounter.text = "SAT";
        }


    }
}
