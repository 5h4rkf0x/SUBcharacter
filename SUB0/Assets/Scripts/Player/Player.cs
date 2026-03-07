using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

enum PlayerState
{
    Ground, Water
}

public class Player : MonoBehaviour
{
    public Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator animator;
    CapsuleCollider2D coll;
    public AudioClip[] jump;
    public AudioClip death;
    Coroutine shot;

    public Gun gun { get; private set; }
    

    Vector2 originalSize;
    Vector2 originalOffset;
    Vector2 crouchSize;
    Vector2 crouchOffset;
    public Vector2 moveVec;
    

    public float moveSpeed;
    float coyoteTimeCounter = 0;
    float coyoteTime = 0.15f;
    float distance = 0.05f;
    float waterWeight = 0.55f;
    [SerializeField] private float[] jumpSpeeds;

    [SerializeField]PlayerState state;

    [SerializeField] bool isGround = false;
    [SerializeField] bool canAirJump = false;
    public bool isDead = false;

    void Awake()
    {

        gameObject.SetActive(true);
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gun = GetComponentInChildren<Gun>();
        coll = GetComponent<CapsuleCollider2D>();

        jumpSpeeds = new float[] { 10.0f, 7.0f };

        originalSize = coll.size;
        originalOffset = coll.offset;
        crouchSize = new Vector2(originalSize.x, originalSize.y - 0.10f);
        crouchOffset = new Vector2(originalOffset.x, originalOffset.y - 0.10f);
    }

    void Start()
    {
        Init(GameManager.instance.saveManager.currentData.playerPos);
    }

    void Update()
    {
        //Debug.Log($"수직 속도 : {rigid.linearVelocityY}");
        
    }

    private void FixedUpdate()
    {
        Move();
        TerrainCollision();
        AnimationControl();
        GunPosSet();
        if (isGround)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }
    public void Init(Vector3 initPos)
    {
        GameManager.instance.gameoverBGM.Stop();
        GameManager.instance.audioSource.volume = 1;
        GameManager.instance.BGM.volume = 0.5f;
        
        transform.position = initPos;
        rigid.linearVelocityY = 0f;
        isDead = false;
        rigid.simulated = true;
        coll.enabled = true;
        animator.Play("Idle",0,0f);
        gameObject.SetActive(true);
       
    }

    void AnimationControl()
    {
        if (isDead) return;

        if(moveVec.x != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }


        if ((rigid.gravityScale * (1 / Mathf.Abs(rigid.gravityScale))) > 0)
        {
            if (rigid.linearVelocityY < -0.1)
            {
                animator.SetBool("Falling", true);
            } 
        }
        else if((rigid.gravityScale * (1 / Mathf.Abs(rigid.gravityScale))) < 0)
        {
            if (rigid.linearVelocityY > 0.1)
            {
                animator.SetBool("Falling", true);
            }
        }
        
        if(!isGround)
        {
            animator.SetBool("Jumping", true);
        }
            
    }

    void Control()
    {

    }

    void Move()
    {
        if (isDead)
            return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Crouch"))
        {
            rigid.linearVelocityX = 0;
            return;
        }
        
            
        rigid.linearVelocityX = moveVec.x * moveSpeed;
    }

    void TerrainCollision()
    {
        LayerMask mask = LayerMask.GetMask("Terrain");

        float originY = coll.bounds.center.y;

        if((rigid.gravityScale * (1 / Mathf.Abs(rigid.gravityScale)))>0)
        {
            originY -= 0.4976513f;
        }
        else if((rigid.gravityScale * (1 / Mathf.Abs(rigid.gravityScale))) < 0)
        {
            originY += 0.4976513f;
        }

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(coll.bounds.center.x, originY), Vector2.down * (rigid.gravityScale * (1 / Mathf.Abs(rigid.gravityScale))), distance,mask);

