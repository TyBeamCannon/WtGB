using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public void OnHover()
    {
        GetComponent<RectTransform>().localEulerAngles += new Vector3(0, 0, 11);
    }

    public void OffHover()
    {
        GetComponent<RectTransform>().localEulerAngles += new Vector3(0, 0, -11);
    }

    public void OnClick()
    {
        OffHover();
    }
}
