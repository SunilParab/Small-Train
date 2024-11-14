using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StationSpawnManager : MonoBehaviour
{

    public GameObject square;
    public GameObject triangle;
    public GameObject circle;
    public GameObject diamond;
    public GameObject pie;
    public GameObject star;
    public GameObject rhombus;
    public GameObject plus;
    public GameObject eye;
    public GameObject pentagon;

    private int spawnTime;
    private List<GameObject> stations;

    public float rareProb = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;
        stations = new List<GameObject>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTime++;

        if (spawnTime % 20 == 0)
        {
            Debug.Log(rareProb);
            int stationNum = 0;
            if (RandomGaussian(0f, 1f) >= rareProb)
            {
                stationNum = UnityEngine.Random.Range(3, 10);
                //Make the probability of rare stations decrease
                rareProb += 0.02f;
            }
            else
            {
                stationNum = UnityEngine.Random.Range(0, 3);
                //Make the probability of rare stations increase
                rareProb -= 0.02f;
            }
            GameObject station = square;

            int xPos = UnityEngine.Random.Range(-9, 9);
            int yPos = UnityEngine.Random.Range(-4, 4);
            
            switch (stationNum) {
                case 0:
                    station = square;
                    break;
                case 1:
                    station = triangle;
                    break;
                case 2:
                    station = circle;
                    break;
                case 3: 
                    station = pie;
                    break;
                case 4:
                    station = star;
                    break;
                case 5:
                    station = rhombus;
                    break;
                case 6:
                    station = diamond;
                    break;
                case 7:
                    station = plus;
                    break;
                case 8:
                    station = eye;
                    break;
                case 9:
                    station = pentagon;
                    break;
            }

            //while trying to find a location, check where I'll spawn against all existing stations
            //if there's one too close, change my location, and then reiterate through the whole for loop again, tries++
            //if there's no station too close, set spawned to true and spawn the station
            bool spawned = false;
            int tries = 0;
            while (!spawned && tries < 10)
            {
                bool clearToSpawn = true;

                for (int i = 0; i < stations.Count; i++)
                {
                    //Debug.Log(stations[i].transform.position.x + " " + stations[i].transform.position.y);
                    if (xPos < stations[i].transform.position.x + 2 && xPos > stations[i].transform.position.x - 2 &&
                        yPos < stations[i].transform.position.y + 2 && yPos > stations[i].transform.position.y - 2)
                    {
                        Debug.Log("Failed Spawn");
                        clearToSpawn = false;
                    }
                }

                if (clearToSpawn)
                {
                    var curStation = Instantiate(station, new Vector3(xPos, yPos, -1), Quaternion.identity);
                    stations.Add(curStation);
                    spawned = true;
                } else
                {
                    tries++;
                    xPos = UnityEngine.Random.Range(-9, 9);
                    yPos = UnityEngine.Random.Range(-4, 4);
                }

            }


        }
    }

    public static float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }
}
