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
        if (LineManager.reference.making) {
            LineManager.reference.Snap(this.gameObject);
            snappedTo = true;
        } else if (Input.GetMouseButtonDown(0) ) {
            LineManager.reference.Activate();
        }
    }

    void OnMouseExit() {
        if (snappedTo) {
            snappedTo = false;
            LineManager.reference.UnSnap();
        }
    }

}
