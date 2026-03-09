using UnityEngine;


public class ShyEnemy : MonoBehaviour,IResetable
{

    [SerializeField] Sprite[] sprites;
    SpriteRenderer sprite;
    Scanner scanner;
    Rigidbody2D rigid;
    [SerializeField] Vector3 initPos;
    Vector2 direction;

    [SerializeField] private float moveSpeed = 3.0f;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        scanner = GetComponent<Scanner>();
        rigid = GetComponent<Rigidbody2D>();

        sprite.sprite = sprites[0];
        // transform.position = initPos;
    }

    private void Update()
    {
        bool playerFacingRight = GameManager.instance.player.gun.facingRight;

        if (scanner.nearestTarget) // 가까운 타겟
        {
            direction = (scanner.nearestTarget.position - transform.position).normalized; // 방향
            if(playerFacingRight)  // 플레이어가 바라보는 방향과 ShyEnemy의 진행방향에 따른 스프라이트 이미지 정렬 및 이동
            {
                if(direction.x >0)
                {
                    sprite.sprite = sprites[0];
                    sprite.flipX = false;
                    rigid.linearVelocity = direction * moveSpeed;
                }
                else
                {
                    sprite.sprite = sprites[1];
                    rigid.linearVelocity = Vector2.zero;
                }
            }
            else
            {
                if(direction.x < 0)
                {
                    sprite.sprite = sprites[0];
                    sprite.flipX = true;
                    rigid.linearVelocity = direction * moveSpeed;
                }
                else
                {
                    sprite.sprite = sprites[1];
                    rigid.linearVelocity = Vector2.zero;
                }

            }
        }
        else // 왜 플레이어가 ShyEnemy보다 왼쪽으로 갔을때 오류가 생기지 않는가??
        {
            rigid.linearVelocity = Vector2.zero;
        }
    }

    public void Init()
    {
        // 컴포넌트가 없으면 확보 --> if 구문의 존재 이유?
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (rigid == null) rigid = GetComponent<Rigidbody2D>();
        if (scanner == null) scanner = GetComponent<Scanner>();

        sprite.sprite = sprites[0];
        transform.position = initPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) // 플레이어와 부딧힌게 아니라면 무시
            return;

        collision.GetComponent<Player>().Death(); // 플레이어 사망 처리
    }
}
