using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;

    private SpriteRenderer sr;
    private Collider2D doorCollider;
    private bool isOpen = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<Collider2D>();

        sr.sprite = closedSprite;
        if (doorCollider != null) doorCollider.enabled = true;
    }

    public void OpenDoor()
    {
        if (isOpen) return; // 닫혔을 때만 작동
        isOpen = true;

        sr.sprite = openSprite;

        if (doorCollider != null) // 충돌판정 없애주기
            doorCollider.enabled = false;

        Debug.Log("[ExitDoor] 문 열림!");
    }

    public void CloseDoor()
    {
        if (!isOpen) return; // 열렸을 때만 작동
        isOpen = false;

        sr.sprite = closedSprite;
        if (doorCollider != null) doorCollider.enabled = true; // 충돌판정 원래대로

        Debug.Log("[ExitDoor] 문 닫힘!");
    }
}
