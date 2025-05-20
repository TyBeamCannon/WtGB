using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [Header("Forward Movement")]
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] public float startY = 500f;
    [SerializeField] public float startX = 0f;
    [SerializeField] public float catchZoneY = -300f;

    [Header("Scale Settings")]
    [SerializeField] public float minScale = 0.5f;
    [SerializeField] public float maxScale = 1.5f;
    [SerializeField] private float mousePull = 2f;
    [SerializeField] private RectTransform catchZone;
    private RectTransform rectTrans;

    [Header("Thrash AI Behavior")]
    [SerializeField] private float maxLerpDistance = 150f;
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private float wallPullDuration = 1.5f;
    [SerializeField] private float idleDuration = 1f;

    private float stateTimer = 0f;
    private enum FishState { Idle, Pulling }
    private FishState currentState = FishState.Idle;

    private float currentTargetX = 0f;  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        rectTrans.anchoredPosition = new Vector2 (startX, startY);
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 currentPos = rectTrans.anchoredPosition;

        currentPos.y -= moveSpeed * Time.unscaledDeltaTime;


        float distanceRatio = Mathf.InverseLerp(startY, catchZoneY, currentPos.y);
        float scale = Mathf.Lerp(minScale, maxScale, distanceRatio);
        rectTrans.localScale = Vector3.one * scale;

        stateTimer += Time.unscaledDeltaTime;

        if (currentState == FishState.Idle)
        {
            if (stateTimer >= idleDuration)
            {
                stateTimer = 0f;
                currentState = FishState.Pulling;
                float direction = Random.value > 0.5f ? 1f : -1f;
                currentTargetX = direction * Random.Range(maxLerpDistance * 0.5f, maxLerpDistance);
            }

        }
        else if (currentState == FishState.Pulling)
        {
            if (stateTimer >= wallPullDuration)
            {
                stateTimer = 0f;
                currentState = FishState.Idle;
                currentTargetX = 0f;
            }
        }


        float baseX = Mathf.Lerp(currentPos.x, currentTargetX, Time.unscaledDeltaTime * lerpSpeed);

        Vector2 fishScreenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, rectTrans.localPosition);



        float mouseX = Input.mousePosition.x;

        Debug.DrawLine(new Vector3(mouseX, 0, 0), new Vector3(fishScreenPos.x, 0, 0), Color.red);

        float distanceFromMouse = mouseX - fishScreenPos.x;
        float pullForce = Mathf.Clamp(distanceFromMouse / 300f, -1f, 1f);

        float depthFactor = Mathf.InverseLerp(startY, catchZoneY, currentPos.y);
        float pullStrength = Mathf.Lerp(100f, 300f, depthFactor);

        float finalX = baseX + (pullForce * Time.unscaledDeltaTime * pullStrength * mousePull);

        currentPos.x = finalX;
        rectTrans.anchoredPosition = currentPos;

        if (RectOverlaps(rectTrans, catchZone))
        {
            FishingMinigameManager.instance.EndFishingMinigame(true);
        }

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
