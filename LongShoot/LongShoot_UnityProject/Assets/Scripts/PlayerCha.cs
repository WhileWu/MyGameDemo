using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCha : MonoBehaviour
{
    public int hp = 10;
    public Transform prefabBullet;
    public float moveSpeed = 3;
    public float fireCD = 0.2f;
    float lastFireTime;

    public Transform prefabBoom;
    
    public AudioClip hitSound;//受伤的音频片段
    public AudioClip fireSound;//发射子弹的音频片段
    AudioSource audioSource;   

    //移动的边界范围
    public Border moveBorder;


    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void Move(Vector3 input)
    {
        Vector3 pos = transform.position + input * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, moveBorder.left, moveBorder.right);
        pos.y = Mathf.Clamp(pos.y, moveBorder.down, moveBorder.up);
        transform.position = pos;
    }

    public void Fire()
    {
        //判断是否可以射击
        if (Time.time < lastFireTime + fireCD)
        {
            return;
        }
        //播放射击声音
        PlaySound(fireSound);
        //生成子弹
        Vector3 pos = transform.position + new Vector3(0, 0.5f, 0);
        //Transform bullet = Instantiate(prefabBullet, pos, Quaternion.identity);
        Instantiate(prefabBullet, pos, Quaternion.identity);
        lastFireTime = Time.time;
    }

    //播放指定音频片段
    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //受伤效果
        hp--;
        //播放受伤声音
        PlaySound(hitSound);
        if (hp <= 0)
        {
            //玩家死亡
             //Destroy(gameObject);
            gameObject.SetActive(false);
            Instantiate(prefabBoom, transform.position, Quaternion.identity);

            //调用玩家死亡处理的函数
            //PlayerDie();
            //StartCoroutine(TestPlayDie());
            Invoke("PlayerDie", 1);
        }
    }

    //IEnumerator TestPlayDie()
    //{
    //    Debug.Log("DelayPlayerDie1");
    //    yield return new WaitForSeconds(1.2f);
    //    Debug.Log("DelayPlayerDie2");
    //    GameMode.Instance.OnPlayerDie();
    //}

    void PlayerDie()
    {
        //Debug.Log("DelayPlayerDie1");
        //Debug.Log("DelayPlayerDie2");
        GameMode.Instance.OnPlayerDie();
    }
}
