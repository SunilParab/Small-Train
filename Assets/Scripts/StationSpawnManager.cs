using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationSpawnManager : MonoBehaviour
{

    public GameObject square;
    public GameObject triangle;
    public GameObject circle;

    private int spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTime++;

        if (spawnTime % 240 == 0)
        {
            int stationType = Random.Range(0, 3);
            int xPos = Random.Range(-9, 9);
            int yPos = Random.Range(-4, 4);
            if (stationType == 0) GameObject.Instantiate(square, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 1) GameObject.Instantiate(triangle, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 2) GameObject.Instantiate(circle, new Vector3(xPos, yPos, -1), Quaternion.identity);
        }
    }
}
