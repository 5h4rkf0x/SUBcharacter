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
        if (isOpen) return;
        isOpen = true;

        sr.sprite = openSprite;

        if (doorCollider != null)
            doorCollider.enabled = false;

        Debug.Log("[ExitDoor] ¹® ¿­¸²!");
    }

    public void CloseDoor()
    {
        if (!isOpen) return;
        isOpen = false;

        sr.sprite = closedSprite;
        if (doorCollider != null) doorCollider.enabled = true;

        Debug.Log("[ExitDoor] ¹® ´ÝÈû!");
    }
}
