using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public AudioClip boom;

    public void OnAnimationStart()
    {
        GameManager.instance.audioSource.PlayOneShot(boom); // Boom.cs와의 차이가 무엇인가?
    }

    public void OnAnimationEnd()
    {
        gameObject.SetActive(false); // 보스의 죽는 애니메이션이 끝나면 보스 객체 비활성화
    }
}
