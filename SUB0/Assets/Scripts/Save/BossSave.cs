using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSave : MonoBehaviour,IResetable
{
    public Bojo bojo;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Hit()
    {
        GameManager gm = GameManager.instance;
        Vector3 cameraPos = transform.parent.position;
        gm.saveManager.currentData.lastScene = SceneManager.GetActiveScene().name;
        gm.saveManager.currentData.playerPos = gm.player.transform.position;
        gm.saveManager.currentData.cameraPos = new Vector3(cameraPos.x, cameraPos.y, -10);

        StartCoroutine(LightOn());
    }

    public void Init()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;
        Hit();
        bojo.GetComponentInChildren<Boss>().engage = true;
        bojo.GetComponent<Animator>().SetTrigger("Engage");
        GameManager.instance.BGM.loop = true;
        GameManager.instance.BGM.Play();
    }

    private IEnumerator LightOn()
    {
        spriteRenderer.sprite = sprites[1];
        yield return new WaitForSeconds(0.01f);
        spriteRenderer.sprite = sprites[0];
        gameObject.SetActive(false);

    }
}
