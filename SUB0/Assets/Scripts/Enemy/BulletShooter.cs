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
        if (!gameObject.activeSelf)
        {
            Debug.Log("발사불가 활성화 안됨");
            return;
        }

        if (scanner.nearestTarget)
        {
            Debug.Log("미사일 발사중");
            direction = (scanner.nearestTarget.position - transform.position).normalized;

            rigid.linearVelocity = direction * speed;

            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }

        if(GameManager.instance.player.isDead)
        {
            gameObject.SetActive(false);
        }
        
    }

    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void Launch(Vector3 position)
    {
        Debug.Log("미사일 발사!");
        transform.position = position;
        gameObject.SetActive(true);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool player = collision.CompareTag("Player");
        bool terrain = collision.CompareTag("Terrain");
        bool obstacle = collision.CompareTag("Obstacle");
        bool enemy = collision.CompareTag("Enemy");
        if (player)
        {
            gameObject.SetActive(false);
            collision.GetComponent<Player>().Death();
        }
        else if (enemy)
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
        else if (terrain || obstacle)
        {
            gameObject.SetActive(false);
        }
    }
}

