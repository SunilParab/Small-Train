using System.Collections;
using System.Collections.Generic;
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

        if (spawnTime % 240 == 0)
        {
            int stationNum = Random.Range(0, 10);
            GameObject station = square;
            int xPos = Random.Range(-9, 9);
            int yPos = Random.Range(-4, 4);
            
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
                    Debug.Log(stations[i].transform.position.x + " " + stations[i].transform.position.y);
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
                    xPos = Random.Range(-9, 9);
                    yPos = Random.Range(-4, 4);
                }

            }

        }
    }
}
