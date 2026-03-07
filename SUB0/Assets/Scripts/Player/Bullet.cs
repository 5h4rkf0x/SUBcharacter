using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip shot;
    Rigidbody2D rigid;

    public int damage;
    public float speed = 10.0f;
    

    private void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 dir)
    {
        // 총알이 발사될 때 효과음 출력 및 총알의 이동속도 설정
        GameManager.instance.audioSource.PlayOneShot(shot);
        dir = dir.normalized;
        rigid.linearVelocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 어떤태그에 부딧혔는가에 대한 불값을 저장하고 해당 총알을 비활성화 및 활성화된 총알의 개수 -1
        bool border = collision.gameObject.CompareTag("Border");
        bool terrain = collision.gameObject.CompareTag("Terrain");
        bool enemy = collision.gameObject.CompareTag("Enemy");
        bool boss = collision.gameObject.CompareTag("Boss");

        if (!(border || terrain || enemy || boss))
            return;

        GameManager.instance.bulletManager.Hit(transform.position);
        gameObject.SetActive(false);
        GameManager.instance.bulletManager.activeBullet--;
    }
}
