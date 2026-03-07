using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SymbolComparer : MonoBehaviour, IResetable
{
    [Header("퍼즐 기본 설정")]
    public SymbolData[] correctSymbol;
    public SymbolData[] inputSymbol;

    [Header("퍼즐 관련 오브젝트")]
    [SerializeField] private ColorPlatform[] platforms; // 발판들 인스펙터에 연결
    Animator animator;
    [SerializeField] private Sprite[] symbolSprites; // 스프라이트 배열 추가
    private PatternUIManager patternUIManager;

    public Vector3 initPos;

    [SerializeField] private int puzzleLength = 4;
    private int inputIndex = 0;
    private float resetTime = 1f;       // 틀렸을 때 1-2초 카운트


    public void Init()
    {
        ResetPuzzle();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        patternUIManager = GetComponent<PatternUIManager>();
        initPos = transform.localPosition;
        GenerateCorrectPattern();
    }

    public void AddInput(SymbolData data)
    {
        if (inputIndex < inputSymbol.Length)
        {
            // 알파값 1로 보정
            Color fixedColor = data.color;
            fixedColor.a = 1f;

            inputSymbol[inputIndex] = new SymbolData(data.symbol, fixedColor, data.sprite);
            inputIndex++;

            if (inputIndex >= correctSymbol.Length)
            {
                CheckAnswer();
            }
        }
    }

    private void CheckAnswer()
    {
        bool isCorrect = true;

        for (int i = 0; i < correctSymbol.Length; i++)
        {
            if (inputSymbol[i] == null || inputSymbol[i].symbol != correctSymbol[i].symbol)
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
           
            Debug.Log("퍼즐 클리어!");
            animator.SetBool("Correct",true);

        }
        else
        {
            
            Debug.Log("틀림 퍼즐 리셋");
            
            StartCoroutine(ColorResetCoroutine());

        }
    }

    public void ResetPuzzle()
    {
        inputIndex = 0;
        for (int i = 0; i < inputSymbol.Length; i++)
        {
            inputSymbol[i] = null;
        }

        foreach (var p in platforms)
        {
            p.ResetPlatform(); // 발판 색 복원
        }

        GenerateCorrectPattern();
        animator.SetBool("Correct", false);
        transform.localPosition = initPos;
        

    }

    private void GenerateCorrectPattern()
    {
        Symbol[] symbols = new Symbol[] { Symbol.Magenta, Symbol.Red, Symbol.Green, Symbol.Blue };
        System.Array.Sort(symbols, (a, b) => Random.value.CompareTo(Random.value));

        correctSymbol = new SymbolData[puzzleLength];
        inputSymbol = new SymbolData[puzzleLength];
        inputIndex = 0;

        for (int i = 0; i < puzzleLength; i++)
        {
            Color color = GetColor(symbols[i]);
            Sprite sprite = GetSprite(symbols[i]); // 스프라이트 가져오기
            correctSymbol[i] = new SymbolData(symbols[i], color, sprite);
        }


    }

    private Color GetColor(Symbol symbol)
    {
        switch (symbol)
        {
            case Symbol.Magenta: return Color.magenta;
            case Symbol.Red: return Color.red;
            case Symbol.Green: return Color.green;
            case Symbol.Blue: return Color.blue;
            default: return Color.white;
        }
    }

    private Sprite GetSprite(Symbol symbol)
    {
        switch (symbol)
        {
            case Symbol.Magenta: return symbolSprites.Length > 0 ? symbolSprites[0] : null;
            case Symbol.Red: return symbolSprites.Length > 1 ? symbolSprites[1] : null;
            case Symbol.Green: return symbolSprites.Length > 2 ? symbolSprites[2] : null;
            case Symbol.Blue: return symbolSprites.Length > 3 ? symbolSprites[3] : null;
            default: return null;
        }
    }

    IEnumerator ColorResetCoroutine()
    {
        foreach (var plat in platforms)
        {
            Debug.Log($"{plat.gameObject.name} -> Red");
            plat.ResetColor(Color.red);
        }
        yield return new WaitForSeconds(resetTime);

        foreach (var plat in platforms)
        {
            plat.ResetPlatform(); // 원래 색상 복원
        }

        GenerateCorrectPattern();

        if(patternUIManager != null)
        {
            patternUIManager.StartPuzzle(correctSymbol.ToList());
            Debug.Log("[SymbolComparer] 퍼즐 리셋됨 → UI 재시작");
        }
    }

}
