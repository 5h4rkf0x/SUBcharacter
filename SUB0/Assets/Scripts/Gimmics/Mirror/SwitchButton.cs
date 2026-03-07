using UnityEngine;

public class SwitchButton : MonoBehaviour, IResetable
{
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite pressedSprite;
    private SpriteRenderer sr;

    public bool IsPressed { get; private set; } = false;

    public void Init()
    {
        IsPressed = false;
        sr.sprite = idleSprite;
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = idleSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MirrorPlayer"))
        {
            IsPressed = true;
            sr.sprite = pressedSprite;
            SwitchComparer.Instance.CheckSwitches();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MirrorPlayer"))
        {
            IsPressed = false;
            sr.sprite = idleSprite;
            SwitchComparer.Instance.CheckSwitches();
        }
    }
}
