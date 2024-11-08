using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeeklyUpgradeManager : MonoBehaviour
{
    public int weekCount = 0;         // Tracks the number of weeks
    public float weekDuration = 120f; // Duration of a "week" in seconds (2 minutes)
    private float timer;

    public GameObject upgradeScreen;  // Reference to the upgrade screen UI
    public TextMeshProUGUI weekText;            // Reference to the Week Text UI element
    public Button locomotiveButton;   // Reference to the "Locomotive" button
    public Button upgradeButton1;     // Reference to the first upgrade button
    public Button upgradeButton2;     // Reference to the second upgrade button
    public Button[] allUpgradeButtons; // All upgrade buttons for easy management

    private string[] upgradeOptions = { "Trains", "Carriages", "New Lines", "Tunnels/Bridges", "Interchanges" };

    private void Start()
    {
        timer = weekDuration;
        upgradeScreen.SetActive(false); // Hide the upgrade screen initially
        upgradeButton1.gameObject.SetActive(false); // Hide upgrade buttons
        upgradeButton2.gameObject.SetActive(false); // Hide upgrade buttons

        locomotiveButton.onClick.AddListener(OnLocomotiveButtonClicked); // Set up listener for Locomotive button
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
            timer -= Time.deltaTime; // Countdown timer for the week

            // Log remaining time in seconds
            Debug.Log("Time until next upgrade: " + Mathf.Ceil(timer) + " seconds");

            if (timer <= 0)
            {
                TriggerWeeklyUpgrade(); // Show upgrade screen when time is up
                timer = weekDuration;   // Reset timer for the next week
            }
        }
    }

    private void TriggerWeeklyUpgrade()
    {
        weekCount++;                          // Increment week count
        UpdateWeekUI();                       // Update week UI display if any
        ShowUpgradeScreen();                  // Show the upgrade screen
    }

    public void ShowUpgradeScreen()
    {
        upgradeScreen.SetActive(true);
        Time.timeScale = 0f; // Activate the upgrade screen
        weekText.text = "Week " + weekCount; // Update the week number
        upgradeButton1.gameObject.SetActive(false);
        upgradeButton2.gameObject.SetActive(false);

        // Show the locomotive button to start the upgrade
        locomotiveButton.gameObject.SetActive(true);
    }

    public void OnLocomotiveButtonClicked()
    {
        locomotiveButton.gameObject.SetActive(false); // Hide locomotive button
        ShowRandomUpgradeOptions(); // Show random upgrades
    }

    public void ShowRandomUpgradeOptions()
    {
        upgradeButton1.gameObject.SetActive(false);
        upgradeButton2.gameObject.SetActive(false);
        Debug.Log("Showing random upgrade options...");

        // Check if the buttons are assigned
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
        // Select two random upgrades from the options
        string[] selectedUpgrades = GetRandomUpgrades();

        // Assign the upgrade names to the buttons
        upgradeButton1.GetComponentInChildren<TextMeshProUGUI>().text = selectedUpgrades[0];
        upgradeButton2.GetComponentInChildren<TextMeshProUGUI>().text = selectedUpgrades[1];


        // Show the upgrade buttons
        upgradeButton1.gameObject.SetActive(true);
        upgradeButton2.gameObject.SetActive(true);
    }

    public string[] GetRandomUpgrades()
    {
        // Create a shuffled array of upgrades
        string[] shuffledUpgrades = (string[])upgradeOptions.Clone();
        for (int i = 0; i < shuffledUpgrades.Length; i++)
        {
            string temp = shuffledUpgrades[i];
            int randomIndex = Random.Range(i, shuffledUpgrades.Length);
            shuffledUpgrades[i] = shuffledUpgrades[randomIndex];
            shuffledUpgrades[randomIndex] = temp;
        }

        // Return the first two upgrades
        return new string[] { shuffledUpgrades[0], shuffledUpgrades[1] };
    }

    public void OnUpgradeSelected(Button selectedButton)
    {
        // Log the selected upgrade
        Debug.Log("Player selected: " + selectedButton.GetComponentInChildren<TextMeshProUGUI>().text);
        ApplyUpgrade(selectedButton.GetComponentInChildren<TextMeshProUGUI>().text);
        // Close the upgrade screen and resume the game
        CloseUpgradeScreen();
    }
    private void ApplyUpgrade(string selectedUpgrade)
    {
        // Apply logic based on selected upgrade (e.g., modify train speeds, add lines, etc.)
        Debug.Log("Applying upgrade: " + selectedUpgrade);
    }

    private void CloseUpgradeScreen()
    {
        upgradeScreen.SetActive(false);   // Hide the upgrade screen
        Time.timeScale = 1f;              // Resume the game
    }

    private void UpdateWeekUI()
    {
        Debug.Log("Week: " + weekCount);  // Log current week (you can replace this with actual UI code)
    }
}
