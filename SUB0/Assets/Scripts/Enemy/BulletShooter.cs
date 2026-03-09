using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour, IResetable
{
    Rigidbody2D rigid;
    [SerializeField] float speed;
    [SerializeField] Vector2 direction;
    Scanner scanner;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        scanner = GetComponent<Scanner>();
        gameObject.SetActive(false);
        
    }

    void FixedUpdate()
    {
        if (!gameObject.activeSelf) // 자기 자신이 활성화되지 않은 상태라면 무시
        {
            Debug.Log("발사불가 활성화 안됨");
            return;
        }

        if (scanner.nearestTarget) // 가까운 타겟이 있다면
        {
            Debug.Log("미사일 발사중");
            direction = (scanner.nearestTarget.position - transform.position).normalized; // 타겟과 자기자신의 위치를 빼서 방향 판정

            rigid.linearVelocity = direction * speed;

            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction); // 바라보는 방향 변경
        }

        if(GameManager.instance.player.isDead) // 플레이어가 죽었으면 자기 자신 비활성화
        {
            gameObject.SetActive(false);
        }
        
    }

    public void Init()
    {
        gameObject.SetActive(false); // ?? init에서 왜 비활성화를 ?
    }

    public void Launch(Vector3 position)
    {
        Debug.Log("미사일 발사!");
        transform.position = position; // 초기 위치 설정
        gameObject.SetActive(true); // FixedUpdate에서 비활성화시 무시하는 구문의 이유
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool player = collision.CompareTag("Player");
        bool terrain = collision.CompareTag("Terrain");
        bool obstacle = collision.CompareTag("Obstacle");
        bool enemy = collision.CompareTag("Enemy");
        if (player) // 플레이어라면
        {
            gameObject.SetActive(false); // 자기자신 비활성화
            collision.GetComponent<Player>().Death(); // 플레이어 사망처리
        }
        else if (enemy) // 적이라면
        {
            gameObject.SetActive(false); // 자기자신 비활성화
            collision.gameObject.SetActive(false); // 부딧힌 객체 비활설화
        }
        else if (terrain || obstacle) // 땅이거나 보스의 공격에 해당하는 오브젝트라면
        {
            gameObject.SetActive(false); // 자기자신 비활성화
        }
    }
}

