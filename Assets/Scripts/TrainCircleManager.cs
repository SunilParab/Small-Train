using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainCircleManager : MonoBehaviour
{
    public Button bottomCircleButton;
    public RectTransform trainCircle;
    public TextMeshProUGUI trainCountText;
    public GameObject bottomCircle;
    public WeeklyUpgradeManager weeklyUpgradeManager;
    private Vector3 bottomCircleTargetPosition = new Vector3(0, -175, 0);
    private Vector3 targetPosition;
    private bool InventoryOpen = false;
    void Start()
    {
        trainCircle.anchoredPosition = new Vector2(0, -Screen.height * 0.75f);
        trainCircle.gameObject.SetActive(false);
        bottomCircleButton.onClick.AddListener(OnBottomCircleClicked);
    }

    void Update()
    {
        if (weeklyUpgradeManager != null)
        {
            UpdateTrainCount(weeklyUpgradeManager.trainCount);
        }
    }

    void OnBottomCircleClicked()
    {

    if (InventoryOpen == false)
    {
            trainCircle.gameObject.SetActive(true);
            targetPosition = new Vector3(0, -240, 0);
            bottomCircleTargetPosition = new Vector3(0, -175, 0);
            StartCoroutine(MoveTrainCircle());
            InventoryOpen = true;
        } else
        {
            targetPosition = new Vector3(0, -323, 0);
            bottomCircleTargetPosition = new Vector3(0, -250, 0);
            StartCoroutine(MoveTrainCircleDown());
            InventoryOpen = false;
        }
    }

    void UpdateTrainCount(int count)
    {
        trainCountText.text = count.ToString();
    }

    public IEnumerator MoveTrainCircle()
    {
        float duration = 0.5f;
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

    public IEnumerator MoveTrainCircleDown()
    {
        float duration = 0.5f;
        Vector3 startTrainPosition = trainCircle.transform.localPosition;
        Vector3 targetTrainPosition = new Vector3(0, -323, 0);
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
        trainCircle.gameObject.SetActive(false);
    }
}
