using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class SegmentInfo : MonoBehaviour
{

    public float firstAngle;
    public float endAngle;

    public int startCount;
    public int endCount;

    public GameObject startStation;
    public GameObject endStation;

    public LineRenderer lineRenderer;
    public int myLine;

    public void PlacholderName(){
        startStation.GetComponent<PassengerSpawn>();
    }

    public void PutStationsInList(List<GameObject> stationList){

        //if a line connects to a station,        
        //put every station in the line in the list
        var segmentList = LineList.reference.lineList[myLine].LineSegments;
        for (int i = 0; i < segmentList.Count; i ++){
            
            //ADD THE STATIONS (except for the last one)
            //add this station into line's list
            stationList.Add(segmentList[i].startStation);
            
            //add this line into station's list
            segmentList[i].startStation.GetComponent<PassengerSpawn>().connectedLines.Add(myLine);
        }
        //ADD THE LAST STATION
        //add this station into line's list
        stationList.Add(segmentList[segmentList.Count-1].endStation);  

        //add this line into station's list
        segmentList[segmentList.Count-1].endStation.GetComponent<PassengerSpawn>().connectedLines.Add(myLine);
    }

    public void FlipVariables() {
        float temp1 = firstAngle;
        firstAngle = (endAngle + 180)%360;
        endAngle = (temp1 + 180)%360;

        int temp2 = startCount;
        startCount = endCount;
        endCount = temp2;
    }

}
