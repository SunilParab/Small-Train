using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawn : MonoBehaviour
{
    //give this (script) list to all the spawned stations
    public List<String> passengersInStation = new();

    //passenger variables
    private int passengerNum = UnityEngine.Random.Range(0,10);
    private String passengerString;
    private int stationNum; //will reference stationNum in stations scripts


    //timer
    public float passengerSpawnTime = UnityEngine.Random.Range(5,21); 
    private float timer;         
    

    // Start is called before the first frame update
    void Start()
    {

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
        //spawn every 5~20 seconds (random) approximately
        timer = passengerSpawnTime; 
    }

    // Update is called once per frame
    void Update()
    {
            
        if (Time.timeScale > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                //add passenger
                //they cannot be the same shape as the station
                if (passengerNum != stationNum){
                    //add passenger
                    //AddPassenger();

                    //reset timer
                    passengerSpawnTime = UnityEngine.Random.Range(5,21); 
                    timer = passengerSpawnTime;
                }
                else {
                    //rerandomize shape
                    passengerNum = UnityEngine.Random.Range(0,10);
                }
            }
        }

        //if train arrives and has enough space:
        for (int i = 0; i < passengersInStation.Count; i ++);
        {
            //if stationNum == passengerNum){
                //get on the train
            //}
        }
    }
}
