using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    Idle,
    Run,
    Skill_FireBall,
    Skill_FireRain,

}

public class Boss : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;

    public int maxHp = 20;
    public float speed = 8;
    public FireBall prefabFireBall;
    public FireRain prefabFireRain;

    int hp;

    BossState state;
    float lastChangeStateTime = 0;

    bool faceRight = true;

    int idleRnd;    //函数局部变量作为类变量，避免重复定义，消耗空间
    float coFireRnd;

    Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        
        lastChangeStateTime = Time.time;

        hp = maxHp;

        firePoint = transform.Find("FirePoint");
    }

    private void Update()
    {
        switch (state)
        {
            case BossState.Idle:
                {
                    if(Time.time - lastChangeStateTime > 3)
                    {
                        idleRnd = Random.Range(1, 4);
                        if(idleRnd == 1)
                        {
                            state = BossState.Run;
                        }
                        else if (idleRnd == 2)
                        {
                            state = BossState.Skill_FireBall;
                                StartCoroutine(CoFireBallState());
                        }
                        else if (idleRnd == 3)
                        {
                            state = BossState.Skill_FireRain;
                            StartCoroutine(CoFireRainState());

                        }
                        lastChangeStateTime = Time.time;
                    }
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                    break;
                }
            case BossState.Run:
                {
                    //判断冲刺到头，转身，切换会idle状态
                    if(faceRight && transform.position.x > 8 || !faceRight && transform.position.x < -8)
                    {
                        Flip();
                        state = BossState.Idle;
                        lastChangeStateTime = Time.time;
                        break;
                    }
                    Vector2 move = new Vector2(speed, rigid.velocity.y - 0.5f);
                    if (!faceRight)
                    {
                        move.x *= -1;
                    }
                    rigid.velocity = move;
                    break;
                }
            case BossState.Skill_FireBall:
                {
                    break;
                }
            case BossState.Skill_FireRain:
                {
                    break;
                }
        }
    }

    IEnumerator CoFireBallState()
    {
        for (int i = 0;i < 3;++i)
        {
            //吐火球
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(1.5f);
        }
        state = BossState.Idle;
        lastChangeStateTime = Time.time;
    }

    IEnumerator CoFireRainState()
    {
        for (int i = 0; i < 2; ++i)
        {
            //吐火球
            anim.SetTrigger("Attack");
            for (int j = 0;j < 6;++j)
            {
                coFireRnd = Random.Range(-10f, 10f);
                FireRain firerain = Instantiate(prefabFireRain, new Vector3(coFireRnd, 4, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(1.5f);
        }
        state = BossState.Idle;
        lastChangeStateTime = Time.time;
    }

    public void FireBall()
    {
        if(state != BossState.Skill_FireBall)
        {
            return;
        }
        FireBall ball = Instantiate(prefabFireBall, firePoint.position, Quaternion.identity);
        if (!faceRight)
        {
            ball.transform.right = Vector3.left;
        }

    }

    //Boss受伤逻辑
    public void GetHit(int damage)
    {
        hp -= damage;
        if (hp < 0) { hp = 0; }
        UIManager.Instance.SetBossHp(hp, maxHp);
        anim.SetTrigger("GetHit");
        if (hp == 0)
        {
            anim.SetTrigger("Die");
            Collider2D[] colliders = GetComponents<Collider2D>();
            foreach (var c in colliders)
            {
                c.enabled = false;
            }
            rigid.isKinematic = true;
        }
    }

    void Flip()
    {
        faceRight = !faceRight;

        Vector3 v1 = transform.localScale;
        v1.x *= -1;
        transform.localScale = v1;
    }
}
