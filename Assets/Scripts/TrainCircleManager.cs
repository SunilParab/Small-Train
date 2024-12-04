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
    public GameObject RedLineCircle;
    public GameObject YellowLineCircle;
    public GameObject BlueLineCircle;
    public WeeklyUpgradeManager weeklyUpgradeManager;
  //  public LineList lineList;
    private Vector3 bottomCircleTargetPosition = new Vector3(0, -175, 0);
    private Vector3 targetPosition;
    private Vector3 targetTunnelPosition;
    private bool InventoryOpen = false;

    public RectTransform tunnelCircle;
    public TextMeshProUGUI tunnelCountText;
    void Start()
    {
       // Transform scaleredcircle = RedLineCircle.GetComponent<Transform>();
        trainCircle.anchoredPosition = new Vector2(0, -Screen.height * 0.75f);
        trainCircle.gameObject.SetActive(false);
        tunnelCircle.anchoredPosition = new Vector2(200, -Screen.height * 0.75f);
        tunnelCircle.gameObject.SetActive(false);
        bottomCircleButton.onClick.AddListener(OnBottomCircleClicked);
    }

    void Update()
    {
       // if (LineList.lineList[0].LineSegments.Count != 0)
       // {
      //      RedLineCircle.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
      //  }
    //    RedLineCircle.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        if (weeklyUpgradeManager != null)
        {
            UpdateTrainCount(weeklyUpgradeManager.trainCount);
            UpdateTunnelCount(weeklyUpgradeManager.tunnelCount);
        }
    }

    void OnBottomCircleClicked()
    {

    if (InventoryOpen == false)
    {
            tunnelCircle.gameObject.SetActive(true);
            trainCircle.gameObject.SetActive(true);
            targetPosition = new Vector3(0, -240, 0);
            targetTunnelPosition = new Vector3(200, -240, 0);
            bottomCircleTargetPosition = new Vector3(0, -175, 0);
            StartCoroutine(MoveTrainCircle());
            InventoryOpen = true;
        } else
        {
            targetTunnelPosition = new Vector3(200, -323, 0);
            targetPosition = new Vector3(0, -323, 0);
            bottomCircleTargetPosition = new Vector3(0, -250, 0);
            StartCoroutine(MoveTrainCircleDown());
            InventoryOpen = false;
        }
    }
    void UpdateTunnelCount(int count)
    {
       tunnelCountText.text = count.ToString();
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
        Vector3 startTunnelPosition = tunnelCircle.transform.localPosition;
        Vector3 targetTunnelPosition = new Vector3(200, -240, 0);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            trainCircle.transform.localPosition = Vector3.Lerp(startTrainPosition, targetTrainPosition, t);
            tunnelCircle.transform.localPosition = Vector3.Lerp(startTunnelPosition, targetTunnelPosition, t);
            bottomCircle.transform.localPosition = Vector3.Lerp(startBottomPosition, targetBottomPosition, t);
            yield return null;
        }
        trainCircle.transform.localPosition = targetTrainPosition;
        tunnelCircle.transform.localPosition = targetTunnelPosition;
        bottomCircle.transform.localPosition = targetBottomPosition;
    }

    public IEnumerator MoveTrainCircleDown()
    {
        float duration = 0.5f;
        Vector3 startTrainPosition = trainCircle.transform.localPosition;
        Vector3 targetTrainPosition = new Vector3(0, -323, 0);
        Vector3 startTunnelPosition = tunnelCircle.transform.localPosition;
        Vector3 targetTunnelPosition = new Vector3(200, -323, 0);
        Vector3 startBottomPosition = bottomCircle.transform.localPosition;
        Vector3 targetBottomPosition = bottomCircleTargetPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            trainCircle.transform.localPosition = Vector3.Lerp(startTrainPosition, targetTrainPosition, t);
            tunnelCircle.transform.localPosition = Vector3.Lerp(startTunnelPosition, targetTunnelPosition, t);
            bottomCircle.transform.localPosition = Vector3.Lerp(startBottomPosition, targetBottomPosition, t);
            yield return null;
        }
        trainCircle.transform.localPosition = targetTrainPosition;
        tunnelCircle.transform.localPosition = targetTunnelPosition;
        bottomCircle.transform.localPosition = targetBottomPosition;
        trainCircle.gameObject.SetActive(false);
        tunnelCircle.gameObject.SetActive(false);
    }
}
