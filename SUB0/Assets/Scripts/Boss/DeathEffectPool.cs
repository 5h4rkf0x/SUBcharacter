using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathEffectPool : MonoBehaviour
{
    public AudioClip death;
    public Bojo bojo;
    public GameObject deathEffect;
    List<GameObject> effectPool = new List<GameObject>();

    int poolSize = 20;
    int poolIndex = 0;
    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            effectPool.Add(Instantiate(deathEffect, transform));
            effectPool[i].SetActive(false);
        }
    }

    public IEnumerator Death()
    {
        for (int i = 0; i < 30; i++)
        {
            float posX = Random.Range(-7.42f, 7.42f);
            float posY = Random.Range(-2.73f, 4.94f);
            Vector2 pos = new Vector2(posX, posY);

            effectPool[poolIndex].transform.position = pos;
            effectPool[poolIndex].SetActive(true);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);

        GameManager.instance.audioSource.PlayOneShot(death);
        bojo.gameObject.SetActive(false);

        Debug.Log("¾À ÀüÈ¯ ÁØºñ");
        yield return new WaitForSeconds(2f);
    }
}
