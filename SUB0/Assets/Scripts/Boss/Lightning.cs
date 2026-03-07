using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Lightning : MonoBehaviour,IResetable
{
    public GameObject markPrefap;
    public GameObject lazer_YPrefap;
    public GameObject lazer_BPrefap;

    [SerializeField] List<GameObject> warningMark1 = new List<GameObject>();
    [SerializeField] List<GameObject> warningMark2 = new List<GameObject>();
    [SerializeField] List<GameObject> yellowLazer = new List<GameObject>();
    [SerializeField] List<GameObject> blueLazer = new List<GameObject>();

    float delayBeforeStrike = 0.5f;
    int poolSize = 20;
    int yellowIndex = 0;
    int blueIndex = 0;


    private void Start()
    {
        for(int i = 0; i< poolSize; i++)
        {
            warningMark1.Add(Instantiate(markPrefap, transform));
            warningMark2.Add(Instantiate(markPrefap, transform));
            yellowLazer.Add(Instantiate(lazer_YPrefap, transform));
            blueLazer.Add(Instantiate(lazer_BPrefap, transform));

            warningMark1[i].SetActive(false);
            warningMark2[i].SetActive(false);
            yellowLazer[i].SetActive(false);
            blueLazer[i].SetActive(false);
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
        yellowIndex = 0;
        blueIndex = 0;
        for (int i = 0; i < poolSize; i++)
        {
            warningMark1[i].SetActive(false);
            warningMark2[i].SetActive(false);
            yellowLazer[i].SetActive(false);
            blueLazer[i].SetActive(false);
        }
    }

    public void Init()
    {
        StopAllCoroutines();
        yellowIndex = 0;
        blueIndex = 0;
        for(int i = 0; i < poolSize; i++)
        {
            warningMark1[i].SetActive(false);
            warningMark2[i].SetActive(false);
            yellowLazer[i].SetActive(false);
            blueLazer[i].SetActive(false);
        }
    }

    public IEnumerator YellowLightningRoutine(Vector3 pos)
    {
        int index = yellowIndex;
        yellowIndex = (yellowIndex + 1) % poolSize;
        warningMark1[index].transform.position = new Vector3(pos.x, transform.position.y);
        warningMark1[index].SetActive(true);

        yield return new WaitForSeconds(delayBeforeStrike);

        yellowLazer[index].transform.position = warningMark1[index].transform.position;
        yellowLazer[index].SetActive(true);
        warningMark1[index].SetActive(false);
        
    }

    public IEnumerator BlueLightningRoutine(Vector3 pos)
    {
        int index = blueIndex;
        blueIndex = (blueIndex + 1) % poolSize;
        warningMark2[index].transform.position = new Vector3(pos.x, transform.position.y);
        warningMark2[index].SetActive(true);

        yield return new WaitForSeconds(1f);

        blueLazer[index].transform.position = warningMark2[index].transform.position;
        blueLazer[index].SetActive(true);
        warningMark2[index].SetActive(false);
        
    }
}
