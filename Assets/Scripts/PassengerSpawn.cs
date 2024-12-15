using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PassengerSpawn : MonoBehaviour
{
    //give this (script) list to all the spawned stations
    public List<string> passengersInStation = new();
    public List<GameObject> passengersInStationGO = new();

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
    public int myLine;  

    //passenger gameobjects
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
    
    //overcrowded variables
    public float overCrowdTimer;
    public int passengerLimit;
    public Image timerImage;
    public bool hasTimer = false;
    public Image temporaryImage;

    //interchange
    public bool hasInterchange = false;
    

    // Start is called before the first frame update
    void Start()
    {
        passengerNum = UnityEngine.Random.Range(0, StationSpawnManager.maxShapeNum);
        passengerSpawnTime = UnityEngine.Random.Range(10,21); 
        passengerLimit = 6;

        passengerString = "square";
        //instantiate passenger shape
        RandomizePassengerShape();

        //spawn every 5~20 seconds (random) approximately
        timer = passengerSpawnTime; 
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePassengerIcons();

        //spawn timer
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
                    passengerSpawnTime = UnityEngine.Random.Range(10,21); 
                    timer = passengerSpawnTime;
                    
                }

                //randomize passenger shape based on probability
                if (StationSpawnManager.RandomGaussian(0f, 1f) >= 0.8f && StationSpawnManager.spawnNextPassengerShape)
                {
                    passengerNum = UnityEngine.Random.Range(3, StationSpawnManager.maxShapeNum); //3 ~ cap
                }   
                else
                {   
                    passengerNum = UnityEngine.Random.Range(0, 3); //0 ~ 2
                }

                RandomizePassengerShape();
            }
        }
        
        //overcrowded timer
        if (Time.timeScale > 0)
        {
            
            if (overCrowdTimer <= 0){
                overCrowdTimer = 0;
                /*
                if (hasTimer){
                    Destroy(temporaryImage);
                    hasTimer = false;
                }
                */
            }

            if (passengersInStation.Count > passengerLimit){
                
                //if there is quite a lot of passengers timer will go up
                overCrowdTimer += Time.deltaTime;

                if (!hasTimer){
                    //create timer
                    temporaryImage = Instantiate(timerImage, new Vector2(transform.position.x,transform.position.y), new Quaternion(0, 0, 180, 0),
                        GameObject.FindGameObjectWithTag("TimerCanvas").transform);

                    temporaryImage.GetComponent<TimerSpawn>().station = gameObject;
                    temporaryImage.GetComponent<TimerSpawn>().increasing = true;
                    hasTimer = true;
                }

            }
            else {

                //if theres enough space timer will go down
                overCrowdTimer -= Time.deltaTime;
                if (hasTimer){
                    temporaryImage.GetComponent<TimerSpawn>().increasing = false;
                }
            }

            if (overCrowdTimer >= 45)
            {
                gameEnd();
                //GAME OVER
            }
        }
        
        //interchange
        if (hasInterchange){
            transform.localScale = new Vector3(0.08f,0.08f,0.08f);
            passengerLimit = 18;
        }
    }

    public void AddPassenger(string newPassenger){

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
                passengerString = "star";
                break;
            case 4:
                passengerString = "plus";
                break;
            case 5:
                passengerString = "rhombus";
                break;
            case 6:
                passengerString = "diamond";
                break;
            case 7:
                passengerString = "pie";
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
        for (int i = 0; i < passengersInStationGO.Count; i ++){
            Destroy(passengersInStationGO[i]);
        }
        passengersInStationGO.Clear();

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
            else if (passengersInStation[i].Equals("star")){
                passenger = starPassenger;
            }
            else if (passengersInStation[i].Equals("plus")){
                passenger = plusPassenger;
            }
            else if (passengersInStation[i].Equals("rhombus")){
                passenger = rhombusPassenger;
            }
            else if (passengersInStation[i].Equals("diamond")){
                passenger = diamondPassenger;
            }
            else if (passengersInStation[i].Equals("pie")){
                passenger = piePassenger;
            }
            else if (passengersInStation[i].Equals("eye")){
                passenger = eyePassenger;
            }
            else {
                passenger = pentagonPassenger;
            }

            //Instantiate(passenger, PassengerPosition(i), Quaternion.identity);
            passengersInStationGO.Add(Instantiate(passenger, PassengerPosition(i), Quaternion.identity));
            
        }
    }

    public void UpdatePassengerIcons()
    {
        for (int i = 0; i < passengersInStationGO.Count; i++)
        {
            passengersInStationGO[i].transform.position = PassengerPosition(i);
        }
    }

    //this somehow has to be constantly updated 
    public Vector2 PassengerPosition(int positionNum){
        
        Vector2 position;

        float baseDistance = 0.1f;

        float xPos = transform.position.x + baseDistance;
        float yPos = transform.position.y - baseDistance;

        float xDistance = 0.25f;
        float yDistance = xDistance / 2;

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
        else if (positionNum == 10){
            position = new Vector2(xPos + xDistance * 3, yPos - yDistance * 4);
        }
        else if (positionNum == 11){
            position = new Vector2(xPos + xDistance * 2, yPos - yDistance * 4);
        }
        else if (positionNum == 12){
            position = new Vector2(xPos + xDistance * 1, yPos - yDistance * 4);
        }

        //lower middle
        else if (positionNum == 13){
            position = new Vector2(xPos + xDistance * 0, yPos - yDistance * 5);
        }

        //lower bottom
        else if (positionNum == 14){
            position = new Vector2(xPos + xDistance * 1, yPos - yDistance * 6);
        }
        else if (positionNum == 15){
            position = new Vector2(xPos + xDistance * 2, yPos - yDistance * 6);
        }
        else if (positionNum == 16){
            position = new Vector2(xPos + xDistance * 3, yPos - yDistance * 6);
        }
        else if (positionNum == 17){
            position = new Vector2(xPos + xDistance * 4, yPos - yDistance * 6);
        }
        else if (positionNum == 18){
            position = new Vector2(xPos + xDistance * 5, yPos - yDistance * 6);
        }
        else {
            position = new Vector2(xPos + xDistance * 6, yPos - yDistance * 6);
        }

        return position;
    }

    public static void gameEnd() {
        SceneManager.LoadSceneAsync("EndScreen");
    }

}
