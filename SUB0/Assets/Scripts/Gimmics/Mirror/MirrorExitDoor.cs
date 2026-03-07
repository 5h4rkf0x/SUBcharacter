using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 보스씬 관련 코드는 일단 주석처리 했습니당
/// </summary>

public class MirrorExitDoor : MonoBehaviour, IResetable
{
    [Header("문 상태 스프라이트")]
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;

    [Header("보스방 씬 이름")]
    [SerializeField] public string bossSceneName = "Boss";

    [Header("거울방 관련 연결")]
    public MirrorTrigger mirrorTrigger;

    private SpriteRenderer sr;
    private Collider2D doorCollider;

    private bool isOpen = false;

    public bool IsOpen => isOpen;

    public void Init()
    {
        CloseDoor();
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<Collider2D>();

        // 시작할 땐 무조건 닫혀 있음
        sr.sprite = closedSprite;
        if (doorCollider != null) doorCollider.enabled = true;
    }

    public void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;
        sr.sprite = openSprite;
        if (doorCollider != null) doorCollider.enabled = false;

        Debug.Log("[MirrorExitDoor] 문 열림!");
    }

    public void CloseDoor() // 필요 없으면 삭제해도 됨
    {
        isOpen = false;
        sr.sprite = closedSprite;
        if (doorCollider != null) doorCollider.enabled = true;

        Debug.Log("[MirrorExitDoor] 문 닫힘!");
    }
}
