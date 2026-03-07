using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeakPlatform : MonoBehaviour,IResetable
{
    private bool isCracking = false;
    private Collider2D col;
    private TilemapRenderer tile;
    private Animator animator;

    private void Awake()
    {
        col = GetComponent<Collider2D>();

        tile = GetComponent<TilemapRenderer>(); // 타일맵 찾기. 스프라이트 렌더러로 변경하게 된다면 수정할 듯
        if (tile == null)
        {
            Debug.Log("TilemapRenderer가 없습니다");
        }

        animator = GetComponent<Animator>();
    }

    public void Init()
    {
        StopAllCoroutines();
        animator.SetBool("Shake", false);
        animator.Play("Idle");
        isCracking = false;
        col.enabled = true;
        tile.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCracking && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("부실한 발판 충돌");

            StartCoroutine(Crack());
        }
    }

    IEnumerator Crack()
    {
        isCracking = true;

        if (animator != null) { animator.SetBool("Shake", true); }

        yield return new WaitForSeconds(2.0f);

        col.enabled = false;
        tile.enabled = false;

        if (animator != null) { animator.SetBool("Shake", false); }
            
        yield return new WaitForSeconds(4.0f); // 다시 생성

        col.enabled = true;
        tile.enabled = true;

        isCracking = false;
    }
}
