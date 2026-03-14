using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject boss; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !boss.activeSelf) // 보스 클리어 이후 벽에 부딧히면 엔딩크래딧
        {
            SceneManager.LoadScene("Ending"); // Ending 씬으로 변환
        }
    }
}
