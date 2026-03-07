using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public AudioClip boom;

    public void OnAnimationStart()
    {
        GameManager.instance.audioSource.PlayOneShot(boom);
    }

    public void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
