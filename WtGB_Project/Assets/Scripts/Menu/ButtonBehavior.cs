using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] Text buttonText;
    public void OnHover()
    {
        buttonText.transform.localEulerAngles = new Vector3(0, 0, 9);
    }

    public void OffHover()
    {
        buttonText.transform.localEulerAngles = new Vector3(0, 0, -3.2f);
    }

    public void OnClick()
    {
        buttonText.transform.localEulerAngles = new Vector3(0, 0, 9);
    }
}
