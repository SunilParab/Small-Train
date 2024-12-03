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
        //put every station in the segment in the list
        var segmentList = LineList.reference.lineList[myLine].LineSegments;
        for (int i = 0; i < segmentList.Count; i ++){
            stationList.Add(segmentList[i].startStation);
        }
        stationList.Add(segmentList[segmentList.Count-1].endStation);  
    }

}
