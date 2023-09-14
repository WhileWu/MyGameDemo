using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor
{
    Red,
    Green,

}

public class PlayerCha : MonoBehaviour
{
    //各种组件
    Rigidbody rigid;
    Animator anim;
    Renderer render;

    public float moveSpeed = 8;
    public float jumpSpeed = 3.8f;

    public int jumpCount = 0;
    public float lowestPos;
    bool isGround;

    PlayerColor color = PlayerColor.Red;

    public Transform prefabDieParticleRed;
    public Transform prefabDieParticleGreen;

    AudioSource jumpSound;
    AudioSource changeColorSound;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        render = GetComponentInChildren<Renderer>();
        //Debug.Log("render" + render);
        changeColorSound = GetComponents<AudioSource>()[0];
        jumpSound = GetComponents<AudioSource>()[1];
    }


    public void Move(bool isRun, bool jump, bool changeColor)
    {
        Vector3 vel = rigid.velocity;
        if(isRun) { vel.z = moveSpeed; }
        else      { vel.z = 0; }
        //移动方向
        //rigid.AddForce(new Vector3(0, 0, moveSpeed * Time.deltaTime));
        anim.SetBool("IsRun", isRun);

        if (jump && jumpCount < 2)
        {
            vel.y = jumpSpeed;
            jumpSound.Play();
            jumpCount++;
        }

        rigid.velocity = vel;
        anim.SetBool("IsGround", isGround);
        isGround = false;
        //rigid.velocity = new Vector3(0, 0, speed);
        if (changeColor)
        {
            changeColorSound.Play();
            ChangeColor();
        }

        if(transform.position.y < lowestPos)
        {
            PlayerDie();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if(tag == "Win")
        {
            PlayerWin();
        }
        if(tag == "Red" || tag == "Green")
        {
            jumpCount = 0;
            isGround = true;
        }
    }

    void ChangeColor()
    {
        if (color == PlayerColor.Red) { color = PlayerColor.Green; }
        else                          { color = PlayerColor.Red; }
        
        if (color == PlayerColor.Red) { render.material.color = Color.red; }
        else                          { render.material.color = Color.green; }
        
        anim.SetTrigger("Change");
    }

    private void OnCollisionStay(Collision collision)
    {
        string tag = collision.gameObject.tag;
        //Debug.Log(tag);
        //Debug.Log(color);
        if (tag == "Red" || tag == "Green")
        {
            isGround = true;
        }

        if( ( color == PlayerColor.Green && tag == "Red" ) || (color == PlayerColor.Red && tag == "Green") )
        {
            PlayerDie();
        }
    }

     void PlayerDie()
    {
        if (color == PlayerColor.Red)
        {
            Instantiate(prefabDieParticleRed, transform.position, Quaternion.identity);

        }
        else
        {
            Instantiate(prefabDieParticleGreen, transform.position, Quaternion.identity);
        }

        Invoke("DelayPlayerDie", 1);
        gameObject.SetActive(false);

    }


    void DelayPlayerDie()
    {
        //调用GameMode的OnPlayerDie函数
        //GameObject.Find("GameMode");
        GameMode.Instance.OnPlayerDie();
        //StartCoroutine(Delay());
    }

    //IEnumerator Delay()
    //{
    //    //yield return new WaitForSeconds(2.0f);
    //    //Debug.Log("here");
    //}

    void PlayerWin()
    {
        gameObject.SetActive(false);
        Invoke("DelayPlayerWin", 1);
    }

    void DelayPlayerWin()
    {
        GameMode.Instance.OnPlayerWin();
    }
}
