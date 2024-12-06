using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StationSpawnManager : MonoBehaviour
{

    //station gameobjects
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

    //passenger/station shape spawn rarity? variables
    public static int maxShapeNum = 3; //caps at 10
    public static bool rareSpawn = false;
    public static int stationCount; 
    public static bool spawnNextPassengerShape;

    private List<GameObject> stations;

    [Header("Variables")]
    private int spawnTime;
    public int spawnSpeed;
    public static float rareProb = 0.9f;
    public int xRange;
    public int yRange;
    public int yDownRange;
    public int range;
    public int zoomCount = 0;
    bool spawnedFirst;
    bool spawnedSecond;
    bool spawnedThird;
    int xPos;
    int yPos;
    GameObject station;

    //passenger spawn timer variables
    public float passengerSpawnTime; 

    //map object
    public GameObject map;

    // Start is called before the first frame update
    void Start()
    {   
        
        spawnTime = 0;
        xPos = UnityEngine.Random.Range(-xRange, xRange);
        yPos = UnityEngine.Random.Range(-yRange, yRange);

        //spawn every 5~20 seconds (random) approximately
        passengerSpawnTime = UnityEngine.Random.Range(5,21); 

        //station distance range
        range = 2;
        xRange = 4;
        yRange = 2;

        //resize prefabs
        float scale = 0.05f;
        square.transform.localScale = new Vector3(scale, scale, scale);
        triangle.transform.localScale = new Vector3(scale, scale, scale);
        circle.transform.localScale = new Vector3(scale, scale, scale);
        diamond.transform.localScale = new Vector3(scale, scale, scale);
        pie.transform.localScale = new Vector3(scale, scale, scale);
        star.transform.localScale = new Vector3(scale, scale, scale);
        rhombus.transform.localScale = new Vector3(scale, scale, scale);
        plus.transform.localScale = new Vector3(scale, scale, scale);
        eye.transform.localScale = new Vector3(scale, scale, scale);
        pentagon.transform.localScale = new Vector3(scale, scale, scale);
        
        stations = new List<GameObject>();

        //spawn three stations when game starts
        while (!spawnedFirst)
        {   
            //square
            station = square;
            spawnedFirst = SpawnStation(station, xPos, yPos);
            xPos = UnityEngine.Random.Range(-xRange, xRange);
            yPos = UnityEngine.Random.Range(-yRange, yRange + 1);
        }
        while (!spawnedSecond)
        {   
            //triangle
            station = triangle;
            spawnedSecond = SpawnStation(station, xPos, yPos);
            xPos = UnityEngine.Random.Range(-xRange, xRange);
            yPos = UnityEngine.Random.Range(-yRange, yRange + 1);
        }
        while (!spawnedThird)
        {   
            //circle
            station = circle;
            spawnedThird = SpawnStation(station, xPos, yPos);
            xPos = UnityEngine.Random.Range(-xRange + 1, xRange);
            yPos = UnityEngine.Random.Range(-yRange, yRange + 1);
        }

        range = 2;
        xRange = 5;
        yRange = 3;

    }

    void Update(){

        //rare stations spawn after ~ten spawns (including initial three)
        if (stationCount >= 7) {
            rareSpawn = true;
        }

        //caps at 10
        if (maxShapeNum >= 10){
            maxShapeNum = 10;
        }

    }

    void FixedUpdate()
    {
        spawnTime++;
        int stationNum;

        if (spawnTime % spawnSpeed == 0)
        {
            //Debug.Log(rareProb);
            
            if (stationCount == 7){
                stationNum = 3;
                maxShapeNum ++;
                spawnNextPassengerShape = true;
            }
            else if (RandomGaussian(0f, 1f) >= rareProb && rareSpawn)
            {
                stationNum = UnityEngine.Random.Range(maxShapeNum, maxShapeNum); //3 ~ cap
                //Make the probability of rare stations decrease
                rareProb += 0.06f;

                maxShapeNum ++;
                spawnNextPassengerShape = true;
            }
            else
            {   
                stationNum = UnityEngine.Random.Range(0, 3); //0 ~ 2

                //Make the probability of rare stations increase
                rareProb -= 0.04f;
                spawnNextPassengerShape = false;
            }
            station = square;

            xPos = UnityEngine.Random.Range(-xRange, xRange);
            yPos = UnityEngine.Random.Range(-yRange, yRange);
            
            switch (stationNum) {
                case 0:
                    station = square;
                    station.GetComponent<PassengerSpawn>().stationString = "square";
                    break;
                case 1:
                    station = triangle;
                    station.GetComponent<PassengerSpawn>().stationString = "triangle";
                    break;
                case 2:
                    station = circle;
                    station.GetComponent<PassengerSpawn>().stationString = "circle";
                    break;
                case 3: 
                    station = star;
                    station.GetComponent<PassengerSpawn>().stationString = "star";
                    break;
                case 4:
                    station = plus;
                    station.GetComponent<PassengerSpawn>().stationString = "plus";
                    break;
                case 5:
                    station = rhombus;
                    station.GetComponent<PassengerSpawn>().stationString = "rhombus";
                    break;
                case 6:
                    station = diamond;
                    station.GetComponent<PassengerSpawn>().stationString = "diamond";
                    break;
                case 7:
                    station = pie;
                    station.GetComponent<PassengerSpawn>().stationString = "pie";
                    break;
                case 8:
                    station = eye;
                    station.GetComponent<PassengerSpawn>().stationString = "eye";
                    break;
                case 9:
                    station = pentagon;
                    station.GetComponent<PassengerSpawn>().stationString = "pentagon";
                    break;
            }

            //while trying to find a location, check where I'll spawn against all existing stations
            //if there's one too close, change my location, and then reiterate through the whole for loop again, tries++
            //if there's no station too close, set spawned to true and spawn the station
            
            int tries = 0;
            if (!CameraZoom.stopZoom){
                
                bool spawned = false;

                while (!spawned && tries < 100)
                {   
                    
                    spawned = SpawnStation(station, xPos, yPos);

                    if (spawned){
                        
                        //increase station count
                        stationCount ++;

                        //camera zoom
                        CameraZoom.haveToZoom = true;
                        zoomCount += 1;

                        if (zoomCount <= 12){
                            
                            //increase spawn range
                            if (zoomCount == 3 | zoomCount == 6 | zoomCount == 9){
    
                                xRange += 1;
                                yDownRange += 1;

                            }

                            else if (zoomCount == 12){
                                
                                xRange += 1;
                                yDownRange += 1;
                                zoomCount = 0;
                            }
                        }
                        
                    }
                    xPos = UnityEngine.Random.Range(-xRange, xRange);
                    yPos = UnityEngine.Random.Range(-yRange - yDownRange, yRange);
                    tries++;
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

    //spawn station boolean
    public bool SpawnStation(GameObject station, int xPos, int yPos){

        for (int i = 0; i < stations.Count; i++){

            //fail spawn when stations are too close
            if (xPos < stations[i].transform.position.x + range && xPos > stations[i].transform.position.x - range &&
                yPos < stations[i].transform.position.y + range && yPos > stations[i].transform.position.y - range)
            {
                //Debug.Log("Failed Spawn");
                return false;
            }
        }

        //fail spawn if collide with water
        var curStation = Instantiate(station, new Vector3(xPos, yPos, 0), Quaternion.identity);
        var waterScript = curStation.GetComponent<StationCollideWater>();

        if (waterScript.IsCollidingWater(map.GetComponent<Collider2D>(), curStation.GetComponent<Collider2D>(), new Vector3(curStation.transform.position.x,curStation.transform.position.y,0))){
            //Debug.Log("Water Spawn");
            Destroy(curStation);
            return false;
        }

        stations.Add(curStation);
        return true;
    }
}
