using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    SpriteRenderer sr;
    public float duration = 0.3f;
    float timer;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>(); // 스프라이트 준비
    }

    public void StartFade(float fadeDuration)
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>(); // 만약 스프라이트가 없다면 초기화
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f); // 스프라이트 컬러 조정
        gameObject.SetActive(true); // 페이드 시작
        StopAllCoroutines(); // 기존의 모든 비동기함수 비활성화
        StartCoroutine(FadeCorutine(fadeDuration)); // 아래의 페이드아웃 관련 비동기함수 실행
    }

    IEnumerator FadeCorutine(float duration) // 페이드아웃 비동기함수
    {
        float timer = 0f;
        while (timer < duration) // 0.3초동안 실행
        {
            timer += Time.deltaTime; 
            float alpha = Mathf.Lerp(1f, 0f, timer / duration); // 부드럽게 투명도? 변경
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha); // 스프라이트 컬러 조정
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
