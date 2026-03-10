using UnityEngine;

public class Bojo : MonoBehaviour, IResetable // Init 함수 필수 생성
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

    public void Init() // 초기화
    {
        animator.Play("StandBy", 0,0f);
        animator.SetBool("PlayerDead", false);
        farAway = false;
    }

    public void Ready() // 패턴 준비
    {
        Debug.Log("패턴 준비");
        boss.attacking = false; // 일반 공격 중단
        animator.SetBool("EndAttack", false); // 패턴 공격이 끝나지 않았음
    }

    public void Engage() // 등장할때 한번 실행
    {
        GameManager.instance.audioSource.PlayOneShot(engage);
    }

    private void Update()
    {
        if (boss.isDead) return; // 보스가 죽어있다면 무시

        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if(stateInfo.IsName("ScaleDown"))
        {
            farAway = true; // 패턴할때 뒤로가기 --> 사격불가
        }
        else if(stateInfo.IsName("ScaleUp"))
        {
            farAway = false; // 패턴 끝나고 다시 앞으로 --> 사격 가능
        }

        if(GameManager.instance.player.isDead)
        {
            animator.SetBool("PlayerDead", true); // 플레이어 사망
        }

        if(boss.isDead)
        {
            OnDeath(); // 보스 사망처리
        }
    }

    public void OnDeath()
    {
        transform.position = new Vector3(0, 0, 0); // 보스 위치를 000으로 변경....?
    }
}
