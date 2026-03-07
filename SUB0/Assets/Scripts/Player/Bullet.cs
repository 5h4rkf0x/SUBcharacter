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
        GameManager.instance.audioSource.PlayOneShot(shot);
        dir = dir.normalized;
        rigid.linearVelocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
