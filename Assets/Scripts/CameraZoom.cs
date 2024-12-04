using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    
    public static float zoom;
    public float baseZoom;
    public float maxZoom;
    public float zoomSpeed;


    public static bool haveToZoom;
    public static bool stopZoom;
    public float haveToZoomDur;
    public float haveToZoomDurBase;

    // Start is called before the first frame update
    void Start()
    {
        zoom = baseZoom;
        haveToZoomDur = haveToZoomDurBase;
        gameObject.GetComponent<Camera>().orthographicSize = zoom;
    }

    // Update is called once per frame
    void Update()
    {   
        if (haveToZoom){
            zoom += Time.deltaTime * zoomSpeed * 0.01f;
            float newY = gameObject.transform.position.y;
            
            if (zoom < maxZoom){
                newY -= Time.deltaTime * zoomSpeed * 0.01f;
            }
            
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, newY, -10);
            if (zoom >= maxZoom){
                zoom = maxZoom;
                stopZoom = true;
            }
        }
        
        if (haveToZoom){
            haveToZoomDur -= Time.deltaTime;
            if (haveToZoomDur <= 0){
                haveToZoomDur = haveToZoomDurBase;
                haveToZoom = false;
            }
        }

        gameObject.GetComponent<Camera>().orthographicSize = zoom;
    }
}
