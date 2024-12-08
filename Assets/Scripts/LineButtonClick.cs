using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineButtonClick : MonoBehaviour
{
    private int lineNum = 0;

    public void ChangeLine(){
        if (EventSystem.current.currentSelectedGameObject.name.Equals("YellowButton")){
            lineNum = 0;
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("RedButton")){
            lineNum = 1;
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("BlueButton")){
            lineNum = 2;
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("GreenButton")
        && EventSystem.current.currentSelectedGameObject.GetComponent<DisabledButtonScript>().canChangeLine){
            lineNum = 3;
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("PurpleButton")
        && EventSystem.current.currentSelectedGameObject.GetComponent<DisabledButtonScript>().canChangeLine){
            lineNum = 4;
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("OrangeButton")
        && EventSystem.current.currentSelectedGameObject.GetComponent<DisabledButtonScript>().canChangeLine){
            lineNum = 5;
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("BrownButton")
        && EventSystem.current.currentSelectedGameObject.GetComponent<DisabledButtonScript>().canChangeLine){
            lineNum = 6;
        }


        print("changed line to: " + lineNum);
        LineDrawer.reference.targetLine = lineNum;
    }
}
