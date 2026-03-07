using UnityEngine;

public class FanController : MonoBehaviour, IResetable
{
    Animator animator;
    [SerializeField] Collider2D windSpace;
    [SerializeField] public bool isOnWhenStart = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("애니메이터 에러");
        }

        if (windSpace == null)
        {
            windSpace = GetComponentInChildren<Collider2D>();
        }

        if (isOnWhenStart) 
        { 
            TurnOn(); 
        }
        else
        {
            TurnOff();
        }

    }

    public void TurnOn()
    {
        animator.SetBool("FanOn", true);
        if (windSpace != null) windSpace.enabled = true;
    }

    public void TurnOff()
    {
        animator.SetBool("FanOn", false);
        if (windSpace != null) windSpace.enabled = false;
    }

    public void Init()
    {
        if (isOnWhenStart)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }
}
