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


    private void Start() // 시작 초기화
    {
        for(int i = 0; i< poolSize; i++)
        {
            warningMark1.Add(Instantiate(markPrefap, transform)); // 경고 표시가 왜 두종류지 프리펩은 한가지인데
            warningMark2.Add(Instantiate(markPrefap, transform));
            yellowLazer.Add(Instantiate(lazer_YPrefap, transform));
            blueLazer.Add(Instantiate(lazer_BPrefap, transform));

            warningMark1[i].SetActive(false); // 준비과정
            warningMark2[i].SetActive(false);
            yellowLazer[i].SetActive(false);
            blueLazer[i].SetActive(false);
        }
    }

<<<<<<< HEAD
<<<<<<< HEAD
    public void Stop() // 정지 함수 --> 초기화
=======
    public void Stop() // 죽었을때 초기화?
>>>>>>> 331f0fa70f56fbd1683b968e9589f050f21e3f2e
=======
    public void Stop() // 죽었을때 초기화?
>>>>>>> 331f0fa70f56fbd1683b968e9589f050f21e3f2e
    {
        StopAllCoroutines(); // 실행중인 모든 코루틴 정지
        yellowIndex = 0; 
        blueIndex = 0;
        for (int i = 0; i < poolSize; i++) // 다시 준비과정으로
        {
            warningMark1[i].SetActive(false);
            warningMark2[i].SetActive(false);
            yellowLazer[i].SetActive(false);
            blueLazer[i].SetActive(false);
        }
    }

<<<<<<< HEAD
<<<<<<< HEAD
    public void Init() // IResetable 필수 함수 --> 초기화
=======
    public void Init() // r 누를때 초기화
>>>>>>> 331f0fa70f56fbd1683b968e9589f050f21e3f2e
=======
    public void Init() // r 누를때 초기화
>>>>>>> 331f0fa70f56fbd1683b968e9589f050f21e3f2e
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

    public IEnumerator YellowLightningRoutine(Vector3 pos) // 세로줄 일반 공격
    {
        int index = yellowIndex; // 경고 표시 활성화
        yellowIndex = (yellowIndex + 1) % poolSize; // 순환 인덱스
        warningMark1[index].transform.position = new Vector3(pos.x, transform.position.y); // 어떻게 플레이어 위치에 떨어지는가?
        warningMark1[index].SetActive(true);

        yield return new WaitForSeconds(delayBeforeStrike); // delayBeforeStrike(0.5s) 기다리기

        yellowLazer[index].transform.position = warningMark1[index].transform.position; // 실제 타격
        yellowLazer[index].SetActive(true);
        warningMark1[index].SetActive(false);
        
    }

    public IEnumerator BlueLightningRoutine(Vector3 pos) // YellowLightningRoutine()과 동일 구조
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
