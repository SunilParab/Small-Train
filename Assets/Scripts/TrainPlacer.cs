using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainPlacer : MonoBehaviour
{
    public GameObject objectPrefab; // Assign your prefab in the Inspector
    private GameObject spawnedObject; // Holds the reference to the spawned object
    public bool isMaking = false;
    public static TrainPlacer reference;

    void Awake() {
        reference = this;
    }

    void Start()
    {
        // Attach the Button's onClick listener
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(SpawnObject);
        }
    }

    void Update()
    {
        if (isMaking)
        {
            // Make the object follow the cursor
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPosition.z = 0;
            spawnedObject.transform.position = cursorPosition;

        }
        

        //raycast shenanigans
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.name == "Cube A")
            {
                //do something here
                break;
            }
        }
    }

    void SpawnObject()
    {
        if (WeeklyUpgradeManager.reference.trainCount > 0 && spawnedObject == null)
        {
            // Instantiate the object at the cursor's position
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPosition.z = 0;
            spawnedObject = Instantiate(objectPrefab, cursorPosition, Quaternion.identity);
            isMaking = true;
        }
    }

    public void stopMaking() {
        isMaking = false;
        Destroy(spawnedObject);
    }

}