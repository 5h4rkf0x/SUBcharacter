using System.Linq;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour, IResetable
{
    [SerializeField] private SymbolComparer symbolComparer;
    [SerializeField] private PatternUIManager patternUIManager;

    [SerializeField] private bool isTriggered = false;

    public void Init()
    {
        isTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true;

            // 퍼즐 초기화
            symbolComparer.ResetPuzzle();

            // UI에 패턴 보여주기
            patternUIManager.StartPuzzle(symbolComparer.correctSymbol.ToList());
        }
    }
}
