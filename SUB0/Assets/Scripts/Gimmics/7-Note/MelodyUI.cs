using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Audio;


public class MelodyUI : MonoBehaviour, IResetable
{
    public Image[] melodySymbols;
    AudioSource audioSource; // 오디오 소스

    [SerializeField] private float ShowSymbolTime = 3f; // 화면에 보여주는 시간

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartMelody(List<MelodyData> pattern)
    {
        Debug.Log("[MelodyUI] StartMelody 호출됨, md.Count=" + pattern.Count);
        Init();
        StartCoroutine(ShowMelodyUI(pattern));
    }

    public void Init()
    {
        StopAllCoroutines();
        for (int x = 0; x < melodySymbols.Length; x++)
        {
            melodySymbols[x].gameObject.SetActive(false);
        }
    }


    public IEnumerator ShowMelodyUI(List<MelodyData> md)
    {
        Debug.Log("[MelodyUI] ShowMelodyUI 시작됨!");

        // 1. 전체 꺼주기
        for (int i = 0; i < melodySymbols.Length; i++)
            melodySymbols[i].gameObject.SetActive(false);

        // 2. 0~6 중 랜덤으로 3개 인덱스 뽑기
        List<int> selectedIndices = Enumerable.Range(0, melodySymbols.Length)
                                              .OrderBy(_ => Random.value)
                                              .Take(md.Count)
                                              .ToList();

        for (int i = 0; i < md.Count; i++)
        {
            var data = md[i];
            int slotIndex = selectedIndices[i];

            if (data.sprite != null)
            {
                melodySymbols[slotIndex].sprite = data.sprite;
                melodySymbols[slotIndex].color = data.color;
                melodySymbols[slotIndex].gameObject.SetActive(true);
                melodySymbols[slotIndex].transform.SetAsFirstSibling();

                if (data.noteAudio != null && audioSource != null)
                    audioSource.PlayOneShot(data.noteAudio);

                yield return new WaitForSeconds(ShowSymbolTime);

                melodySymbols[slotIndex].gameObject.SetActive(false);
            }
        }

        // 끝나고도 혹시 모르니 다 꺼버리기
        for (int i = 0; i < melodySymbols.Length; i++)
            melodySymbols[i].gameObject.SetActive(false);
    }
}
