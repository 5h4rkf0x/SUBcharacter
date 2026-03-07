using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MelodyDoor : MonoBehaviour, IResetable
{
    [SerializeField] Animator animator;
    Vector3 pos;

    void Start()
    {
        pos = transform.position;
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetTrigger("Clear");

    }

    public void CloseDoor()
    {
        animator.SetTrigger("Enter");
    }

    public void Init()
    {
        OpenDoor();
    }
}
