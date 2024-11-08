using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeeklyUpgradeManager : MonoBehaviour
{
    public int weekCount = 0;         // Tracks the number of weeks
    public float weekDuration = 120f; // Duration of a "week" in seconds (2 minutes)
    private float timer;

    public GameObject upgradeScreen;
    public TextMeshProUGUI weekText;
    public Button locomotiveButton;
    public Button upgradeButton1;
    public Button upgradeButton2;
    public Button[] allUpgradeButtons;

    private string[] upgradeOptions = { "Trains", "Carriages", "New Lines", "Tunnels/Bridges", "Interchanges" };

    private void Start()
    {
        timer = weekDuration;
        upgradeScreen.SetActive(false);
        upgradeButton1.gameObject.SetActive(false);
        upgradeButton2.gameObject.SetActive(false);

        locomotiveButton.onClick.AddListener(OnLocomotiveButtonClicked);
        foreach (Button btn in allUpgradeButtons)
        {
            btn.onClick.AddListener(() => OnUpgradeSelected(btn));
        }
        foreach (Button upgradeButton in allUpgradeButtons)
        {
            upgradeButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            timer -= Time.deltaTime;
            Debug.Log("Time until next upgrade: " + Mathf.Ceil(timer) + " seconds");

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
        upgradeScreen.SetActive(true);
        Time.timeScale = 0f;
        weekText.text = "Week " + weekCount;
        upgradeButton1.gameObject.SetActive(false);
        upgradeButton2.gameObject.SetActive(false);
        locomotiveButton.gameObject.SetActive(true);
    }

    public void OnLocomotiveButtonClicked()
    {
        locomotiveButton.gameObject.SetActive(false);
        ShowRandomUpgradeOptions();
    }

    public void ShowRandomUpgradeOptions()
    {
        upgradeButton1.gameObject.SetActive(false);
        upgradeButton2.gameObject.SetActive(false);
        Debug.Log("Showing random upgrade options...");

        if (upgradeButton1 == null)
            Debug.LogError("upgradeButton1 is not assigned!");
        else
            Debug.Log("upgradeButton1 is assigned");

        if (upgradeButton2 == null)
            Debug.LogError("upgradeButton2 is not assigned!");
        else
            Debug.Log("upgradeButton2 is assigned");

        foreach (Button upgradeButton in allUpgradeButtons)
        {
            if (upgradeButton == null)
                Debug.LogError("One of the buttons in allUpgradeButtons is not assigned!");
            else
                Debug.Log("Button in allUpgradeButtons is assigned");

            upgradeButton.gameObject.SetActive(true);
        }
        string[] selectedUpgrades = GetRandomUpgrades();
        upgradeButton1.GetComponentInChildren<TextMeshProUGUI>().text = selectedUpgrades[0];
        upgradeButton2.GetComponentInChildren<TextMeshProUGUI>().text = selectedUpgrades[1];

        upgradeButton1.gameObject.SetActive(true);
        upgradeButton2.gameObject.SetActive(true);
    }

    public string[] GetRandomUpgrades()
    {
        string[] shuffledUpgrades = (string[])upgradeOptions.Clone();
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
        Debug.Log("Player selected: " + selectedButton.GetComponentInChildren<TextMeshProUGUI>().text);
        ApplyUpgrade(selectedButton.GetComponentInChildren<TextMeshProUGUI>().text);
        CloseUpgradeScreen();
    }
    private void ApplyUpgrade(string selectedUpgrade)
    {
        Debug.Log("Applying upgrade: " + selectedUpgrade);
    }

    private void CloseUpgradeScreen()
    {
        upgradeScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    private void UpdateWeekUI()
    {
        Debug.Log("Week: " + weekCount);
    }
}
