using UnityEngine;

public class GravityController : MonoBehaviour
{
    // zone에 들어오면 중력 반전되는 방식

    private bool isNormalGravity = true;
    private Transform playerTransform;
    private Rigidbody2D playerRb;
    private SpriteRenderer playerSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerTransform == null)
            {
                playerTransform = collision.transform;
                playerRb = collision.GetComponent<Rigidbody2D>();
                playerSprite = collision.GetComponent<SpriteRenderer>();
            }

            GravityToggle();
        }

    }

    private void GravityToggle()
    {
        isNormalGravity = !isNormalGravity;

        playerRb.gravityScale *= -1;

        playerTransform.localScale = new Vector3(playerTransform.localScale.x, -playerTransform.localScale.y, playerTransform.localScale.z);
    }

}
