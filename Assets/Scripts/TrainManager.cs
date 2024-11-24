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

    private int segmentIndexStart;
    private int segmentIndexEnd;
    private int segmentPercentageCount = 0;

    private LineInfo[] lineInfos;
    private int[] segmentNum;
    private int ourSegment = 0;

    // Start is called before the first frame update
    void Start()
    {

        lineScript = LineList.reference;

        lineInfos = lineScript.lineList;

        //Count the total number of gameObjects in this line segment
        segmentNum = new int[lineInfos[myLine].LineSegments.Count];
        for (int i = 0; i < lineInfos[myLine].LineSegments.Count; i++)
        {
            segmentNum[i] = lineInfos[myLine].LineSegments[i].segments.Count;
        }

        x = this.transform.position.x;
        y = this.transform.position.y;


        float startXPos = lineInfos[myLine].LineSegments[0].segments[segmentIndexStart].transform.position.x;
        float startYPos = lineInfos[myLine].LineSegments[0].segments[segmentIndexStart].transform.position.y;
        float endXpos = lineInfos[myLine].LineSegments[0].segments[segmentIndexEnd].transform.position.x;
        float endYpos = lineInfos[myLine].LineSegments[0].segments[segmentIndexEnd].transform.position.y;

        xDelta = Mathf.Lerp(startXPos, endXpos, 0.1f);
        yDelta = Mathf.Lerp(startYPos, endYpos, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        x += xDelta * Time.deltaTime;
        y += yDelta * Time.deltaTime;

        this.transform.position = new Vector3(x, y, 0);

        segmentPercentageCount++;
        //If segmentPercentageCount is 10, then go to the next gameObject segment. If we're at the end of this gameObject segment,
        //go to the next segment line. If we're out of segments, don't run this
        if (segmentPercentageCount == 10 && segmentIndexEnd < segmentNum[ourSegment] && ourSegment < segmentNum.Length)
        {
            segmentIndexStart++;
            segmentIndexEnd++;
            nextSegment(ourSegment);
        }
    }

    void nextSegment(int a)
    {
        float startXPos = lineInfos[myLine].LineSegments[0].segments[segmentIndexStart].transform.position.x;
        float startYPos = lineInfos[myLine].LineSegments[0].segments[segmentIndexStart].transform.position.y;
        float endXpos = lineInfos[myLine].LineSegments[0].segments[segmentIndexEnd].transform.position.x;
        float endYpos = lineInfos[myLine].LineSegments[0].segments[segmentIndexEnd].transform.position.y;

        xDelta = Mathf.Lerp(startXPos, endXpos, 0.1f);
        yDelta = Mathf.Lerp(startYPos, endYpos, 0.1f);

        ourSegment++;
    }
}
