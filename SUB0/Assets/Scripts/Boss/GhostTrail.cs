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
        if(timer >= spawnInterval)
        {
            pool.SpawnGhost(sprite.sprite, transform.position, transform.localScale, 0.3f);
            timer = 0f;
        }
    }

    
}
