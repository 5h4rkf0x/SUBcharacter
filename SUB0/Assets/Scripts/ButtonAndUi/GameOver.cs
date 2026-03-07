using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Image gameover;

    private void Awake()
    {
        gameover = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        if (GameManager.instance.player.isDead)
        {
            gameover.enabled = true;
        }
        else
            gameover.enabled = false;

    }
}
