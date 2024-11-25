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

    public void WaterCollide(Collider2D station){

        List<Collider2D> list = new();
        //Collider2D[] array = new Collider2D[1];

        var collider = GetComponent<Collider2D>();
        //Physics2D.OverlapPoint()

        //Physics2D.OverlapCollider(collider, ,collider2Ds);

        if (station.gameObject.CompareTag("Water")){
            waterCollide = true;
        }   
    }
}
