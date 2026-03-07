using System.Linq;
using UnityEngine;

public class MelodyTrigger : MonoBehaviour, IResetable
{
    [SerializeField] private MelodyComparer melodyComparer;
    [SerializeField] private MelodyUI melodyUI;
    [SerializeField] private MelodyDoor melodyDoor;

    [SerializeField] BooTrigger booTrigger;

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

            melodyDoor.CloseDoor();

            booTrigger.ActivateShy();

            // 퍼즐 초기화
            melodyComparer.ResetMelody();

            // UI에 패턴 보여주기
            melodyUI.StartMelody(melodyComparer.correctMelodys.ToList());
        }
    }
}
