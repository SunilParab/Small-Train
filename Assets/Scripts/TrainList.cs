using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainList : MonoBehaviour
{
    
    public List<LineInfo>[] lineList = new List<LineInfo>[7];

    //Make enumerator (it might be better with ints)

    public static TrainList reference;
    public int availableLines = 3;
    public GameObject TSegment;

    void Awake()
    {
        reference = this;
        for (int i = 0; i < lineList.Length; i++) {
            lineList[i] = new List<LineInfo>();
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

    public void addSegment(LineInfo segment, int targetLine, bool isStart) { //Change the int to enumerator with the line names

        if (targetLine == -1) {
            //If target is -1 create it in the first empty line
            for (int i = 0; i < availableLines; i++) {
                if (lineList[i].Count == 0) {
                    lineList[i].Add(segment);
                    break;
                    print("agadg");
                }
            }

            //ToDo makes thing that says you have no lines

        } else {    
            if (isStart) {
                lineList[targetLine].Insert(0,segment);
            } else {
                lineList[targetLine].Add(segment);
            }

        }

        makeT(segment.segments[0],targetLine,true);
        makeT(segment.segments.Last(),targetLine,false);
        //Go through the target line and generate T's
    }

    void makeT(GameObject target, int targetLine, bool isStart) {

        var curT = Instantiate(TSegment, target.gameObject.transform);

        curT.GetComponent<TTrigger>().trainLine = targetLine;
        curT.GetComponent<TTrigger>().isStart = isStart;

        if (isStart) {
            curT.transform.RotateAround(curT.transform.position, transform.up, 180f);
        }

    }

}
