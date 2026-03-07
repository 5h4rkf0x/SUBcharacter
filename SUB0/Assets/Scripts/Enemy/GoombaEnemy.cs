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
        if(health <= 0)
        {
            
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {   
        float moveDir = IsMoveRight ? 1f : -1f;     // 움직이는 방향이 같으면 왼쪽으로 1, 다르면 오른쪽으로 -1
        rigid.linearVelocity = new Vector2(moveDir * moveSpeed, rigid.linearVelocity.y);

        float distanceMoved = transform.position.x - startPos.x;

        if (Mathf.Abs(distanceMoved) >= moveDistance)
        {
            IsMoveRight = !IsMoveRight;
            startPos = transform.position;
            render.flipX = !render.flipX;
        }
    }

    public void Init()
    {
        health = 2;
        transform.position = startPos;
        gameObject.SetActive(true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;
        GameManager.instance.audioSource.PlayOneShot(hit);
        health -= collision.GetComponent<Bullet>().damage;

        if(health <=0)
        {
            GameManager.instance.audioSource.PlayOneShot(death);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        collision.gameObject.GetComponent<Player>().Death();
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