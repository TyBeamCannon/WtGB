using DPUtils.Systems.DateTime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    [SerializeField] RectTransform clockFace;
    [SerializeField] Text date, time, week;

    [SerializeField] float startingRotation;

    [SerializeField] Light sunlight;
    [SerializeField] float nightIntensity;
    [SerializeField] float dayIntensity;
    [SerializeField] AnimationCurve dayNightCurve;

    private void Awake()
    {
        sunlight = GameObject.FindWithTag("Light").GetComponent<Light>();
        startingRotation = clockFace.localEulerAngles.z;
    }

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
    }

    private void UpdateDateTime(DateTime dateTime)
    {
        date.text = dateTime.DateToString();
        time.text = dateTime.TimeToString();
        week.text = $"WK: {dateTime.CurrentWeek.ToString()}";

        float t = (float)dateTime.Hour / 24f;

        float newRotation = Mathf.Lerp(0, 360, t);
        clockFace.localEulerAngles = new Vector3(0, 0, newRotation + startingRotation);

        float dayNightT = dayNightCurve.Evaluate(t);

        sunlight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, dayNightT);
    }
}
