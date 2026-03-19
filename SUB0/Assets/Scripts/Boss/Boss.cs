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

    private void Awake() // 시작 초기화
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

    private void Start() // 보스 패턴 종류 전체 추가
    {
        closePatterns.Add(BasicPattern);
        farAwayPatterns.Add(BasicWidenPattern);
        closePatterns.Add(FasterLaser);
        farAwayPatterns.Add(Impact);
    }

    void Update()
    {
        AnimationControl();
        if(!GameManager.instance.player.isDead) // 플레이어가 살아 있다면
        {
            if (!attacking && !isDead) // 보스가 공격중이고, 죽어있지 않다면
            {
                Debug.Log("패턴 시작");
                attacking = true;

                if (bojo.farAway) // 보스가 멀리 있다면 멀리 있는 패턴중 랜덤한 한개 실행
                {
                    int index = Random.Range(0, 2);
                    StartCoroutine(farAwayPatterns[index]());
                }
                else // 보스가 가까이 있다면 가까이 있는 패턴중 랜덤한 한개 실행
                {
                    int index = Random.Range(0, 2);
                    StartCoroutine(closePatterns[index]());
                }
            }
        }
        else // 플레이어 사망시 실행중인 모든 함수들을 중단하고 초기화
        {
            StopAllCoroutines();
            sprite.sprite = sprites[0];
            lightning.Stop();
            explosion.Stop();
            bojo.GetComponent<Animator>().SetBool("EndAttack", true);
        }

        
        
    }

    public void Init() // IResetable 필수 함수
    {
        GameManager.instance.audioSource.Stop(); // 브금 중단 --> 아래에 있는게 브금 중단이고 이거는 뭐지
        GameManager.instance.BGM.loop = false; // 브금 루프 중단
        GameManager.instance.BGM.Stop(); // 브금 중단
        StopAllCoroutines(); // 코루틴 중단
        bojo.GetComponent<Animator>().ResetTrigger("Engage"); // 보스 위치 초기화
        engage = false; // 여기 아래부터는 보스의 처음 속성을 초기화
        transform.position = initPos;
        transform.localScale = originalScale;
        health = maxHealth;
        attacking = true;
    }

    public void AnimationControl() // 보스가 멀리있을때와 가까이 있을때에 따른 에니메이션 변경
    {
        if(bojo.farAway) // 멀리있을 때
        {
            transform.localScale = farAwayScale;
            coll.enabled = false;
        }
        else // 가까이 있을 때
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

    IEnumerator BasicPattern() // 빵 빵 빵 빠빠빠빠빵
    {
        Debug.Log("3 쉬고 5");
        int count = 0;

        while(count < 3) // 3번 반복
        {
            StartCoroutine(lightning.YellowLightningRoutine(GameManager.instance.player.transform.position)); // 세로 일반 공격
            yield return new WaitForSeconds(0.5f);
            count++;
        }

        count = 0;
        yield return new WaitForSeconds(0.5f); // 총 1초 기다림

        while(count < 20) // 20번 반복
        {
            StartCoroutine(lightning.YellowLightningRoutine(GameManager.instance.player.transform.position)); // 세로 일반 공격
            yield return new WaitForSeconds(0.1f); // 0.1초 간격으로 반복
            count++;
        }

        bojo.GetComponent<Animator>().SetBool("EndAttack",true);
    }

    IEnumerator BasicWidenPattern() // 세로 특수 패턴 --> far 어쩌구 패턴
    {
        Debug.Log("개간지 광역기");
        GameManager.instance.audioSource.volume = 0.5f;
        attacking = true;
        int count = 0;
        while(count < points[0].point.Count) // 이 스크립트의 맨 아래에 있는 Widen 클래스에 있는 points 배열의 크기만큼 작동
        {
            StartCoroutine(lightning.BlueLightningRoutine(points[0].point[count])); // 세로 특수 공격
            yield return new WaitForSeconds(0.01f);
            count++;
        }

        yield return new WaitForSeconds(2.5f);

        bojo.GetComponent<Animator>().SetBool("EndAttack", true); // 공격 끝
        GameManager.instance.audioSource.volume = 1f;
    }

    IEnumerator FasterLaser()
    {
        Debug.Log("점점 빨라질 것이다");
        attacking = true;
        int count = 0;
        float delay = 1f; // 초기 공격 쿨타임 1s
        while(count < 20) // 20번 반복
        {
            StartCoroutine(lightning.YellowLightningRoutine(GameManager.instance.player.transform.position)); // 세로 일반 공격
            yield return new WaitForSeconds(delay);
            count++;
            delay = Mathf.Max(0.05f, delay - 0.05f); // 0.05s 간격씩 줄여나감
        }

        bojo.GetComponent<Animator>().SetBool("EndAttack", true); // 공격 끝
    }

    IEnumerator Impact() // 지형폭발
    {
        Debug.Log("안 올라가곤 못배길껄");

        
        attacking = true;
        int count = 0;

        laserScan.SetActive(true); // 간지 좋되는 찌이잉 하는거

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

    IEnumerator Hit() // 보스가 피격당했을 때 에니메이션 변화
    {
        sprite.sprite = sprites[1]; // 빨갛게 바꾸기
        yield return new WaitForSeconds(0.01f); // 0.01초동안 피격효과
        sprite.sprite = sprites[0]; // 다시 원상복구
    }
    
}
[System.Serializable]
public class Widen
{
    public List<Vector3> point;
}
