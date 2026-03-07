using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAttackPlatformController : MonoBehaviour, IResetable
{
    // platforms ¸®½ºÆ®¿¡ ÇÃ·§Æû ³ÖÀ¸¸é ÇÃ·§ÆûÀÌ ¼ø¼­´ë·Î º¸¿´´Ù »ç¶óÁü

    [SerializeField] List<GameObject> platforms;
    [SerializeField] float timer = 1f;
    private bool isStarted = false;

    [SerializeField] GameObject nonPushSwitch;

    private void Awake()
    {
        if (platforms == null || platforms.Count == 0)
        {
            platforms = new List<GameObject>();

            foreach (Transform child in transform)
            {
                platforms.Add(child.gameObject);
            }
        }

        foreach (GameObject gameObject in platforms)
        {
            gameObject.SetActive(false);
        }

        if (nonPushSwitch != null)
        {
            nonPushSwitch.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isStarted)
        {
            isStarted = true;
            StartCoroutine("StartTimeAttack");

            if (nonPushSwitch != null)
            {
                nonPushSwitch.SetActive(false);
            }
        }
    }

    IEnumerator StartTimeAttack()
    {
        foreach (GameObject gameObject in platforms)
        {
            gameObject.SetActive(true);
            yield return new WaitForSeconds(timer);
            gameObject.SetActive(false);
        }

        isStarted = false;

        if (nonPushSwitch != null)
        {
            nonPushSwitch.SetActive(true);
        }
    }

    public void Init()
    {
        StopCoroutine("StartTimeAttack");
        isStarted = false;

        foreach (GameObject gameObject in platforms)
        {
            gameObject.SetActive(false);
        }

        if (nonPushSwitch != null)
        {
            nonPushSwitch.SetActive(true);
        }
    }
}
