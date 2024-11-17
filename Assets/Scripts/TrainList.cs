using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainList : MonoBehaviour
{
    
    public List<LineInfo>[] lineList = new List<LineInfo>[7];

    //Make enumerator

    public static TrainList reference;

    public GameObject TSegment;

    void Awake()
    {
        reference = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addSegment(LineInfo segment, int targetLine) { //Change the int to enumerator with the line names
        //Add the segment to the target line
            //If target is -1 create it in the first empty line
        
        //Go through the target line and generate T's
    }

    void makeT(LineInfo target, bool isStart) {
        //If is start grab the first child


        //T goes directly on top of chosen segment, copies its rotation
            //If !isStart then angle - 180

        //give the T its trainLine
    }

}
