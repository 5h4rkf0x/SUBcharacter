using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Audio;

public class GoombaEnemy : MonoBehaviour, IResetable
{
    public AudioClip hit;
    public AudioClip death;
    Rigidbody2D rigid;
    SpriteRenderer render;

    [SerializeField] private float moveSpeed;
    [SerializeField] int health;
    float moveDistance = 2.0f;   // 움직일 거리
   
    private bool IsMoveRight = true;
    private Vector3 startPos;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        health = 2;
    }
    // 지알아서 특정 범위에서만 움직여야 함

    void Update()
    {
        if(health <= 0) // 사망시 비활성화
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {   
        float moveDir = IsMoveRight ? 1f : -1f;     // 움직이는 방향이 오른쪽이면 1, -1
        rigid.linearVelocity = new Vector2(moveDir * moveSpeed, rigid.linearVelocity.y); // 이동 로직

        float distanceMoved = transform.position.x - startPos.x; // 얼마나 이동했는지

        if (Mathf.Abs(distanceMoved) >= moveDistance) // 일정 범위 이상 움직였을 때;
        {
            IsMoveRight = !IsMoveRight; // 38 Line 을 통해 반대로 움직일 수 있게
            startPos = transform.position; // 현재 위치를 시작 위치로 변경
            render.flipX = !render.flipX; // 잘 모르겠다 --> 스프라이트 뒤집어 주는건가?
        }
    }

    public void Init() // 속성값 초기화
    {
        health = 2;
        transform.position = startPos;
        gameObject.SetActive(true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) // bullet 태그를 가진 녀석과 충돌한 것이 아니라면 무시
            return;
        GameManager.instance.audioSource.PlayOneShot(hit); // hit 효과음 출력
        health -= collision.GetComponent<Bullet>().damage; // 체력을 bullet의 데미지만큼 -

        if(health <=0) // 사망시
        {
            GameManager.instance.audioSource.PlayOneShot(death); // 사망 효과음 출력
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) // 플레이어와 충돌하지 않았다면 무시
            return;

        collision.gameObject.GetComponent<Player>().Death(); // 플레이어와 충돌시 사망
    }

}

/// <summary>
/// 굼바 특징
/// 특정위치에서만 움직임
/// 지알아서 움직임 (ㄹㅇ AI)
/// 좌우로만 움직임
/// 그러면? 몇칸 범위를 하지
/// 
/// </summary>