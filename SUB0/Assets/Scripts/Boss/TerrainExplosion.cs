using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainExplosion : MonoBehaviour, IResetable
{
    public GameObject markPrefap;
    public GameObject boomPrefap;

    public List<GameObject> lineWarning = new List<GameObject>();
    public List<GameObject> lineExplode = new List<GameObject>();

    public int poolSize;
    int index = 0;
    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            lineWarning.Add(Instantiate(markPrefap, transform));
            lineExplode.Add(Instantiate(boomPrefap, transform));

            lineWarning[i].SetActive(false);
            lineExplode[i].SetActive(false);
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
        for (int i = 0; i < poolSize; i++)
        {
            lineWarning[i].SetActive(false);
            lineExplode[i].SetActive(false);
        }
    }

    public void Init()
    {
        index = 0;
        for(int i =0; i<poolSize; i++)
        {
            lineWarning[i].SetActive(false);
            lineExplode[i].SetActive(false);
        }
    }

    public IEnumerator LineExplosion(Vector3 pos)
    {
        int currentIndex = index;
        index = (index + 1) % poolSize;

        lineWarning[currentIndex].transform.position = new Vector3(0, pos.y, 0);
        lineWarning[currentIndex].SetActive(true);

        yield return new WaitForSeconds(0.5f);

        lineExplode[currentIndex].transform.position = lineWarning[currentIndex].transform.position;
        lineWarning[currentIndex].SetActive(false);
        lineExplode[currentIndex].SetActive(true);

        yield return new WaitForSeconds(0.3f);

        lineExplode[currentIndex].SetActive(false);

    }
}
