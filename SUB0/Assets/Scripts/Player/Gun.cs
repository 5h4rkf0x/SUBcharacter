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

    public void Fire()
    {
        BulletManager bm = GameManager.instance.bulletManager;
        Rigidbody2D p = GameManager.instance.player.rigid;

        float gravityScale = p.gravityScale * (1 / Mathf.Abs(p.gravityScale));
        
        // 플레이어가 각각 어떤 행동, 어떤 방향을 총구로 가르키고 있는지에 따라 각각 총알의 발사 각도 설정
        switch(dir)
        {
            case GunDirection.UP:
                if (facingRight) // 플레이어가 우측을 보고 있는가?
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
            case GunDirection.DOWN: // 공중에 뜬 상태에서 아래 방향키를 누른 상태
                if (facingRight) // 플레이어가 우측을 보고 있는가?
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
                if (facingRight) // 플레이어가 우측을 보고 있는가?
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
                if (facingRight) // 플레이어가 우측을 보고 있는가?
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
    



