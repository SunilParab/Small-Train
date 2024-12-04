using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationCollideWater : MonoBehaviour
{
    public static bool waterCollide = false;

    /*
    void OnTriggerStay2D(Collider2D water){
        print("water collide!!!!!!");
        if (water.gameObject.CompareTag("Water")){
            waterCollide = true;
        }
    }

    /*
    public void WaterCollideOld(Collider2D station){

        List<Collider2D> list = new();
        //Collider2D[] array = new Collider2D[1];

        var collider = GetComponent<Collider2D>();
        //Physics2D.OverlapPoint()

    
        //Physics2D.OverlapCollider(collider, list);

        if (station.gameObject.CompareTag("Water")){
            waterCollide = true;
        }   
    }
    */

    public bool IsCollidingWater(Collider2D water, Collider2D station, Vector3 point){
        Vector3 closest = water.ClosestPoint(point);
        Vector3 otherClosest = station.ClosestPoint(closest);
        return closest == otherClosest;
    }
}
