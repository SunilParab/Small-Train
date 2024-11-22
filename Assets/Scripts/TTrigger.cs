using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTrigger : MonoBehaviour
{

    public int trainLine;
    public bool isStart;
    bool snappedTo;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver() {
        if (LineDrawer.reference.making) {
            LineDrawer.reference.Snap(this.gameObject);
            snappedTo = true;
        } else if (Input.GetMouseButtonDown(0)) {
            LineDrawer.reference.Activate(trainLine,isStart);
        }
    }

    void OnMouseExit() {
        if (snappedTo) {
            snappedTo = false;
            LineDrawer.reference.UnSnap();
        }
    }

}
