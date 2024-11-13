using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationSpawnManager : MonoBehaviour
{

    public GameObject square;
    public GameObject triangle;
    public GameObject circle;
    public GameObject diamond;
    public GameObject pie;
    public GameObject star;
    public GameObject rhombus;
    public GameObject plus;
    public GameObject eye;
    public GameObject pentagon;

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
            int stationType = Random.Range(0, 10);
            int xPos = Random.Range(-9, 9);
            int yPos = Random.Range(-4, 4);
            if (stationType == 0) GameObject.Instantiate(square, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 1) GameObject.Instantiate(triangle, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 2) GameObject.Instantiate(circle, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 3) GameObject.Instantiate(diamond, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 4) GameObject.Instantiate(pie, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 5) GameObject.Instantiate(star, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 6) GameObject.Instantiate(rhombus, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 7) GameObject.Instantiate(plus, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 8) GameObject.Instantiate(eye, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 9) GameObject.Instantiate(pentagon, new Vector3(xPos, yPos, -1), Quaternion.identity);

        }
    }
}
