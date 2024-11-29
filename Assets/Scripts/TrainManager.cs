using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class TrainManager : MonoBehaviour
{

    public LineList lineScript;

    public int myLine;

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


    private LineInfo[] lineInfos;
    private int[] segmentNum;
    private int ourSegment = 0;

    // Start is called before the first frame update
    void Start()
    {

        lineScript = LineList.reference;
        lineInfos = lineScript.lineList;

        x = transform.position.x;
        y = transform.position.y;


        //set start and cur target TODO set curSegment
        curStart = curSegment.lineRenderer.GetPosition(0+curHalf);
        curTarget = curSegment.lineRenderer.GetPosition(1+curHalf);
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
                        curSegment = lineInfos[myLine].LineSegments[curSegmentIndex-1];
                    } else {
                        curSegment = lineInfos[myLine].LineSegments[curSegmentIndex+1];
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

        //TODO check if distance is more than half-segment length

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

    void slowDown() {

    }

    void speedUp() {

    }

    void pickupPassengers() {

    }

    bool checkLineEnd() {
        return false;
    }

}
