using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTrigger : MonoBehaviour
{

    public int trainLine;
    public bool isStart;
    bool snappedTo;

    public GameObject myStation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver() {
        if (!LineDrawer.reference.making && Input.GetMouseButtonDown(0)) {
            if (LineList.reference.lineList[trainLine].isLooped) {
                LineDrawer.reference.Activate(trainLine,isStart,LineList.reference.lineList[trainLine].StationsInLine[LineList.reference.lineList[trainLine].StationsInLine.Count-2]);
            } else {
                LineDrawer.reference.Activate(trainLine,isStart,myStation);
            }
        }
    }

}
