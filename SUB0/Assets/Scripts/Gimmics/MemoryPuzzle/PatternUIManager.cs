using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.BoolParameter;


public class PatternUIManager : MonoBehaviour, IResetable
{
    public Image[] symbolImages;
    bool reset = false;

    [SerializeField] private float ShowSymbolTime = 3f; // 화면에 보여주는 시간

    public void StartPuzzle(List<SymbolData> pattern)
    {
        Debug.Log("[PatternUIManager] StartPuzzle 호출됨, pattern.Count=" + pattern.Count);
        StartCoroutine(ShowPatternUI(pattern));
    }

    public void Init()
    {
        reset = true;
        for (int x = 0; x < symbolImages.Length; x++)
        {
            symbolImages[x].gameObject.SetActive(false);
        }
    }

    public IEnumerator ShowPatternUI(List<SymbolData> pattern)
    {
        Debug.Log("[PatternUIManager] ShowPatternUI 시작됨!");

        for (int i = 0; i < pattern.Count && i < symbolImages.Length; i++)
        {
            var data = pattern[i];
         
            if (data.sprite != null)
            {
                var fixedColor = data.color;
                fixedColor.a = 1f;
                symbolImages[i].sprite = data.sprite;
                symbolImages[i].color = fixedColor;
                symbolImages[i].gameObject.SetActive(true);
                symbolImages[i].transform.SetAsFirstSibling();      // ui재시작 테스트 코드
                if (reset)
                {
                    reset = false;
                    for (int x = 0; x < symbolImages.Length; x++)
                    {
                        symbolImages[x].gameObject.SetActive(false);
                    }
                    yield break;
                }

                yield return new WaitForSeconds(ShowSymbolTime);

                symbolImages[i].gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning($"[PatternUIManager] SymbolData {i}에 스프라이트가 없음!");
            }

            
        }
    }

}
