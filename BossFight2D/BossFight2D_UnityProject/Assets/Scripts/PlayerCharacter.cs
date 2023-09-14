using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody2D rigid;
    Animator anim;

    Transform checkGround;

    public float speed = 3;
    public float jumpSpeed = 8;
    public int maxHp = 5;
    int hp;


    bool isJump = false;
    bool isGround = false;
    bool faceLeft = true;

    //失去控制的时间(物理帧)
    float outCtrlTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        checkGround = transform.Find("CheckGround");
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.isJump == true)
        {
            //Debug.Log("按下空格");
            isJump = true;
        }

        //更新动画状态
        anim.SetBool("IsGround", isGround);
        anim.SetFloat("Speed", Mathf.Abs(playerController.h));
        if (playerController.attack)
        {
            anim.SetTrigger("Attack");
        }
    }

    //去动画状态机中检测，是否正在攻击
    public bool IsAttacking()
    {
        AnimatorStateInfo asi = anim.GetCurrentAnimatorStateInfo(0);
        return asi.IsName("Attack1") || asi.IsName("Attack2") || asi.IsName("Attack3");
    }

    private void FixedUpdate()
    {
        CheckGround();
        if (!IsAttacking())
        {
            Move(playerController.h);
        }
        isJump = false;
        --outCtrlTime;
    }

    void Move(float h)
    {
        if (outCtrlTime > 0)
        {
            return;
        }
        Flip(h);
        float vy = rigid.velocity.y;
        if (isJump == true && isGround == true)
        {
            anim.SetTrigger("Jump");
            vy = jumpSpeed;
        }
       rigid.velocity = new Vector2(h * speed, vy); 
    }

    void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(checkGround.position, 0.1f , ~LayerMask.GetMask("Player"));
    }

    void Flip(float h)
    {
        Vector3 scaleLeft = new Vector3(1, 1, 1);
        Vector3 scaleRight = new Vector3(-1, 1, 1);

        if(h > 0.1f)
        {
            faceLeft = false;
            transform.localScale = scaleRight;
        }else if(h < -0.1f)
        {
            faceLeft = true;
            transform.localScale = scaleLeft;
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.white;
            if (isGround)
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawSphere(checkGround.position, 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Boss") || collision.transform.CompareTag("BossHit") )
        {
            GetHit(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (( collision.transform.CompareTag("Boss") || collision.transform.CompareTag("BossHit") ) )
        {
            
            GetHit(1);
        }
    }

    public void GetHit(int damage)
    {
        Debug.Log("get hit" + damage);
        hp -= damage;
        if(hp < 0)
        {
            hp = 0;
        }
        UIManager.Instance.SetPlayerHp(hp, maxHp);
        //受伤动画
        anim.SetTrigger("GetHit");
        //受伤时，向反方向弹飞
        Vector2 v1 = new Vector2(4, 4);
        if (!faceLeft)
        {
            v1.x *= -1;
        }
        if(hp < 0) { hp = 0; }
        //rigid.AddForce(force);
        rigid.velocity = v1;

        outCtrlTime = 30;

    }
}
