using UnityEngine;

public class MusicTriggers : MonoBehaviour
{
    [SerializeField] AudioSource homeTheme;
    [SerializeField] AudioSource townTheme;

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
        if (homeTheme.isPlaying)
        {
            homeTheme.Stop();
            townTheme.Play();
        }
        else if (townTheme.isPlaying)
        {
            townTheme.Stop();
            homeTheme.Play();
        }
    }
}
