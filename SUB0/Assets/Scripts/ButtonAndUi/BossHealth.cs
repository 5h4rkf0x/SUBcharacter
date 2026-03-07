using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Boss boss;
    public Slider healthBar;
    private void Awake()
    {
        healthBar = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        boss = FindAnyObjectByType<Boss>();
    }

    public void LateUpdate()
    {
        if(boss.engage && !boss.isDead)
        {
            healthBar.gameObject.SetActive(true);
            healthBar.value = (float)boss.health / (float)boss.maxHealth;
        }
        else
        {
            healthBar.gameObject.SetActive(false);
        }
    }
}
