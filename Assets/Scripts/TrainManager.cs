using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UIElements;
using static System.Collections.Specialized.BitVector32;

public class TrainManager : MonoBehaviour
{

    public LineList lineScript;

    //what line the train is in
    public int myLine;

    //what station the train arrived at
    public int myStation;
    
    //content of train as string
    public List<string> insideTrain = new();
    private int insideTrainMax = 6;
    public float x;
    public float y;

    public float speed; //How much to move per second
    float distance; //How far along current half-segment the train has moved
    Vector3 curStart; //Start of current half-segment
    Vector3 curTarget; //End of current half-segment

    //Storing current position
    SegmentInfo curSegment;
    int curHalf;

    bool reversed; //If the train is going backwards
    bool goingToStop;

    //When nearing a station/leaving a station
    public bool decelerating = false;
    public bool accelerating = false;
    float pickUpPassengersTimer = 1000;
    public bool leavingStation = false;


    //Iterating through all the segments in these arrays
    private LineInfo[] lineInfos;


    //Remove all of these useless variables at some point
    private int[] segmentNum; //Stores an array that is the number of line segments long, and each value is the number of track pieces
    private int ourPiece = 0; //What track piece are we on in this line segment
    private int ourSegment = 0; //What line segment are we on in this train line

    //Position of a track piece and the next track piece
    private float startXPos;
    private float startYPos;
    private float endXPos;
    private float endYPos;

    // I replaced start with this, it just instantiate the variables
    public void RegularMake()
    {
        //At the very start, the stationIndex that is next will always be 1
        myStation = 1;

        lineScript = LineList.reference;
        lineInfos = lineScript.lineList;

        x = transform.position.x;
        y = transform.position.y;

        //set start and cur target
        curSegment = lineInfos[myLine].LineSegments[0];

        curStart = curSegment.lineRenderer.GetPosition(0+curHalf);
        curTarget = curSegment.lineRenderer.GetPosition(1+curHalf);

        SpriteRenderer SR = this.gameObject.GetComponent<SpriteRenderer>();
        switch (myLine)
        {
            case 0:
                SR.color = Color.yellow;
                break;
            case 1:
                SR.color = Color.red;
                break;
            case 2:
                SR.color = Color.blue;
                break;
            case 3:
                SR.color = Color.cyan;
                break;
            case 4:
                SR.color = Color.green;
                break;
            case 5:
                SR.color = Color.magenta;
                break;
            case 6:
                SR.color = Color.white;
                break;

        }
    }

    // Make function for placing train on specific segement
    public void PlaceMake(SegmentInfo segment, int targetHalf)
    {

        myLine = segment.myLine;

        lineScript = LineList.reference;
        lineInfos = lineScript.lineList;

        x = transform.position.x;
        y = transform.position.y;

        //set start and cur target
        curSegment = segment;


        curStart = curSegment.lineRenderer.GetPosition(0+targetHalf);
        curTarget = curSegment.lineRenderer.GetPosition(1+targetHalf);
        curHalf = targetHalf;


        curStart = curSegment.lineRenderer.GetPosition(0+curHalf);
        curTarget = curSegment.lineRenderer.GetPosition(1+curHalf);

        SpriteRenderer SR = this.gameObject.GetComponent<SpriteRenderer>();
        switch (myLine)
        {
            case 0:
                SR.color = Color.yellow;
                break;
            case 1:
                SR.color = Color.red;
                break;
            case 2:
                SR.color = Color.blue;
                break;
            case 3:
                SR.color = Color.cyan;
                break;
            case 4:
                SR.color = Color.green;
                break;
            case 5:
                SR.color = Color.magenta;
                break;
            case 6:
                SR.color = Color.white;
                break;

        }

        //Set start position
        transform.position = curStart;// + directionVector * distance

    }

