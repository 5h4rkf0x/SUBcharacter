using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    Transform Transform;
    SpriteRenderer sprite;
    public GhostPool pool;
    public Vector3 currentPos;

    public float spawnInterval = 0.1f;
    float timer;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        Transform = GetComponent<Transform>();
    }

    private void Update()
    {
        currentPos = Transform.position;
        timer += Time.deltaTime;
        if(timer >= spawnInterval) // 0.1s 마다 잔상 활성화
        {
<<<<<<< HEAD
<<<<<<< HEAD
            pool.SpawnGhost(sprite.sprite, transform.position, transform.localScale, 0.3f); // 
=======
            pool.SpawnGhost(sprite.sprite, transform.position, transform.localScale, 0.3f); // 0.3초동안 서서히 사라지는 보스의 잔상 생성
>>>>>>> 331f0fa70f56fbd1683b968e9589f050f21e3f2e
=======
            pool.SpawnGhost(sprite.sprite, transform.position, transform.localScale, 0.3f); // 0.3초동안 서서히 사라지는 보스의 잔상 생성
>>>>>>> 331f0fa70f56fbd1683b968e9589f050f21e3f2e
            timer = 0f;
        }
    }

    
}
