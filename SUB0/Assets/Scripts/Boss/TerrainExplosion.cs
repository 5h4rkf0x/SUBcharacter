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
            lineWarning.Add(Instantiate(markPrefap, transform)); // boss scene -> boss stage prefab -> terrain explosion
            lineExplode.Add(Instantiate(boomPrefap, transform)); // boss scene -> boss stage prefab -> terrain explosion

            lineWarning[i].SetActive(false); // 아직 비활성화
            lineExplode[i].SetActive(false); // 아직 비활성화
        }
    }

    public void Stop() // 패턴 중지
    {
        StopAllCoroutines(); // 패턴 중지
        for (int i = 0; i < poolSize; i++)
        {
            lineWarning[i].SetActive(false); // 비활성화
            lineExplode[i].SetActive(false); // 비활성화
        }
    }

    public void Init() // 보스 패턴 초기화
    {
        index = 0;
        for(int i =0; i<poolSize; i++)
        {
            lineWarning[i].SetActive(false); // 경고 표시 준비
            lineExplode[i].SetActive(false); // 공격 표시 준비
        }
    }

    public IEnumerator LineExplosion(Vector3 pos) // 지형폭발 패턴 --> boss.cs impact()
    {
        int currentIndex = index;
        index = (index + 1) % poolSize; // 순환 인덱스

        lineWarning[currentIndex].transform.position = new Vector3(0, pos.y, 0); // 경고 표시 출력
        lineWarning[currentIndex].SetActive(true);

        yield return new WaitForSeconds(0.5f); // 여기는 또 그 변수로 처리 안했네 이유?

        lineExplode[currentIndex].transform.position = lineWarning[currentIndex].transform.position; // 공격 위치 설정
        lineWarning[currentIndex].SetActive(false); // 경고표시 비활성화
        lineExplode[currentIndex].SetActive(true); // 공격 실행

        yield return new WaitForSeconds(0.3f); // 0.3s 기다리기

        lineExplode[currentIndex].SetActive(false); // 공격 레이저 비활성화
    }
}
