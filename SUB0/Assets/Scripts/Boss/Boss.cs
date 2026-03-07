using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class Boss : MonoBehaviour,IResetable
{
    public GameObject laserScan;
    Rigidbody2D rigid;
    Animator animator;
    public Sprite[] sprites;
    SpriteRenderer sprite;
    Collider2D coll;
    Bojo bojo;
    List<System.Func<IEnumerator>> closePatterns = new List<System.Func<IEnumerator>>();
    List<System.Func<IEnumerator>> farAwayPatterns = new List<System.Func<IEnumerator>>();
    
    public Lightning lightning;
    public TerrainExplosion explosion;
    public DeathEffectPool deathEffect;
    public Widen[] points;
    Vector3 originalScale;
    Vector3 farAwayScale;
    Vector3 initPos;
    
    public int health;
    public int maxHealth;

    public bool engage = false;
    public bool attacking = true;
    public bool isDead = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        bojo = GetComponentInParent<Bojo>();
        coll = GetComponent<Collider2D>();

        

        health = maxHealth;
        originalScale = transform.localScale;
        farAwayScale = transform.localScale / 30f;
        initPos = transform.position;
    }

    private void Start()
    {
        closePatterns.Add(BasicPattern);
        farAwayPatterns.Add(BasicWidenPattern);
        closePatterns.Add(FasterLaser);
        farAwayPatterns.Add(Impact);
    }

    void Update()
    {
        AnimationControl();
        if(!GameManager.instance.player.isDead)
        {
            if (!attacking && !isDead)
            {
                Debug.Log("패턴 시작");
                attacking = true;

                if (bojo.farAway)
                {
                    int index = Random.Range(0, 2);
                    StartCoroutine(farAwayPatterns[index]());
                }
                else
                {
                    int index = Random.Range(0, 2);
                    StartCoroutine(closePatterns[index]());
                }
            }
        }
        else
        {
            StopAllCoroutines();
            sprite.sprite = sprites[0];
            lightning.Stop();
            explosion.Stop();
            bojo.GetComponent<Animator>().SetBool("EndAttack", true);
        }

        
        
    }

    public void Init()
    {
        GameManager.instance.audioSource.Stop();
        GameManager.instance.BGM.loop = false;
        GameManager.instance.BGM.Stop();
        StopAllCoroutines();
        bojo.GetComponent<Animator>().ResetTrigger("Engage");
        engage = false;
        transform.position = initPos;
        transform.localScale = originalScale;
        health = maxHealth;
        attacking = true;
    }

    public void AnimationControl()
    {
        if(bojo.farAway)
        {
            transform.localScale = farAwayScale;
            coll.enabled = false;
        }
        else
        {
            transform.localScale = originalScale;
            coll.enabled = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;
        animator.SetTrigger("Hit");
        StartCoroutine(Hit());
        collision.gameObject.SetActive(false);
        GameManager.instance.bulletManager.activeBullet--;
        health -= collision.GetComponent<Bullet>().damage;
        if (health <= 0)
        {
            GameManager.instance.BGM.loop = false;
            GameManager.instance.BGM.Stop();
            isDead = true;
            rigid.simulated = false;
            coll.enabled = false;
            StopAllCoroutines();
            lightning.Stop();
            explosion.Stop();
            bojo.GetComponent<Animator>().Play("Death");
            bojo.OnDeath();
            bojo.GetComponent<Animator>().SetTrigger("Death");
            transform.position = new Vector3(0, 0, 0);
            sprite.sprite = sprites[0];
            StartCoroutine(deathEffect.Death());
        }

    }

    IEnumerator BasicPattern()
    {
        Debug.Log("3 쉬고 5");
        int count = 0;

        while(count < 3)
        {
            StartCoroutine(lightning.YellowLightningRoutine(GameManager.instance.player.transform.position));
            yield return new WaitForSeconds(0.5f);
            count++;
        }

        count = 0;
        yield return new WaitForSeconds(0.5f);

        while(count < 20)
        {
            StartCoroutine(lightning.YellowLightningRoutine(GameManager.instance.player.transform.position));
            yield return new WaitForSeconds(0.1f);
            count++;
        }

        bojo.GetComponent<Animator>().SetBool("EndAttack",true);
    }

    IEnumerator BasicWidenPattern()
    {
        Debug.Log("개간지 광역기");
        GameManager.instance.audioSource.volume = 0.5f;
        attacking = true;
        int count = 0;
        while(count < points[0].point.Count)
        {
            StartCoroutine(lightning.BlueLightningRoutine(points[0].point[count]));
            yield return new WaitForSeconds(0.01f);
            count++;
        }

        yield return new WaitForSeconds(2.5f);

        bojo.GetComponent<Animator>().SetBool("EndAttack", true);
        GameManager.instance.audioSource.volume = 1f;
    }

    IEnumerator FasterLaser()
    {
        Debug.Log("점점 빨라질 것이다");
        attacking = true;
        int count = 0;
        float delay = 1f;
        while(count < 20)
        {
            StartCoroutine(lightning.YellowLightningRoutine(GameManager.instance.player.transform.position));
            yield return new WaitForSeconds(delay);
            count++;
            delay = Mathf.Max(0.05f, delay - 0.05f);
        }

        bojo.GetComponent<Animator>().SetBool("EndAttack", true);
    }

    IEnumerator Impact()
    {
        Debug.Log("안 올라가곤 못배길껄");

        
        attacking = true;
        int count = 0;

        laserScan.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        GameManager.instance.audioSource.volume = 0.1f;
        while (count < points[1].point.Count)
        {
            StartCoroutine(explosion.LineExplosion(points[1].point[count]));
            yield return new WaitForSeconds(0.01f);
            count++;
        }

        yield return new WaitForSeconds(2.5f);

        bojo.GetComponent<Animator>().SetBool("EndAttack", true);
        GameManager.instance.audioSource.volume = 1f;
    }

    IEnumerator Hit()
    {
        sprite.sprite = sprites[1];
        yield return new WaitForSeconds(0.01f);
        sprite.sprite = sprites[0];
    }
    
}
[System.Serializable]
public class Widen
{
    public List<Vector3> point;
}
