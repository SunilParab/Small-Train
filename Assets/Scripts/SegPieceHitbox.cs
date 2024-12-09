using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegPieceHitbox : MonoBehaviour
{
    public SegmentInfo segment;
    public int myHalf;

    void OnMouseOver() {
        if (TrainPlacer.reference.isMaking) {
            if (Input.GetMouseButtonDown(0) ) {
                TrainPlacer.reference.stopMaking();

                WeeklyUpgradeManager.reference.trainCount--;

                //Make the train
                GameObject me = GameObject.Instantiate(LineDrawer.reference.train, new Vector3(0, 0, 0.5f), Quaternion.identity);
                me.GetComponent<TrainManager>().PlaceMake(segment,myHalf);

            }
        }
    }

}
