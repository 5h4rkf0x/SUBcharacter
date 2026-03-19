using UnityEngine;

public class Lazer : MonoBehaviour
{
    public AudioClip impact;

    public void OnAnimationStart()
    {
        GameManager.instance.audioSource.PlayOneShot(impact); // 레이저 공격 소리 출력
    }

    public void OnAnimationEnd()
    {
        gameObject.SetActive(false); // 공격에니메이션 끝나고 객체 비활성화
    }
}
