using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class DisabledButtonScript : MonoBehaviour
{   
    public Color color;
    public Button button;

    public bool canChangeLine;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

        if (LineList.reference.availableLines == 4 && name.Equals("GreenButton")){

            button.image.color = color;
            button.transform.localScale = new Vector3(0.6f, 0.6f, 0);
            canChangeLine = true;
        }

        else if (LineList.reference.availableLines == 5 && name.Equals("PurpleButton")){

            button.image.color = color;
            button.transform.localScale = new Vector3(0.6f, 0.6f, 0);
            canChangeLine = true;
        }

        else if (LineList.reference.availableLines == 6 && name.Equals("OrangeButton")){

            button.image.color = color;
            button.transform.localScale = new Vector3(0.6f, 0.6f, 0);
            canChangeLine = true;
        }

        else if (LineList.reference.availableLines == 7 && name.Equals("BrownButton")){

            button.image.color = color;
            button.transform.localScale = new Vector3(0.6f, 0.6f, 0);
            canChangeLine = true;
        }
    }
}
