using System.Collections.Generic;
using UnityEngine;

public class GhostPool : MonoBehaviour,IResetable
{
    public GameObject ghostPrefab;
    public int size = 10;
    public List<GameObject> pools;
    int index = 0;

    private void Awake() // 잔상 준비
    {
        pools = new List<GameObject>();
        for (int i = 0; i < size; i++)
        {
            pools.Add(Instantiate(ghostPrefab,transform));
            pools[i].SetActive(false);
        }
    }

    public void Init() // IResetable 필수 함수
    {
        index = 0;
        for(int i = 0; i< size; i++)
        {
            pools[i].SetActive(false); // 전체 비활성화
        }
    }

    public void SpawnGhost(Sprite sprite, Vector3 position, Vector3 scale, float lifetime) // GhostTrail.cs
    {
        pools[index].SetActive(true); // 잔상 활성화
        SpriteRenderer sr = pools[index].GetComponent<SpriteRenderer>();
        sr.sortingOrder = -2; // 보스의 이미지를 가리면 더이상 잔상이라고 부르기 어렵기 때문에 레이어를 뒤로 보냄
        pools[index].transform.position = position; // 잔상 위치 설정
        sr.sprite = sprite;
        pools[index].transform.localScale = scale; // 멀리 있을 때와 가까이 있을 때에 각각의 잔상의 크기 일치화
        pools[index].GetComponent<FadeOut>().StartFade(lifetime); // 생긴 이후 시간에 따라 fadeout
        index = (index + 1) % size; // 순환 인덱스
    }
}