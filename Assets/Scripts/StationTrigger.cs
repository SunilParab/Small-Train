using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationTrigger : MonoBehaviour
{

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
        } else if (Input.GetMouseButtonDown(0) ) {
            LineDrawer.reference.Activate(-1,true); //-1 Means new line
        }
    }

    void OnMouseExit() {
        if (snappedTo) {
            snappedTo = false;
            LineDrawer.reference.UnSnap();
        }
    }

}
