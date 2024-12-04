using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawn : MonoBehaviour
{
    //give this (script) list to all the spawned stations
    public List<String> passengersInStation = new();
    //public List<GameObject> stationsInLine = new();

    //lines the station is connected to 
    public List<int> connectedLines = new();
    

    //passenger variables
    private int passengerNum;
    private String passengerString;
    public String stationString; //will reference stationString in stations scripts


    //timer
    public float passengerSpawnTime;
    private float timer;   

    //segment reference stuff
    SegmentInfo curSegment;
    private LineInfo[] lineInfos;    
    public int myLine;  
    

    // Start is called before the first frame update
    void Start()
    {
        passengerNum = UnityEngine.Random.Range(0,10);
        passengerSpawnTime = UnityEngine.Random.Range(5,21); 

        //instantiate passenger shape
        RandomizePassengerShape();

        //spawn every 5~20 seconds (random) approximately
        timer = passengerSpawnTime; 
    }

    // Update is called once per frame
    void Update()
    {   
        //change later for optimization
        //stationsInLine = 
        //curSegment = lineInfos[myLine].LineSegments[0];


        if (Time.timeScale > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                //add passenger
                //they cannot be the same shape as the station
                if (!passengerString.Equals(stationString)){
                    //add passenger
                    AddPassenger(passengerString);

                    //reset timer
                    passengerSpawnTime = UnityEngine.Random.Range(5,21); 
                    timer = passengerSpawnTime;
                }
                else {
                    //rerandomize shape
                    passengerNum = UnityEngine.Random.Range(0,10);
                    RandomizePassengerShape();
                }
            }
        }
    }

    public void AddPassenger(string newPassenger){

        //check if station is full...

        //add passenger
        passengersInStation.Add(newPassenger);
    }

    public void RandomizePassengerShape(){
        switch (passengerNum) {
            case 0:
                passengerString = "square";
                break;
            case 1:
                passengerString = "triangle";
                break;
            case 2:
                passengerString = "circle";
                break;
            case 3: 
                passengerString = "pie";
                break;
            case 4:
                passengerString = "star";
                break;
            case 5:
                passengerString = "rhombus";
                break;
            case 6:
                passengerString = "diamond";
                break;
            case 7:
                passengerString = "plus";
                break;
            case 8:
                passengerString = "eye";
                break;
            case 9:
                passengerString = "pentagon";
                break;
        }
    }
}
