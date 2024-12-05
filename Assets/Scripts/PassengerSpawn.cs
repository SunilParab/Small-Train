using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawn : MonoBehaviour
{
    //give this (script) list to all the spawned stations
    public List<string> passengersInStation = new();

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

    public GameObject circlePassenger;
    public GameObject squarePassenger;
    public GameObject trianglePassenger;
    public GameObject starPassenger;
    public GameObject pentagonPassenger;
    public GameObject rhombusPassenger;
    public GameObject plusPassenger;
    public GameObject piePassenger;
    public GameObject diamondPassenger;
    public GameObject eyePassenger;
    

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
                    SpawnPassengerIcons();

                    //reset timer
                    passengerSpawnTime = UnityEngine.Random.Range(5,21); 
                    timer = passengerSpawnTime;
                    
                }
                
                passengerNum = UnityEngine.Random.Range(0,10);
                RandomizePassengerShape();
            }
        }

    }

    public void AddPassenger(string newPassenger){

        //check if train is full...

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

    public void SpawnPassengerIcons(){

        GameObject passenger;

        for (int i = 0; i < passengersInStation.Count; i ++){
            if (passengersInStation[i].Equals("square")){
                passenger = squarePassenger;     
            }
            else if (passengersInStation[i].Equals("triangle")){
                passenger = trianglePassenger;
            }
            else if (passengersInStation[i].Equals("circle")){
                passenger = circlePassenger;
            }
            else if (passengersInStation[i].Equals("pie")){
                passenger = piePassenger;
            }
            else if (passengersInStation[i].Equals("star")){
                passenger = starPassenger;
            }
            else if (passengersInStation[i].Equals("rhombus")){
                passenger = rhombusPassenger;
            }
            else if (passengersInStation[i].Equals("diamond")){
                passenger = diamondPassenger;
            }
            else if (passengersInStation[i].Equals("plus")){
                passenger = plusPassenger;
            }
            else if (passengersInStation[i].Equals("eye")){
                passenger = eyePassenger;
            }
            else {
                passenger = pentagonPassenger;
            }

            Instantiate(passenger, PassengerPosition(i), Quaternion.identity);
        }
    }

    public Vector2 PassengerPosition(int positionNum){
        
        Vector2 position = new();
        float xPos = transform.position.x + 0.2f;
        float yPos = transform.position.y - 0.2f;

        float xDistance = 0.35f;
        float yDistance = 0.175f;

        //top
        if (positionNum == 0){
            position = new Vector2(xPos + xDistance * 1, yPos - yDistance * 2);
        }
        else if (positionNum == 1){
            position = new Vector2(xPos + xDistance * 2, yPos - yDistance * 2);
        }
        else if (positionNum == 2){
            position = new Vector2(xPos + xDistance * 3, yPos - yDistance * 2);
        }
        else if (positionNum == 3){
            position = new Vector2(xPos + xDistance * 4, yPos - yDistance * 2);
        }
        else if (positionNum == 4){
            position = new Vector2(xPos + xDistance * 5, yPos - yDistance * 2);
        }
        else if (positionNum == 5){
            position = new Vector2(xPos + xDistance * 6, yPos - yDistance * 2);
        }

        //middle
        else if (positionNum == 6){
            position = new Vector2(xPos + xDistance * 7, yPos - yDistance * 3);
        }

        //bottom
        else if (positionNum == 7){
            position = new Vector2(xPos + xDistance * 6, yPos - yDistance * 4);
        }
        else if (positionNum == 8){
            position = new Vector2(xPos + xDistance * 5, yPos - yDistance * 4);
        }
        else if (positionNum == 9){
            position = new Vector2(xPos + xDistance * 4, yPos - yDistance * 4);
        }
        else {
            position = new Vector2(xPos + xDistance * 3, yPos - yDistance * 4);
        }

        return position;
    }
}
