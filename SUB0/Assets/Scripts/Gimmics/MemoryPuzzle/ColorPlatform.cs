using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorPlatform : MonoBehaviour
{
    public SymbolData symbolData;  // 이 발판이 가진 심볼
    [SerializeField] private SymbolComparer comparer; // 인스펙터 연결

    private SpriteRenderer spriteRenderer;

    private bool isPressed;
    private bool isInResetState = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // 발판 초기 색/스프라이트 반영
        Init();
    }

    public void Init()
    {
        if (symbolData != null)
        {
            Color fixedColor = symbolData.color;
            fixedColor.a = 1f;

            spriteRenderer.color = fixedColor;

            if (symbolData.sprite != null)
                spriteRenderer.sprite = symbolData.sprite;
            ResetPlatform();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (isPressed || isInResetState) { return; } // 이미 밟혔거나 리셋 중이면 무시

        isPressed = true;
        comparer.AddInput(symbolData);

        if(!isInResetState)
        {
            spriteRenderer.color = Color.yellow;
        }
        
    }
    #region 발판 리셋 관련 코드
    /// <summary>
    /// 발판 리셋 함수
    /// </summary>
    public void ResetPlatform()
    {
        if (symbolData != null)
        {
            spriteRenderer.color = symbolData.color;
        }
    }

    /// <summary>
    /// 틀렸을 때 발판 초기화해주는 함수
    /// </summary>
    /// <param name="color"></param>
    public void ResetColor(Color color)
    {
        StartCoroutine(ChangeColorRed(color));
    }


    IEnumerator ChangeColorRed(Color color)
    {
        isInResetState = true;

        // 빨간색으로 바꿈
        spriteRenderer.color = color;

        yield return new WaitForSeconds(1f); // 빨간색 유지 시간

        ResetPlatform(); // 원래 색 복원
        isInResetState = false;
        isPressed = false;
    }

#endregion
    /// <summary>
    /// 이건 보류
    /// </summary>
    /// <param name="color"></param>
    public void PassColor(Color color)
    {
        var fixedColor = color;
        fixedColor.a = 1f;
        spriteRenderer.color = fixedColor;
    }

}
