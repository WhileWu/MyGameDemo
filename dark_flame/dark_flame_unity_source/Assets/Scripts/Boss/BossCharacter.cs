using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharacter : MonoBehaviour
{
    Animator animator;
    Rigidbody rigid;
    //AnimatorStateInfo stateInfo;
    PlayerCharacter player;

    public EnemyState state;
    
    bool isSecondPart = false;

    public int normalDamage = 20; //普通攻击伤害
    public int specialDamage = 30; //特殊攻击伤害
    public int health = 300; //血量
    public int moveSpeed = 2; //移动速度
    public float stopDistance = 3;

    public ParticleSystem specialParticle01;
    public ParticleSystem specialParticle02;
    public ParticleSystem specialParticle03;
    public ParticleSystem deathParticle;
    public ParticleSystem bloodParticle;

    GameObject[] fireParticle;

    Vector3 move;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        rigid = GetComponent<Rigidbody>();

        fireParticle = GameObject.FindGameObjectsWithTag("BossFire");
        foreach(var pair in fireParticle)
        {
            pair.SetActive(false);
        }

        specialParticle01.gameObject.SetActive(false);
        specialParticle02.gameObject.SetActive(false);
    }
	
	void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 to = playerPos - transform.position;
        var dist = to.magnitude; //敌人距玩家的距离

        switch (state)
        {
            case EnemyState.Sleeping:
                {
                }
                break;
            case EnemyState.Move:
                {
                    if (player.state == PlayerState.Die)
                    {
                        move = Vector3.zero;
                        animator.SetBool("Move", false);
                        break;
                    }

                    Rotate(to);
                    move = transform.forward;
                    animator.SetBool("Move", true);

                    float angle = Vector3.Angle(to, transform.forward);
                    if (dist <= stopDistance && angle < 10)
                    {
                        state = EnemyState.Attack;
                        animator.SetBool("Move", false);
                        if (!isSecondPart)
                        {
                            Attack();
                        }
                        else
                        {
                            SecondPartAttack();
                        }
                    }
                }
                break;
            case EnemyState.Attack:
                {
                }
                break;
            case EnemyState.GetHit:
                {
                }
                break;
            case EnemyState.Die:
                {
                }
                break;
        }
    }

    //更新移动逻辑
    private void FixedUpdate()
    {
        if (state == EnemyState.Move)
        {
            rigid.MovePosition(rigid.position + move * moveSpeed * Time.fixedDeltaTime);
        }
    }

    // Boss转向dir
    internal void Rotate(Vector3 dir)
    {
        Quaternion faceToQuat = Quaternion.LookRotation(dir); //角色面朝目标方向的四元数
        Quaternion slerp = Quaternion.Slerp(transform.rotation, faceToQuat, 0.2f);
        transform.rotation = slerp;
    }

    private void Attack()
    {
        //int[] actions = { 1, 2, 3 };
        int[] actions = { 1, 2, 4 };
        int r = Random.Range(0, actions.Length);
        animator.SetInteger("Combo", actions[r]);
    }

    private void SecondPartAttack()
    {
        //int[] actions = { 4, 5, 6, 7 };
        int[] actions = { 3, 5, 6, 7 };
        int r = Random.Range(0, actions.Length);
        animator.SetInteger("Combo", actions[r]);
    }

    // Boss普通攻击
    public void NormalAttack()
    {
        var nearby = Physics.OverlapSphere(transform.position, 3, LayerMask.GetMask("Player"));
        
        foreach (var pair in nearby)
        {
            var target = pair.gameObject.transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, target);

            if (angle <= 50)
            {
                player.GetHit(normalDamage);
                break;
            }
        }
    }

    // Boss特殊攻击
    public void SpecialAttack(int type)
    {
        // 获取伤害判定的原点
        Vector3 damagePoint = Vector3.zero;
        if (type == 1)
        {
            var t = gameObject.transform.Find("SpecialAttack01"); //获取武器上的判定点
            damagePoint = t.position;
            //播放特效
            specialParticle01.gameObject.SetActive(true);
            specialParticle01.Play();
        }
        else if (type == 2)
        {
            var t = gameObject.transform.Find("Armature/Hip/Bone.002/Chest/Shoulder.l/Upperarm.l/LowerArm.l/Hand.l/SpecialAttack02"); //获取武器上的判定点
            damagePoint = t.position;
            //播放特效
            specialParticle02.gameObject.SetActive(true);
            specialParticle02.Play();
        }

        // 球形射线检测，产生伤害
        var nearby = Physics.OverlapSphere(damagePoint, 5, LayerMask.GetMask("Player"));

        Debug.Log("SpecialAttack");
        foreach (var pair in nearby)
        {
            if (pair.gameObject.CompareTag("Player"))
            {
                if (type == 1)
                {
                    player.GetHit(specialDamage);
                    break;
                }
                else if (type == 2)
                {
                    var target = pair.gameObject.transform.position - damagePoint;
                    var angle = Vector3.Angle(transform.forward, target);

                    if (angle <= 120 && target.magnitude <= 3f)
                    {
                        player.GetHit(specialDamage);
                        break;
                    }
                }
            }
        }
    }

    public void GetHit(int damage)
    {
        if (state == EnemyState.Die)
        {
            return;
        }
        health -= damage;

        this.gameObject.GetComponent<EnemyAudio>().GetHitAudio();
        bloodParticle.Play();

        if(health <= 150 && !isSecondPart)
        {
            // 播放Boss_GetHit动画，利用动画帧事件分步开启第二形态
            animator.Play("Boss_GetHit", 0,0);
            isSecondPart = true;
            Debug.Log("BOSS开启第二状态:" + isSecondPart);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        state = EnemyState.Die;
        deathParticle.Play();
        GameMode.Instance.BossDie();

        transform.GetComponent<Collider>().isTrigger = true;
        transform.GetComponent<Rigidbody>().isKinematic = true;

        foreach (var pair in fireParticle)
        {
            pair.SetActive(false);
        }
    }

    //public void AttackFinish(int type)
    //{
    //    Debug.Log("AttackFinsh");
    //    if (type == -1 || type == 3 || type == 5 || type == 101 || type == 102)
    //    {
    //        Debug.Log("AttackFinsh if");
    //        animator.SetInteger("Combo", 0);
    //        state = EnemyState.Move;
    //    }
    //}

    public void AttackFinish()
    {
        animator.SetInteger("Combo", 0);
        state = EnemyState.Move;
    }

    /// <summary>
    /// 隐藏一些特效(动画帧事件)
    /// </summary>
    public void HideParticle()
    {
        specialParticle01.gameObject.SetActive(false);
        specialParticle02.gameObject.SetActive(false);
    }

    /// <summary>
    /// 激活boss的火焰特效(动画帧事件)
    /// </summary>
    public void ActiveFire()
    {
        Debug.Log("activeFire");
        foreach (var pair in fireParticle)
        {
            pair.SetActive(true);
        }
        specialParticle03.Play();
    }

    /// <summary>
    /// 跺脚时伤害（动画帧事件）
    /// </summary>
    public void AngerDamage()
    {
        var damageArea = gameObject.transform.Find("SpecialAttack03");
        var damageOverLarp = Physics.OverlapSphere(damageArea.position, 4);
        if (damageOverLarp.Length > 0)
        {
            foreach (var pair in damageOverLarp)
            {
                if (pair.gameObject.CompareTag("Player"))
                {
                    player.GetHit(specialDamage);
                }
            }
        }
    }
}
