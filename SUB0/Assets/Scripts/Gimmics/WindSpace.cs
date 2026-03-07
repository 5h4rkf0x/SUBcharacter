using UnityEngine;

public class WindSpace : MonoBehaviour
{
    [SerializeField] private Vector2 windDirection = Vector2.left; // 바람 방향 조정
    [SerializeField] private float windForce = 130f; // 바람 세기 (필요한 만큼 조절)

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rbPlayer = collision.attachedRigidbody;
            if (rbPlayer != null)
            {
                rbPlayer.AddForce(windDirection.normalized * windForce, ForceMode2D.Force);
            }
        }
    }
}
