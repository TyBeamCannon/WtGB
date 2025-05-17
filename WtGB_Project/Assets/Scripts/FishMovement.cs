using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [Header("Forward Movement")]
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] public float startY = 500f;
    [SerializeField] public float startX = 0f;
    [SerializeField] public float catchZoneY = -300f;

    [Header("Thrash Settings")]
    [SerializeField] public float thrashSpeed = 4f;
    [SerializeField] public float thrashAmplitude = 1f;

    [Header("Scale Settings")]
    [SerializeField] public float minScale = 0.5f;
    [SerializeField] public float maxScale = 1.5f;

    [SerializeField] private RectTransform catchZone;
    private RectTransform rectTrans;
    private Vector2 startPos;

    [Header("Thrash AI Behavior")]
    [SerializeField] private float lerpFrequency = 1.5f;
    [SerializeField] private float maxLerpDistance = 150f;
    [SerializeField] private float lerpSpeed = 5f;

    private float timeSinceLastDecision = 0f;
    private float targetXOffset = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        startPos = rectTrans.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 currentPos = rectTrans.anchoredPosition;

        currentPos.y -= moveSpeed * Time.unscaledDeltaTime;
        rectTrans.anchoredPosition = currentPos;

        float distanceRatio = Mathf.InverseLerp(startY, catchZoneY, currentPos.y);
        float scale = Mathf.Lerp(minScale, maxScale, distanceRatio);
        rectTrans.localScale = Vector3.one * scale;
          
        if(RectOverlaps(rectTrans,catchZone))
        {
            Debug.Log("Fish entered catchzone");
            FishingMinigameManager.instance.EndFishingMinigame(true);
        }

        timeSinceLastDecision += Time.unscaledDeltaTime;

        if(timeSinceLastDecision >= lerpFrequency)
        {
            timeSinceLastDecision = 0f;
            float direction = Random.value > 0.5f ? 1f : -1f;
            targetXOffset = direction * Random.Range(maxLerpDistance * 0.5f, maxLerpDistance);
        }

        float currentX = rectTrans.anchoredPosition.x;
        currentX = Mathf.Lerp(currentX, startX + targetXOffset, Time.unscaledDeltaTime * lerpSpeed);

        Vector2 fishScreenPos = RectTransformUtility.WorldToScreenPoint(null, rectTrans.position);
        float mouseX = Input.mousePosition.x;
        float distanceFromMouse = mouseX - fishScreenPos.x;

        float pullForce = Mathf.Clamp(distanceFromMouse / 300f, -1f, 1f);
        currentX += pullForce * Time.unscaledDeltaTime * 200f;
        currentPos.x = currentX;
        rectTrans.anchoredPosition = currentPos;
    }

    private bool RectOverlaps(RectTransform a, RectTransform b)
    {
        Rect aRect = GetWorldRect(a);
        // ;]
        Rect bRect = GetWorldRect(b);

        return aRect.Overlaps(bRect);
    }

    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return new Rect(corners[0], corners[2] - corners[0]);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit Something??!!" + other.name);

        if (other.CompareTag("FishWall"))
        {
            FishingMinigameManager.instance.EndFishingMinigame(false);
        }

        if (other.CompareTag("CatchZone"))
        {
            Debug.Log("Fish Entered catch Zone");
            FishingMinigameManager.instance.EndFishingMinigame(true);
        }
    }
}