        if(hit.collider != null)
        {
            isGround = true;
            canAirJump = true;
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", false);
            animator.ResetTrigger("1stJump");
            animator.ResetTrigger("2ndJump");
        }
    }

    void GunPosSet()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Crouch"))
        {
            gun.dir = GunDirection.CROUCH;
        }
        else if(stateInfo.IsName("ShootDown"))
        {
            gun.dir = GunDirection.DOWN;
        }
        else if(stateInfo.IsName("LookUp") || stateInfo.IsName("LookUp_Run"))
        {
            gun.dir = GunDirection.UP;
        }
        else
            gun.dir = GunDirection.STAND;
    }

    public void Death()
    {
        GameManager.instance.audioSource.PlayOneShot(death);
        GameManager.instance.BGM.Stop();
        GameManager.instance.BGM.loop = false;
        GameManager.instance.gameoverBGM.Play();
        isDead = true;
        rigid.simulated = false;
        coll.enabled = false;
        animator.SetTrigger("Death");

    }

    #region EventFunc
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("충돌감지");

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Death();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Terrain"))
            return;

        isGround = false;
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Death();

        }
        else if (collision.CompareTag("Water"))
        {
            state = PlayerState.Water;
            rigid.linearVelocityY *= 0.3f;
            canAirJump = true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Water"))
            return;
        state = PlayerState.Water;

        rigid.gravityScale = 0.75f * (rigid.gravityScale * (1 / Mathf.Abs(rigid.gravityScale)));

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Water"))
            return;
        state = PlayerState.Ground;

        rigid.gravityScale = 2.5f * (rigid.gravityScale * (1 / Mathf.Abs(rigid.gravityScale)));
    }

    #region UNITY_EVENTS

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if ( input.x < 0)
        {
            sprite.flipX = true;
            gun.facingRight = false;
        }
        else if(input.x > 0)
        {
            sprite.flipX = false;
            gun.facingRight = true;
        }
        moveVec = input;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!(isGround || canAirJump || isDead))
            return;

        if(context.performed)
        {

            if(isGround && coyoteTimeCounter>0)
            {
                Debug.Log("1단 점프");
                isGround = false;
                animator.SetTrigger("1stJump");
                GameManager.instance.audioSource.PlayOneShot(jump[0]);
                if(state != PlayerState.Water)
                {
                    rigid.linearVelocityY = jumpSpeeds[0] * (rigid.gravityScale * (1/Mathf.Abs(rigid.gravityScale)));
                }
                else
                {
                    rigid.linearVelocityY = jumpSpeeds[0] * (rigid.gravityScale * (1 / Mathf.Abs(rigid.gravityScale))) * waterWeight;
                }
                
            }
            else if(canAirJump) 
            {
                Debug.Log("2단 점프");
                GameManager.instance.audioSource.PlayOneShot(jump[1]);
                if (state != PlayerState.Water)
                {
                    animator.SetTrigger("2ndJump");
                    canAirJump = false;
                    rigid.linearVelocityY = jumpSpeeds[1] * (rigid.gravityScale * (1 / Mathf.Abs(rigid.gravityScale)));
                }
                else
                {
                    animator.SetTrigger("2ndJump");
                    rigid.linearVelocityY = jumpSpeeds[1] * (rigid.gravityScale * (1 / Mathf.Abs(rigid.gravityScale))) * waterWeight;
                }
                
                
            }
            
        }
        if(context.canceled)
        {
            if(rigid.linearVelocityY > 0)
            {
                rigid.linearVelocityY *= 0.5f;
            }
            
        }

    }

    public void OnLookUp(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            animator.SetBool("LookUp", true);
        }
        else if(context.canceled)
        {
            animator.SetBool("LookUp", false);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            coll.size = crouchSize;
            coll.offset = crouchOffset;
            animator.SetBool("Crouching", true);
        }
        else if(context.canceled)
        {
            coll.size = originalSize;
            coll.offset = originalOffset;
            animator.SetBool("Crouching", false);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            shot = StartCoroutine(Shooting());
        }
        else if(context.canceled)
        {
            StopCoroutine(shot);
            shot = null;
        }

        
    }
    #endregion
    #endregion

    IEnumerator Shooting()
    {
        while(true)
        {
            if(!isDead)
            {
                gun.Fire();
            }

            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
[System.Serializable]
public struct GunPosition
{
    public Vector3 idle_Right;
    public Vector3 idle_Left;
    public Vector3 crouch_Right;
    public Vector3 crouch_Left;
    public Vector3 shootDown_Right;
    public Vector3 shootDown_Left;
    public Vector3 lookUp_Right;
    public Vector3 lookUp_Left;

}
