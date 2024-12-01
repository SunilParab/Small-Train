using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationCollideWater : MonoBehaviour
{
    public static bool waterCollide = false;

    void OnTriggerStay2D(Collider2D water){
        print("water collide!!!!!!");
        if (water.gameObject.CompareTag("Water")){
            waterCollide = true;
        }
    }

    public bool WaterCollide(Collider2D station, Collider2D water, Vector3 stationCenter){

        Vector3 waterClosest = water.ClosestPoint(stationCenter);
        Vector3 stationClosest = station.ClosestPoint(waterClosest);
        return waterClosest == stationClosest;
    }
}
