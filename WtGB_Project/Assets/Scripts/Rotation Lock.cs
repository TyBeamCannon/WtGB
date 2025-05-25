using Unity.VisualScripting;
using UnityEngine;

public class RotationLock : MonoBehaviour
{
    private void Update()
    {
        this.GetComponent<RectTransform>().eulerAngles = Vector3.zero;
    }
}
