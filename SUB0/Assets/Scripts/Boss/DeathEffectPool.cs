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
        for (int i = 0; i < poolSize; i++) // 사망시 나오는 이펙트 풀 생성
        {
            effectPool.Add(Instantiate(deathEffect, transform));
            effectPool[i].SetActive(false);
        }
    }

    public IEnumerator Death() // 비동기 함수 --> 보스의 사망 판정?
    {
        for (int i = 0; i < 30; i++)
        {
            float posX = Random.Range(-7.42f, 7.42f); // 랜덤한 위치에서 사망 이펙트 생성
            float posY = Random.Range(-2.73f, 4.94f);
            Vector2 pos = new Vector2(posX, posY);

            effectPool[poolIndex].transform.position = pos; // 랜덤으로 정해진 위치로 이펙트 위치 초기화
            effectPool[poolIndex].SetActive(true); // 사망시 나오는 이펙트 활성화

            yield return new WaitForSeconds(0.1f); // 0.1초의 간격으로 30번 반복
        }

        yield return new WaitForSeconds(1f); // 30번의 사망 이펙트 후 1초 기다리기

        GameManager.instance.audioSource.PlayOneShot(death); // 사망 효과음
        bojo.gameObject.SetActive(false); // 보스 비활성화

        Debug.Log("씬 전환 준비");
        yield return new WaitForSeconds(2f);
    }
}
