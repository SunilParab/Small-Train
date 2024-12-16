using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarriagePlacer : MonoBehaviour
{
    public GameObject objectPrefab; // Assign your prefab in the Inspector
    private GameObject spawnedObject; // Holds the reference to the spawned object
    public bool isMaking = false;
    public static CarriagePlacer reference;

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
    }

    void SpawnObject()
    {
        if (WeeklyUpgradeManager.reference.carriageCount > 0 && spawnedObject == null)
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
