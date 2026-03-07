using UnityEngine;

public class MelodyAudio : MonoBehaviour, IResetable
{
    private AudioSource audioSource;
  
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(MelodyData data)
    {
        if (data != null && data.noteAudio != null)
            audioSource.PlayOneShot(data.noteAudio);
    }

    public void Init()
    {
        if (audioSource != null)
            audioSource.Stop();
        audioSource.clip = null;
    }
}