    // Update is called once per frame
    void Update()
    {

        distance += speed * Time.deltaTime;

        RecalibrateVariables();

        //Detect distance to next station
        var nextStation = LineList.reference.lineList[myLine].StationsInLine[myStation]; //TODO: Fix nextStation not being set properly
        float distanceToNextStation = Vector3.Distance(this.transform.position, nextStation.transform.position);
        //Debug.Log("Station we're going to: " + myStation + "\tDistance: " + distanceToNextStation + "\tSpeed: " + speed);
        if (distanceToNextStation <= 1 && !leavingStation)
        {
            decelerating = true;
            accelerating = false;
        }
        else if (leavingStation)
        {
            if (distanceToNextStation > 1)
            {
                leavingStation = false;
            }
        }

        //Decelerating
        if (decelerating)
        {
            speed = (1 - (Time.deltaTime * 0.8f)) * speed;
        }

        //Picking up passengers
        if (distanceToNextStation <= 0.005f && !accelerating)
        {
            speed = 0;
            if (pickUpPassengersTimer <= 0)
            {
                //Debug.Log("Picking up a passenger");
                if (!DropOffPassengers())
                {
                    if (!PickupPassengers())
                    {
                        leavingStation = true;
                    }
                }
                pickUpPassengersTimer = 250;
            }
            pickUpPassengersTimer -= Time.deltaTime * 500;
        } else
        {
            pickUpPassengersTimer = 500;

            //Accelerating
            if (accelerating) speed = Math.Min((1 + (Time.deltaTime * 0.8f)) * speed + 0.001f, 1);
            if (speed == 1) accelerating = false;
        }

        if (leavingStation)
        {
            decelerating = false;
            accelerating = true;
        }

        //Move the train in the direction by _distance_ amount
        var directionVector = Vector3.Normalize(curTarget - curStart);
        transform.position = curStart + directionVector * distance;

    }

    void RecalibrateVariables()
    {
        //If the distance that I've traveled is greater than the length of the half segment...
        if (Mathf.Sqrt(Mathf.Pow(curTarget.y - curStart.y, 2) + Mathf.Pow(curTarget.x - curStart.x, 2)) < distance)
        {
            //Debug.Log("Recalibrating Variables");
            distance -= Mathf.Sqrt(Mathf.Pow(curTarget.y - curStart.y, 2) + Mathf.Pow(curTarget.x - curStart.x, 2));

            if (curHalf == 1)
            {

                var turningAround = false;


                var curSegmentIndex = lineInfos[myLine].LineSegments.IndexOf(curSegment);

                if (curSegmentIndex == -1)
                {

                }

                //Check if its at the end of the line
                if (reversed)
                {
                    if (curSegmentIndex == 0)
                    {
                        reversed = false;
                        turningAround = true;
                    }
                }
                else
                {
                    if (lineInfos[myLine].LineSegments.Count - 1 == curSegmentIndex)
                    {
                        reversed = true;
                        turningAround = true;
                    }
                }

                //Get next segment
                if (!turningAround)
                {
                    if (reversed)
                    {
                        //choose station
                        curSegment = lineInfos[myLine].LineSegments[curSegmentIndex - 1];
                        myStation = LineList.reference.lineList[myLine].StationsInLine.IndexOf(curSegment.GetComponent<SegmentInfo>().startStation);
                        Debug.Log(curSegmentIndex-1);

                    }
                    else
                    {
                        //choose station
                        curSegment = lineInfos[myLine].LineSegments[curSegmentIndex + 1];
                        myStation = LineList.reference.lineList[myLine].StationsInLine.IndexOf(curSegment.GetComponent<SegmentInfo>().endStation);
                        Debug.Log(curSegmentIndex+1);

                    }
                } 
                else
                {
                    if (reversed)
                    {
                        //choose station
                        //curSegment = lineInfos[myLine].LineSegments[curSegmentIndex - 1];
                        myStation = LineList.reference.lineList[myLine].StationsInLine.IndexOf(curSegment.GetComponent<SegmentInfo>().startStation);
                        //Debug.Log(curSegmentIndex-1);

                    }
                    else
                    {
                        //choose station
                        //curSegment = lineInfos[myLine].LineSegments[curSegmentIndex + 1];
                        myStation = LineList.reference.lineList[myLine].StationsInLine.IndexOf(curSegment.GetComponent<SegmentInfo>().endStation);
                        //Debug.Log(curSegmentIndex+1);

                    }
                }


                curHalf = 0;
            }
            else
            {
                curHalf = 1;
            }

            if (!reversed)
            {
                curStart = curSegment.lineRenderer.GetPosition(0 + curHalf);
                curTarget = curSegment.lineRenderer.GetPosition(1 + curHalf);
            }
            else
            {
                curStart = curSegment.lineRenderer.GetPosition(2 - curHalf);
                curTarget = curSegment.lineRenderer.GetPosition(1 - curHalf);
            }
        }
    }

