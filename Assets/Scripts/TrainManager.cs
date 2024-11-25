using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class TrainManager : MonoBehaviour
{

    public LineList lineScript;

    public int myLine;

    public float x;
    public float y;

    public float xDelta;
    public float yDelta;

    //Track piece indexes and % of the way between the two
    private int segmentIndexStart;
    private int segmentIndexEnd;
    private float segmentPercentageCount = 0;

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

        //Count the total number of gameObjects in this line segment, then the next, and so on
        segmentNum = new int[lineInfos[myLine].LineSegments.Count];
        for (int i = 0; i < lineInfos[myLine].LineSegments.Count; i++)
        {
            segmentNum[i] = lineInfos[myLine].LineSegments[i].segments.Count;
        }

        x = this.transform.position.x;
        y = this.transform.position.y;

        segmentIndexEnd = segmentIndexStart + 1;

        // + lineInfos[myLine].LineSegments[0].segments[segmentIndexEnd].transform.parent.position.x;
        startXPos = lineInfos[myLine].LineSegments[0].segments[segmentIndexStart].transform.position.x;
        startYPos = lineInfos[myLine].LineSegments[0].segments[segmentIndexStart].transform.position.y;
        endXPos = lineInfos[myLine].LineSegments[0].segments[segmentIndexEnd].transform.position.x;
        endYPos = lineInfos[myLine].LineSegments[0].segments[segmentIndexEnd].transform.position.y;

        xDelta = (endXPos - startXPos) / 10;
        yDelta = (endYPos - startYPos) / 10;
    }

    // Update is called once per frame
    void Update()
    { 
        updateSegmentNum();

        x += xDelta * Time.deltaTime * 20;
        y += yDelta * Time.deltaTime * 20;

        this.transform.position = new Vector3(x, y, 0);

        Debug.Log("\nx and y deltas: " + xDelta + " " + yDelta + 
            "\nour track piece index: " + ourPiece +
            "\nour segment index: " + ourSegment);

        //If we're changing our x, base our segmentPercentageCount on the % of the way from startX to endX, else base it on Y
        if (xDelta != 0) segmentPercentageCount += (x - startXPos) / (endXPos - startXPos);
        else segmentPercentageCount += (y - startYPos) / (endYPos - startYPos);
        //Vector2.Distance(transform.position, endPos)<.1f

        //If segmentPercentageCount is >= 1, then go to the next gameObject segment. If we're at the end of this line segment,
        //go to the next line segment. If we're out of segments, don't run this
        if (segmentPercentageCount >= 1 && segmentIndexEnd < segmentNum[ourSegment] - 1)
        {
            segmentIndexStart++;
            segmentIndexEnd++;
            ourPiece++;

            segmentPercentageCount = 0;

            nextSegment();



        }
        else if (segmentPercentageCount >= 1 && ourPiece >= segmentNum.Length && ourSegment < lineInfos[myLine].LineSegments.Count)
        {
            ourPiece = 0;
            segmentIndexStart = ourPiece;
            segmentIndexEnd = ourPiece + 1;

            segmentPercentageCount = 0;

            ourSegment++;

            nextSegment();


        } 
    }

    void nextSegment()
    {
        startXPos = lineInfos[myLine].LineSegments[ourSegment].segments[segmentIndexStart].transform.position.x;
        startYPos = lineInfos[myLine].LineSegments[ourSegment].segments[segmentIndexStart].transform.position.y;
        endXPos = lineInfos[myLine].LineSegments[ourSegment].segments[segmentIndexEnd].transform.position.x;
        endYPos = lineInfos[myLine].LineSegments[ourSegment].segments[segmentIndexEnd].transform.position.y;

        xDelta = (endXPos - startXPos) / 10;
        yDelta = (endYPos - startYPos) / 10;

        ourPiece++;
    }

    void updateSegmentNum()
    {
        //Update the parameters of the train line if it is updated
        segmentNum = new int[lineInfos[myLine].LineSegments.Count];
        for (int i = 0; i < lineInfos[myLine].LineSegments.Count; i++)
        {
            segmentNum[i] = lineInfos[myLine].LineSegments[i].segments.Count;
        }
    }

    /*
    int currentSegmentIndex
    public void AddLineInfo(LineInfo lineInfo)
    {
        //append lineInfo to line array
    }*/
}
