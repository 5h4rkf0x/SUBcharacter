using UnityEngine;

public class Lazer : MonoBehaviour
{
    public AudioClip impact;

    public void OnAnimationStart()
    {
        GameManager.instance.audioSource.PlayOneShot(impact);
    }

    public void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
