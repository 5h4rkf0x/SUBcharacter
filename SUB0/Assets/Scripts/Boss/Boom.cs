using UnityEngine;

public class Boom : MonoBehaviour
{
    public AudioClip boom;
    public void OnAnimationEnd()
    {
        
    }

    public void OnAnimationStart()
    {
        GameManager.instance.audioSource.PlayOneShot(boom);
    }
}
