using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class TrainManager : MonoBehaviour
{

    public LineList lineScript;

    //what line the train is in
    public int myLine;

    //what station the train arrived at
    public int myStation;
    
    //content of train as string
    public List<string> insideTrain = new();
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


    //Iterating through all the segments in these arrays
    private LineInfo[] lineInfos;
    private int[] segmentNum; //Stores an array that is the number of line segments long, and each value is the number of track pieces
    private int ourPiece = 0; //What track piece are we on in this line segment
    private int ourSegment = 0; //What line segment are we on in this train line

    //Position of a track piece and the next track piece
    private float startXPos;
    private float startYPos;
    private float endXPos;
    private float endYPos;

    // Start is called before the first frame update
    void Start()
    {

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

    // Update is called once per frame
    void Update()
    {

        distance += speed * Time.deltaTime;

        if (Mathf.Sqrt(Mathf.Pow(curTarget.y-curStart.y,2)+Mathf.Pow(curTarget.x-curStart.x,2)) < distance) {
            distance -= Mathf.Sqrt(Mathf.Pow(curTarget.y-curStart.y,2)+Mathf.Pow(curTarget.x-curStart.x,2));

            if (curHalf == 1) {
                
                var turningAround = false;
                var curSegmentIndex = lineInfos[myLine].LineSegments.IndexOf(curSegment);

                //Check if its at the end of the line
                if (reversed) {
                    if (curSegmentIndex == 0) {
                        reversed = false;
                        turningAround = true;
                    }
                } else {
                    if (lineInfos[myLine].LineSegments.Count - 1 == curSegmentIndex) {
                        reversed = true;
                        turningAround = true;
                    }
                }

                //Get next segment
                if (!turningAround) {
                    if (reversed) {

                        //choose station
                        myStation = LineList.reference.lineList[myLine].StationsInLine.IndexOf(curSegment.GetComponent<SegmentInfo>().startStation);
                        curSegment = lineInfos[myLine].LineSegments[curSegmentIndex-1];

                        //grab passengers
                        PickupPassengers();
                    } else {

                        //choose station
                        myStation = LineList.reference.lineList[myLine].StationsInLine.IndexOf(curSegment.GetComponent<SegmentInfo>().endStation);
                        curSegment = lineInfos[myLine].LineSegments[curSegmentIndex+1];

                        //grab passengers
                        PickupPassengers();
                    }
                }


                curHalf = 0;
            } else {
                curHalf = 1;
            }

            if (!reversed) {
                curStart = curSegment.lineRenderer.GetPosition(0+curHalf);
                curTarget = curSegment.lineRenderer.GetPosition(1+curHalf);
            } else {
                curStart = curSegment.lineRenderer.GetPosition(2-curHalf);
                curTarget = curSegment.lineRenderer.GetPosition(1-curHalf);
            }
        }


        //Move the train in the direction by _distance_ amount
        var directionVector = Vector3.Normalize(curTarget - curStart);
        transform.position = curStart + directionVector * distance;

    }

    void nextSegment(int a)
    {/*
        float startXPos = lineInfos[myLine].LineSegments[0].segments[segmentIndexStart].transform.position.x;
        float startYPos = lineInfos[myLine].LineSegments[0].segments[segmentIndexStart].transform.position.y;
        float endXpos = lineInfos[myLine].LineSegments[0].segments[segmentIndexEnd].transform.position.x;
        float endYpos = lineInfos[myLine].LineSegments[0].segments[segmentIndexEnd].transform.position.y;

        xDelta = Mathf.Lerp(startXPos, endXpos, 0.1f);
        yDelta = Mathf.Lerp(startYPos, endYpos, 0.1f);

        */ourSegment++;
    }

    void SlowDown() {

    }

    void SpeedUp() {

    }

    void PickupPassengers() {
        //if train arrives and has enough space:
        //if (insideTrain.Add.Count == 6) that means its full

        

        //get station its reaching as GameObject
        var station = LineList.reference.lineList[myLine].StationsInLine[myStation];

        //myStation does not exist right now!!!

        //get station shape as string
        var stationString = station.GetComponent<PassengerSpawn>().stationString;

        //check if the line has a station that is connected to a 
        //different line that has passenger's shaped station
        //or
        //if connectedLines[i] has stations[j] with (passengrList[k] == station):
        
        //loop for lines
        for (int i = 0; i < station.GetComponent<PassengerSpawn>().connectedLines.Count; i++){
            
            //loop for stations in that line
            for (int j = 0; j < LineList.reference.lineList[i].StationsInLine.Count; j ++){
                
                //loop for passengers in that station in that line
                for (int k = 0; k < PassengerListInStationInLine(j,i).Count; k ++){
                    if (PassengerListInStationInLine(j,i)[k].Equals(stationString)){
                        
                        insideTrain.Add(PassengerListInStationInLine(i,j)[k]);
                        return;
                    }
                }
            }
        }

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

}
