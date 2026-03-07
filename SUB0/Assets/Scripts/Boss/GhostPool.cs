using System.Collections.Generic;
using UnityEngine;

public class GhostPool : MonoBehaviour,IResetable
{
    public GameObject ghostPrefab;
    public int size = 10;
    public List<GameObject> pools;
    int index = 0;

    private void Awake()
    {
        pools = new List<GameObject>();
        for (int i = 0; i < size; i++)
        {
            pools.Add(Instantiate(ghostPrefab,transform));
            pools[i].SetActive(false);
        }
    }

    public void Init()
    {
        index = 0;
        for(int i = 0; i< size; i++)
        {
            pools[i].SetActive(false);
        }
    }

    public void SpawnGhost(Sprite sprite, Vector3 position, Vector3 scale, float lifetime)
    {
        pools[index].SetActive(true);
        SpriteRenderer sr = pools[index].GetComponent<SpriteRenderer>();
        sr.sortingOrder = -2;
        pools[index].transform.position = position;
        sr.sprite = sprite;
        pools[index].transform.localScale = scale;
        pools[index].GetComponent<FadeOut>().StartFade(lifetime);
        index = (index + 1) % size;
    }
}
