using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineList : MonoBehaviour
{
    
    public LineInfo[] lineList = new LineInfo[7];

    //Make enumerator (it might be better with ints)

    public static LineList reference;
    public int availableLines = 3;
    public GameObject TSegment;

    void Awake()
    {
        reference = this;
        for (int i = 0; i < lineList.Length; i++) {
            lineList[i] = ScriptableObject.CreateInstance<LineInfo>();
            lineList[i].trainLine = i;
            lineList[i].LineSegments = new List<SegmentInfo>();
            lineList[i].TSegment = this.TSegment;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addSegment(SegmentInfo segment, int targetLine, bool isStart) { //Change the int to enumerator with the line names

        if (targetLine == -1) {
            //If target is -1 create it in the first empty line
            bool spotFound = false;
            for (int i = 0; i < availableLines; i++) {
                if (lineList[i].LineSegments.Count == 0) {
                    lineList[i].addSegment(segment,isStart);
                    spotFound = true;
                    targetLine = i;
                    break;

                }
            }

            if (!spotFound) {
                Destroy(segment.gameObject);
            }
            //ToDo makes thing that says you have no lines

        } else {    
            lineList[targetLine].addSegment(segment,isStart);
        }

        segment.myLine = targetLine;
        segment.PutStationsInList(LineList.reference.lineList[targetLine].StationsInLine);

        //Lines need to be stored as an object that contains the list and the T references
        /*Destroy(segment.startT);
        segment.startT = MakeT(segment.segments[0],targetLine,true);
        Destroy(segment.endT);
        segment.endT = MakeT(segment.segments.Last(),targetLine,false);*/
        //Go through the target line and generate T's
    }

}
