using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeHit : MonoBehaviour
{
    SpriteRenderer[] renders;

    public float redTime = 0.1f;// 保持红色的时间，可调
    float changeColorTime;//
    public int hp;
    public Transform prefabBoom;


    // Start is called before the first frame update
    void Start()
    {
        renders = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > changeColorTime)
        {
            SetColor(Color.white);
        }
    }

    void SetColor(Color c)
    {
        if(renders[0].color == c)
        {
            return;
        }
        foreach (var r in renders)
        {
            r.color = c;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetColor(Color.red);
        changeColorTime = Time.time + redTime;
        hp -= 1;
        if(hp <= 0)
        {
            //死亡
            Destroy(gameObject);

            //播放一个爆炸特效
            Instantiate(prefabBoom, transform.position, Quaternion.identity);
            //判断是否为Boss的死亡
            if (gameObject.tag == "Boss")
            {
                GameMode.Instance.OnPlayerWin();
            }
        }
    }

    //void PlayerWin()
    //{
    //    gameObject.SetActive(false);
    //    //StartCoroutine(DelayPlayerWin());
    //    Invoke("DelayPlayerWin", 1);
    //    //Debug.Log("PlayerWin");
    //}

    //void DelayPlayerWin()
    //{
    //    GameMode.Instance.OnPlayerWin();
    //}
}
