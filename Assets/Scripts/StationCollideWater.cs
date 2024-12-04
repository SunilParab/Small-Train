using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationCollideWater : MonoBehaviour
{
    public static bool waterCollide = false;

    public bool IsCollidingWater(Collider2D water, Collider2D station, Vector3 point){
        Vector3 closest = water.ClosestPoint(point);
        Vector3 otherClosest = station.ClosestPoint(closest);
        return closest == otherClosest;
    }
}
