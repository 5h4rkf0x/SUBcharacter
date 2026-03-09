using NUnit.Framework.Constraints;
using UnityEngine.Tilemaps;
using UnityEngine;

public class BoostEnemy : MonoBehaviour,IResetable
{
    public AudioClip hit;
    public AudioClip death;
    Rigidbody2D rigid;
    Scanner scanner;
    Vector2 direction;
    Vector3 initPos;
    [SerializeField] private float boostSpeed = 4f; // BoostEnemy의 속도를 기존 Enemy의 속도보다 몇배 빠르게 할 것인지
    [SerializeField] int health;

    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        scanner = GetComponent<Scanner>();
        health = 5;
        initPos = transform.position;
    }

    private void Update()
    {
        if (health <= 0) // 죽었을때 효과음 출력 및 객체 비활성화
        {
            GameManager.instance.audioSource.PlayOneShot(death);
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    { 
        if(scanner.nearestTarget) // 가장 가까운 적 가져오기 --> scanner 클래스 참고
        {
            direction = (scanner.nearestTarget.position - transform.position).normalized; // 현재 가장 가까운 적의 방향을 알아내는 로직 --> 가까운 적의 위치와 현재 
            rigid.linearVelocityX = direction.x * boostSpeed; // 빠른 이동속도 적용
        }
        else
        {
            rigid.linearVelocityX = 0; // 플레이어가 없으면 멈춤?
        }
    }

    public void Init() // 적의 기본 속성값 --> 언제 호출되는거지?? --> GameManager.cs 인가? --> 해당 스테이지 진입할때 확인할 것
    {
        health = 5;
        transform.position = initPos;
        scanner.nearestTarget = null; // 가까운 적 초기화
        gameObject.SetActive(true);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Player")) // 플레이어와 부딧힌게 아니라면 판정할 필요 X
        {
            return;
        }

        collision.gameObject.GetComponent<Player>().Death(); // 플레이어와 부딧혔다면 플레이어 사망처리
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) // 총알과 부딧힌게 아니라면 판정할 필요 X
            return;
        GameManager.instance.audioSource.PlayOneShot(hit); // 총알에 맞았다면 hit 효과음 출력
        health -= collision.GetComponent<Bullet>().damage; // 자기 자신의 체력을 총알의 딜만큼 깎음
    }
}

