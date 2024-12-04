using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeGenerator : MonoBehaviour
{

    public static float segLength = 0.5f;
    public static Collider2D segmentChecker;
    public static GameObject bridgePrefab;
    public static StationSpawnManager spawner;

    //Check everypoint along the line renderer (intervals the size of a line segment-segment)
    //Use the collidewater check

    //Find the first and last position of a water collision

    //CHECK IF THE LINE TURNS SO THE BRIDGE GOES ON ITS MIDPOINT

    //put another line on top of the current one with the bridge texture
    //give the line segment reference to its bridge so they can be destroyed together


    //TODO edge case of water being 1 box big !!!!  set the end point to one segment-segment after once the start point is found

    public static void BridgeGen(SegmentInfo segment)
    {
        bool startFound = false;
        Vector3 bridgeStart = new Vector3();
        Vector3 bridgeEnd = new Vector3();
        bool startOnFirstHalf = false;
        bool endOnSecondHalf = false;
        float firstAngle = segment.firstAngle;
        float endAngle = segment.endAngle;
        float startx = segment.lineRenderer.GetPosition(0).x;
        float starty = segment.lineRenderer.GetPosition(0).y;
        float midx = segment.lineRenderer.GetPosition(1).x;
        float midy = segment.lineRenderer.GetPosition(1).y;
        float endx = segment.lineRenderer.GetPosition(2).x;
        float endy = segment.lineRenderer.GetPosition(2).y;

        //Keep counting segments in the first direction until the angle from the newest segment to the endpoint equals the final angle
        float curx = startx;
        float cury = starty;


        bool firstHalf = true;
        int counter = 0;

        while (firstHalf && (curx != endx || cury != endy) && counter < 100) {

            switch (firstAngle) {
                case 0:
                    curx += segLength;
                    break;
                case 45:
                    curx += segLength;
                    cury += segLength;
                    break;
                case 90:
                    cury += segLength;
                    break;
                case 135:
                    curx -= segLength;
                    cury += segLength;
                    break;
                case 180:
                    curx -= segLength;
                    break;
                case 225:
                    curx -= segLength;
                    cury -= segLength;
                    break;
                case 270:
                    cury -= segLength;
                    break;
                case 315:
                    curx += segLength;
                    cury -= segLength;
                    break;
            }

            if (StationCollideWater.IsCollidingWater(spawner.map.GetComponent<Collider2D>(), segmentChecker, new Vector3(curx,cury,0))) {
                if (!startFound) {
                    startFound = true;
                    startOnFirstHalf = true;
                    bridgeStart = new Vector2(curx,cury);
                    bridgeEnd = new Vector2(curx,cury);

                } else {
                    bridgeEnd = new Vector2(curx,cury);
                }
            }

            if (curx == midx && cury == midy) {
                firstHalf = false;
            }

            counter++;
        }


        counter = 0;
        while ((curx != endx || cury != endy) && counter < 100) {

            switch (endAngle) {
                case 0:
                    curx += segLength;
                    break;
                case 45:
                    curx += segLength;
                    cury += segLength;
                    break;
                case 90:
                    cury += segLength;
                    break;
                case 135:
                    curx -= segLength;
                    cury += segLength;
                    break;
                case 180:
                    curx -= segLength;
                    break;
                case 225:
                    curx -= segLength;
                    cury -= segLength;
                    break;
                case 270:
                    cury -= segLength;
                    break;
                case 315:
                    curx += segLength;
                    cury -= segLength;
                    break;
            }

            if (StationCollideWater.IsCollidingWater(spawner.map.GetComponent<Collider2D>(), segmentChecker, new Vector3(curx,cury,0))) {
                if (!startFound) {
                    startFound = true;
                    bridgeStart = new Vector2(curx,cury);
                    bridgeEnd = new Vector2(curx,cury);

                } else {
                    endOnSecondHalf = true;
                    bridgeEnd = new Vector2(curx,cury);
                }
            }

            counter++;
        }

        if (startFound) {
            var newBridge = Instantiate(bridgePrefab,segment.transform);
            var bridgeRenderer = newBridge.GetComponent<LineRenderer>();
            if (startOnFirstHalf && endOnSecondHalf) {
                Vector3[] linePoints = new Vector3[3];
                linePoints[0] = bridgeStart;
                linePoints[1] = new Vector3(midx,midy,0);
                linePoints[2] = bridgeEnd;
                bridgeRenderer.positionCount = 3;
                bridgeRenderer.SetPositions(linePoints);
            } else {
                Vector3[] linePoints = new Vector3[2];
                linePoints[0] = bridgeStart;
                linePoints[1] = bridgeEnd;
                bridgeRenderer.positionCount = 2;
                bridgeRenderer.SetPositions(linePoints);
            }
        }

    }



}
