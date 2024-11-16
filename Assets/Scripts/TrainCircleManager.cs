using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainCircleManager : MonoBehaviour
{
    public Button bottomCircleButton; // Button at the bottom of the screen
    public RectTransform trainCircle; // UI element for the Train Circle
    public TextMeshProUGUI trainCountText; // Text for the train count
    public GameObject bottomCircle;
    public WeeklyUpgradeManager weeklyUpgradeManager; // Reference to WeeklyUpgradeManager
    private Vector3 bottomCircleTargetPosition = new Vector3(0, -175, 0);
    private Vector3 targetPosition;

    void Start()
    {
        // Set starting position below the screen
        trainCircle.anchoredPosition = new Vector2(0, -Screen.height * 0.75f);
        trainCircle.gameObject.SetActive(false); // Hide the Train Circle at the start

        bottomCircleButton.onClick.AddListener(OnBottomCircleClicked);
    }

    void Update()
    {
        if (weeklyUpgradeManager != null)
        {
            UpdateTrainCount(weeklyUpgradeManager.trainCount); // Pass train count dynamically
        }
    }

    void OnBottomCircleClicked()
    {
        trainCircle.gameObject.SetActive(true); // Show the Train Circle

        // Move the Train Circle up
        targetPosition = new Vector3(0, -240, 0); // Moves up to -240 Y
        bottomCircleTargetPosition = new Vector3(0, -175, 0);

        StartCoroutine(MoveTrainCircle());

    }

    void UpdateTrainCount(int count)
    {
        trainCountText.text = count.ToString(); // Update train count text
    }

    public IEnumerator MoveTrainCircle()
    {
        float duration = 0.5f; // Animation duration
        Vector3 startTrainPosition = trainCircle.transform.localPosition;
        Vector3 targetTrainPosition = new Vector3(0, -240, 0);

        Vector3 startBottomPosition = bottomCircle.transform.localPosition;
        Vector3 targetBottomPosition = bottomCircleTargetPosition;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            trainCircle.transform.localPosition = Vector3.Lerp(startTrainPosition, targetTrainPosition, t);
            bottomCircle.transform.localPosition = Vector3.Lerp(startBottomPosition, targetBottomPosition, t);

            yield return null;
        }

        trainCircle.transform.localPosition = targetTrainPosition;
        bottomCircle.transform.localPosition = targetBottomPosition;
    }
}
