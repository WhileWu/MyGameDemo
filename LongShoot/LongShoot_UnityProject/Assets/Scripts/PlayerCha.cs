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
    
    public AudioClip hitSound;//���˵���ƵƬ��
    public AudioClip fireSound;//�����ӵ�����ƵƬ��
    AudioSource audioSource;   

    //�ƶ��ı߽緶Χ
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
        //�ж��Ƿ�������
        if (Time.time < lastFireTime + fireCD)
        {
            return;
        }
        //�����������
        PlaySound(fireSound);
        //�����ӵ�
        Vector3 pos = transform.position + new Vector3(0, 0.5f, 0);
        //Transform bullet = Instantiate(prefabBullet, pos, Quaternion.identity);
        Instantiate(prefabBullet, pos, Quaternion.identity);
        lastFireTime = Time.time;
    }

    //����ָ����ƵƬ��
    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //����Ч��
        hp--;
        //������������
        PlaySound(hitSound);
        if (hp <= 0)
        {
            //�������
             //Destroy(gameObject);
            gameObject.SetActive(false);
            Instantiate(prefabBoom, transform.position, Quaternion.identity);

            //���������������ĺ���
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
