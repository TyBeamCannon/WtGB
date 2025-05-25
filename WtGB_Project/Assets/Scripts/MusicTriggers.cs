using UnityEngine;

public class MusicTriggers : MonoBehaviour
{
    [SerializeField] AudioSource musicBox;

    [SerializeField] AudioClip theme;

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
        musicBox.PlayOneShot(theme, GameManager.instance.MusicValue * GameManager.instance.MasterValue);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
            return;
        musicBox.Stop();
    }
}