    bool DropOffPassengers()
    {
        var station = LineList.reference.lineList[myLine].StationsInLine[myStation];
        var stationString = station.GetComponent<PassengerSpawn>().stationString;

        for (int i = 0; i < insideTrain.Count; i++) 
        {
            if (insideTrain[i].Equals(stationString))
            {
                insideTrain.Remove(insideTrain[i]);
                return true;
            }
        }

        return false;

    }

    bool PickupPassengers() {
        //if train arrives and has enough space:
        //if (insideTrain.Add.Count == 6) that means its full

        

        //get station its reaching as GameObject
        var station = LineList.reference.lineList[myLine].StationsInLine[myStation];
        var passengersInNextStationGO = station.GetComponent<PassengerSpawn>().passengersInStationGO;
        var passengersInNextStationString = station.GetComponent<PassengerSpawn>().passengersInStation;

        //myStation does not exist right now!!!

        //get station shape as string
        var stationString = station.GetComponent<PassengerSpawn>().stationString;

        //check if the line has a station that is connected to a 
        //different line that has passenger's shaped station
        //or
        //if connectedLines[i] has stations[j] with (passengrList[k] == station):

        /*Debug.Log("STATIONS:");
        for (int i = 0; i < LineList.reference.lineList[myLine].StationsInLine.Count; i++)
        {
            Debug.Log(LineList.reference.lineList[myLine].StationsInLine[i].GetComponent<PassengerSpawn>().stationString);
        }
        Debug.Log("PASSENGERS WAITING AT OUR STATION:");
        for (int i = 0; i < LineList.reference.lineList[myLine].StationsInLine.Count; i++)
        {
            
        }*/


        //loop for lines
        for (int i = 0; i < station.GetComponent<PassengerSpawn>().connectedLines.Count; i++){
            //Debug.Log(station.GetComponent<PassengerSpawn>().connectedLines.Count);

            //loop for stations in that line
            for (int j = 0; j < LineList.reference.lineList[station.GetComponent<PassengerSpawn>().connectedLines[i]].StationsInLine.Count; j++){
                stationString = LineList.reference.lineList[station.GetComponent<PassengerSpawn>().connectedLines[i]].StationsInLine[j].GetComponent<PassengerSpawn>().stationString;
                
                //loop for passengers in my station in my line
                for (int k = 0; k < PassengerListInStationInLine(myStation, myLine).Count; k++){
                    //Debug.Log("Station we're comparing: " + stationString + "\tPassenger we're comparing: " + PassengerListInStationInLine(myStation, myLine)[k] +
                        //"\tTotal number of stations in this line: " + LineList.reference.lineList[station.GetComponent<PassengerSpawn>().connectedLines[i]].StationsInLine.Count);
                    if (PassengerListInStationInLine(myStation, myLine)[k].Equals(stationString) && insideTrain.Count < insideTrainMax){
                        //Let's pick up this passenger!
                        //Debug.Log("Picked up passenger " + passengersInNextStationString[k]);
                        insideTrain.Add(PassengerListInStationInLine(myStation, myLine)[k]);
                        PassengerListInStationInLine(myStation, myLine).Remove(PassengerListInStationInLine(myStation, myLine)[k]);
                        Destroy(PassengerListInStationInLineGO(myStation, myLine)[k]);
                        PassengerListInStationInLineGO(myStation, myLine).Remove(PassengerListInStationInLineGO(myStation, myLine)[k]);
                        return true;
                    }
                }
            }
        }

        //We need to get going!
        return false;

        /*
        //get list of passenger shape as string
        var passengerList = LineList.reference.lineList[myLine].StationsInLine[myStation]
        .GetComponent<PassengerSpawn>().passengersInStation;

        /*
        //check if the line has passenger's shaped station
        for (int i = 0; i < passengerList.Count; i ++)
        {
            if (passengerList[i].Equals(stationString)){
                
                insideTrain.Add(passengerList[i]);
            }
        }
        */
    }

    bool CheckLineEnd() {
        return false;
    }


    //return list of passengers in specific station in specific line
    public List<string> PassengerListInStationInLine(int chooseStation, int chooseLine){
        
        var list = LineList.reference.lineList[chooseLine].StationsInLine[chooseStation]
        .GetComponent<PassengerSpawn>().passengersInStation;

        return list;
    }

    //return list of passenger GameObjects in specific station in specific line
    public List<GameObject> PassengerListInStationInLineGO(int chooseStation, int chooseLine)
    {

        var list = LineList.reference.lineList[chooseLine].StationsInLine[chooseStation]
        .GetComponent<PassengerSpawn>().passengersInStationGO;

        return list;
    }


}
