using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public AudioClip startSound;   //开始按钮音效
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
        //播放音效
        audioSource.clip = startSound;
        audioSource.Play();
        //开启协程
        StartCoroutine(CoGameStart());
    }

    IEnumerator CoGameStart()
    {
        GameObject btn = GameObject.Find("开始按钮");
        //开始按钮闪烁
        for (int i = 0;i < 6;i++)
        {
            btn.SetActive(!btn.activeInHierarchy);
            yield return new WaitForSeconds(0.3f);
        }
        //切换场景
        //Debug.Log("切换场景");
        SceneManager.LoadScene("Game");
    }
}
