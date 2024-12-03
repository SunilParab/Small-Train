using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineInfo : ScriptableObject
{
    public int trainLine;
    public List<SegmentInfo> LineSegments;
    public List<GameObject> StationsInLine;
    public GameObject TSegment;

    public GameObject startT;
    public GameObject endT;


    public void addSegment(SegmentInfo segment, bool isStart) { //Change the int to enumerator with the line names

        if (isStart) {
            LineSegments.Insert(0,segment);
        } else {
            LineSegments.Add(segment);
        }

        //Lines need to be stored as an object that contains the list and the T references
        Destroy(startT);
        startT = MakeT(LineSegments[0].lineRenderer.GetPosition(0),true,LineSegments[0].startStation);
        Destroy(endT);
        endT = MakeT(LineSegments.Last().lineRenderer.GetPosition(2),false,LineSegments.Last().endStation);
        //Go through the target line and generate T's
    }

    GameObject MakeT(Vector3 targetPiece, bool isStart, GameObject givenStation) {

        var curT = Instantiate(TSegment, targetPiece, Quaternion.identity);

        curT.GetComponent<TTrigger>().trainLine = trainLine;
        curT.GetComponent<TTrigger>().isStart = isStart;
        curT.GetComponent<TTrigger>().myStation = givenStation;

        if (isStart) {
            curT.transform.RotateAround(curT.transform.position, Vector3.up, 180f);
        }
        return curT;

    }

}
