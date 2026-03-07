using UnityEngine;

public enum GunDirection
{
    UP,DOWN,CROUCH,STAND
}

public class Gun : MonoBehaviour
{
    public GunPosition gunPos;
    public GunDirection dir;
    public bool facingRight;


    private void Awake()
    {
    }

    private void Update()
    {
        
    }

    public void Fire()
    {
        BulletManager bm = GameManager.instance.bulletManager;
        Rigidbody2D p = GameManager.instance.player.rigid;

        float gravityScale = p.gravityScale * (1 / Mathf.Abs(p.gravityScale));

        switch(dir)
        {
            case GunDirection.UP:
                if (facingRight)
                {
                    transform.localPosition = gunPos.lookUp_Right;
                    bm.GetBullet(transform.position, Vector2.up * gravityScale);
                }
                else
                {
                    transform.localPosition = gunPos.lookUp_Left;
                    bm.GetBullet(transform.position, Vector2.up * gravityScale);
                }
                break;
            case GunDirection.DOWN:
                if (facingRight)
                {
                    transform.localPosition = gunPos.shootDown_Right;
                    bm.GetBullet(transform.position, Vector2.down * gravityScale);
                }
                else
                {
                    transform.localPosition = gunPos.shootDown_Left;
                    bm.GetBullet(transform.position, Vector2.down * gravityScale);
                }
                break;
            case GunDirection.STAND:
                if (facingRight)
                {
                    transform.localPosition = gunPos.idle_Right;
                    bm.GetBullet(transform.position, Vector2.right);
                }
                else
                {
                    transform.localPosition = gunPos.idle_Left;
                    bm.GetBullet(transform.position, Vector2.left);
                }
                break;
            case GunDirection.CROUCH:
                if (facingRight)
                {
                    transform.localPosition = gunPos.crouch_Right;
                    bm.GetBullet(transform.position, Vector2.right);
                }
                else
                {
                    transform.localPosition = gunPos.crouch_Left;
                    bm.GetBullet(transform.position, Vector2.left);
                }
                break;
        }
    }
}
    



