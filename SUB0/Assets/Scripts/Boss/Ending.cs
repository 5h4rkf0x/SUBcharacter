using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject boss; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !boss.activeSelf)
        {
            SceneManager.LoadScene("Ending");
        }
    }
}
