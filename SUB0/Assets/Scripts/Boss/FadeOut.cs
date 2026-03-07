using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    SpriteRenderer sr;
    public float duration = 0.3f;
    float timer;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void StartFade(float fadeDuration)
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadeCorutine(fadeDuration));
    }

    IEnumerator FadeCorutine(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / duration);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
