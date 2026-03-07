using UnityEngine;

public class Bojo : MonoBehaviour, IResetable
{
    public AudioClip engage;
    Animator animator;
    Boss boss;
    AnimatorStateInfo stateInfo;

    public bool farAway = false;
  

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boss = GetComponentInChildren<Boss>();
        
    }

    public void Init()
    {
        animator.Play("StandBy", 0,0f);
        animator.SetBool("PlayerDead", false);
        farAway = false;
    }

    public void Ready()
    {
        Debug.Log("패턴 준비");
        boss.attacking = false;
        animator.SetBool("EndAttack", false);
    }

    public void Engage()
    {
        GameManager.instance.audioSource.PlayOneShot(engage);
    }

    private void Update()
    {
        if (boss.isDead) return;

        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if(stateInfo.IsName("ScaleDown"))
        {
            farAway = true;
        }
        else if(stateInfo.IsName("ScaleUp"))
        {
            farAway = false;
        }

        if(GameManager.instance.player.isDead)
        {
            animator.SetBool("PlayerDead", true);
        }

        if(boss.isDead)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        transform.position = new Vector3(0, 0, 0);
    }
}
