using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public AudioClip startSound;   //��ʼ��ť��Ч
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButton()
    {
        //������Ч
        audioSource.clip = startSound;
        audioSource.Play();
        //����Э��
        StartCoroutine(CoGameStart());
    }

    IEnumerator CoGameStart()
    {
        GameObject btn = GameObject.Find("��ʼ��ť");
        //��ʼ��ť��˸
        for (int i = 0;i < 6;i++)
        {
            btn.SetActive(!btn.activeInHierarchy);
            yield return new WaitForSeconds(0.3f);
        }
        //�л�����
        //Debug.Log("�л�����");
        SceneManager.LoadScene("Game");
    }
}
