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
        MakeT(LineSegments[0],true,LineSegments[0].startStation);
        Destroy(endT);
        MakeT(LineSegments.Last(),false,LineSegments.Last().endStation);
        //Go through the target line and generate T's
    }

    public void MakeT(SegmentInfo targetPiece, bool isStart, GameObject givenStation) {

        Vector3 targetPosition;
        if (isStart) {
            targetPosition = targetPiece.lineRenderer.GetPosition(0);
        } else {
            targetPosition = targetPiece.lineRenderer.GetPosition(2);
        }

        var curT = Instantiate(TSegment, targetPosition, Quaternion.identity);

        curT.GetComponent<TTrigger>().trainLine = trainLine;
        curT.GetComponent<TTrigger>().isStart = isStart;
        curT.GetComponent<TTrigger>().myStation = givenStation;

        if (isStart) {
            curT.transform.RotateAround(curT.transform.position, Vector3.forward, 180f + targetPiece.firstAngle - 90);
            curT.GetComponent<SpriteRenderer>().color = targetPiece.lineRenderer.startColor;
        } else {
            curT.transform.RotateAround(curT.transform.position, Vector3.forward, targetPiece.endAngle - 90);
            curT.GetComponent<SpriteRenderer>().color = targetPiece.lineRenderer.endColor;
        }

        

        if (isStart) {
            startT = curT;
        } else {
            endT = curT;
        }

    }

}
