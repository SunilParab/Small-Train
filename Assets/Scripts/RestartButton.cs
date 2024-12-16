using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{

    public string goTo;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        // Attach the Button's onClick listener
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(CloseGame);
        }
    }


    void CloseGame() {
        Application.Quit();
    }

}
