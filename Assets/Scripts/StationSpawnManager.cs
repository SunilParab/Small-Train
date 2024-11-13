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

    public int xRange;
    public int yRange;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;

        //change this based on zoom amount
        xRange = 3;
        yRange = 1;

        //resize prefabs
        float scale = 0.025f;
        square.transform.localScale = new Vector3(scale, scale, scale);
        triangle.transform.localScale = new Vector3(scale, scale, scale);
        circle.transform.localScale = new Vector3(scale, scale, scale);
        diamond.transform.localScale = new Vector3(scale, scale, scale);
        pie.transform.localScale = new Vector3(scale, scale, scale);
        star.transform.localScale = new Vector3(scale, scale, scale);
        rhombus.transform.localScale = new Vector3(scale, scale, scale);
        plus.transform.localScale = new Vector3(scale, scale, scale);
        eye.transform.localScale = new Vector3(scale, scale, scale);
        pentagon.transform.localScale = new Vector3(scale, scale, scale);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTime++;

        if (spawnTime % 240 == 0)
        {
            int stationType = Random.Range(0, 10);
            int xPos = Random.Range(-xRange, xRange);
            int yPos = Random.Range(-yRange, yRange);
            if (stationType == 0) Instantiate(square, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 1) Instantiate(triangle, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 2) Instantiate(circle, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 3) Instantiate(diamond, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 4) Instantiate(pie, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 5) Instantiate(star, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 6) Instantiate(rhombus, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 7) Instantiate(plus, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 8) Instantiate(eye, new Vector3(xPos, yPos, -1), Quaternion.identity);
            else if (stationType == 9) Instantiate(pentagon, new Vector3(xPos, yPos, -1), Quaternion.identity);

        }
    }
}
