using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class WeeklyUpgradeManager : MonoBehaviour
{
    public int weekCount = 0;         // Tracks the number of weeks
    public float weekDuration = 120f; // Duration of a "week" in seconds (2 minutes)
    public float timer;

    public GameObject upgradeScreen;
    public TextMeshProUGUI weekText;
    public Button locomotiveButton;
    public Button upgradeButton1;
    public Button upgradeButton2;
    public Button pausebutton;
    public Button resumebutton;
    public Button speedupbutton;
    public Button[] allUpgradeButtons;

    private List<string> upgradeOptions = new List<string>{ "Train", "Carriage", "Line", "Tunnel", "Interchange" };
    public int trainCount = 3;
    public int carriageCount = 0;
    public int tunnelCount = 3;
    public int interchangeCount = 0;
    public bool isGamePaused = false;
    public bool upgardeScreenShown = false;
    public bool speedupActive = false;
    public static WeeklyUpgradeManager reference;

    //icon sprite
    public Sprite trainIcon;
    public Sprite carriageIcon;
    public Sprite tunnelIcon;
    public Sprite interchangeIcon;
    public Sprite lineIcon;

    private void Awake()
    {
        reference = this;
    }


    private void Start()
    {
        speedupbutton.onClick.AddListener(OnSpeedupButtonClicked);
        resumebutton.onClick.AddListener(OnResumeButtonClicked);
        pausebutton.onClick.AddListener(OnPauseButtonClicked);
        timer = weekDuration;
        upgradeScreen.SetActive(false);
        upgradeButton1.gameObject.SetActive(false);
        upgradeButton2.gameObject.SetActive(false);

      //  locomotiveButton.onClick.AddListener(OnLocomotiveButtonClicked);
        foreach (Button btn in allUpgradeButtons)
        {
           // btn.onClick.AddListener(() => OnUpgradeSelected(btn));
        }
        foreach (Button upgradeButton in allUpgradeButtons)
        {
            upgradeButton.gameObject.SetActive(false);
        }
    }
    void OnSpeedupButtonClicked()
    {
        Debug.Log("pressed");
        if (speedupActive == false)
        {
            Time.timeScale = 5.0f;
            speedupActive = true;
        } else
        {
                Time.timeScale = 1.0f;
                speedupActive = false;
        }
    }
    void OnResumeButtonClicked()
    {
        isGamePaused = false;
    }
    private void OnPauseButtonClicked()
    {
        isGamePaused = true;
    }
    private void Update()
    {
        if (Time.timeScale > 0 && isGamePaused == false) //&& isGamePaused == false
        {
            timer -= Time.deltaTime;
          //  Debug.Log("Time until next upgrade: " + Mathf.Ceil(timer) + " seconds");

            if (timer <= 0)
            {
                TriggerWeeklyUpgrade();
                timer = weekDuration;
            }
        }
    }

    private void TriggerWeeklyUpgrade()
    {
        weekCount++;
        UpdateWeekUI();
        ShowUpgradeScreen();
    }

    public void ShowUpgradeScreen()
    {
        upgardeScreenShown = true;
        upgradeScreen.SetActive(true);
        Time.timeScale = 0f;
        weekText.text = "Week " + weekCount;
        upgradeButton1.gameObject.SetActive(false);
        upgradeButton2.gameObject.SetActive(false);

        locomotiveButton.image.sprite = trainIcon;
        locomotiveButton.gameObject.SetActive(true);
    }

    public void OnLocomotiveButtonClicked()
    {
        trainCount++;
        locomotiveButton.gameObject.SetActive(false);
        ShowRandomUpgradeOptions();
    }

    public void ShowRandomUpgradeOptions()
    {
        upgradeButton1.gameObject.SetActive(false);
        upgradeButton2.gameObject.SetActive(false);
        //Debug.Log("Showing random upgrade options...");

        if (upgradeButton1 == null)
            Debug.LogError("upgradeButton1 is not assigned!");
        else
            //Debug.Log("upgradeButton1 is assigned");

        if (upgradeButton2 == null)
            Debug.LogError("upgradeButton2 is not assigned!");
        else
            //Debug.Log("upgradeButton2 is assigned");

        foreach (Button upgradeButton in allUpgradeButtons)
        {
            if (upgradeButton == null)
                Debug.LogError("One of the buttons in allUpgradeButtons is not assigned!");
            else
                //Debug.Log("Button in allUpgradeButtons is assigned");

            upgradeButton.gameObject.SetActive(true);
        }
        string[] selectedUpgrades = GetRandomUpgrades();
        upgradeButton1.GetComponentInChildren<TextMeshProUGUI>().text = selectedUpgrades[0];
        upgradeButton2.GetComponentInChildren<TextMeshProUGUI>().text = selectedUpgrades[1];
        
        Sprite chooseSprite1;
        Sprite chooseSprite2;

        //sprite 1
        if (upgradeButton1.GetComponentInChildren<TextMeshProUGUI>().text.Equals("Train")){
            chooseSprite1 = trainIcon;
        }
        else if (upgradeButton1.GetComponentInChildren<TextMeshProUGUI>().text.Equals("Carriage")){
            chooseSprite1 = carriageIcon;
        }
        else if (upgradeButton1.GetComponentInChildren<TextMeshProUGUI>().text.Equals("Line")){
            chooseSprite1 = lineIcon;
        }
        else if (upgradeButton1.GetComponentInChildren<TextMeshProUGUI>().text.Equals("Tunnel")){
            chooseSprite1 = tunnelIcon;
        }
        else {
            chooseSprite1 = interchangeIcon;
        }

        //sprite 2
        if (upgradeButton2.GetComponentInChildren<TextMeshProUGUI>().text.Equals("Train")){
            chooseSprite2 = trainIcon;
        }
        else if (upgradeButton2.GetComponentInChildren<TextMeshProUGUI>().text.Equals("Carriage")){
            chooseSprite2 = carriageIcon;
        }
        else if (upgradeButton2.GetComponentInChildren<TextMeshProUGUI>().text.Equals("Line")){
            chooseSprite2 = lineIcon;
        }
        else if (upgradeButton2.GetComponentInChildren<TextMeshProUGUI>().text.Equals("Tunnel")){
            chooseSprite2 = tunnelIcon;
        }
        else {
            chooseSprite2 = interchangeIcon;
        }


        upgradeButton1.image.sprite = chooseSprite1;
        upgradeButton2.image.sprite = chooseSprite2;

        upgradeButton1.gameObject.SetActive(true);
        upgradeButton2.gameObject.SetActive(true);
    }

    public string[] GetRandomUpgrades()
    {
        string[] shuffledUpgrades = (string[])upgradeOptions.ToArray();
        for (int i = 0; i < shuffledUpgrades.Length; i++)
        {
            string temp = shuffledUpgrades[i];
            int randomIndex = Random.Range(i, shuffledUpgrades.Length);
            shuffledUpgrades[i] = shuffledUpgrades[randomIndex];
            shuffledUpgrades[randomIndex] = temp;
        }

        return new string[] { shuffledUpgrades[0], shuffledUpgrades[1] };
    }

    public void OnUpgradeSelected(Button selectedButton)
    {
        //Debug.Log("Player selected: " + selectedButton.GetComponentInChildren<TextMeshProUGUI>().text);
        ApplyUpgrade(selectedButton.GetComponentInChildren<TextMeshProUGUI>().text);
        CloseUpgradeScreen();
        if (speedupActive == true)
        {
            Time.timeScale = 5.0f;
        } //else 
      //  {
         //   Time.timeScale = 1.0f;
       // }
    }
    private void ApplyUpgrade(string selectedUpgrade)
    {
        //Debug.Log(selectedUpgrade);
        if (selectedUpgrade.Equals("Train"))
        {
            trainCount++;
            //Debug.Log("train " + trainCount);
        }
        if (selectedUpgrade.Equals("Carriage"))
        {
            carriageCount++;
            TrainCircleManager.valueCalculated = true;
            //Debug.Log("carriage " + carriageCount);
        }
        if (selectedUpgrade.Equals("Line"))
        {
            LineList.reference.availableLines++;

            if (LineList.reference.availableLines >= 7) {
                upgradeOptions.Remove("Line");
            }

            //Debug.Log("new line " + newlineCount);
        }
        if (selectedUpgrade.Equals("Interchange"))
        {
            interchangeCount++;
            TrainCircleManager.valueCalculated = true;
            //Debug.Log("inter " + interchangeCount);
        }
        if (selectedUpgrade.Equals("Tunnel"))
        {
            tunnelCount++;
            //Debug.Log("tunnel " + tunnelCount);
        }
       // Debug.Log("Applying upgrade: " + selectedUpgrade);
    }

    private void CloseUpgradeScreen()
    {
        upgardeScreenShown = false;
        upgradeScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    private void UpdateWeekUI()
    {
        //Debug.Log("Week: " + weekCount);
    }
}
