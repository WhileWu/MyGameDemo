using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public GameObject panel;
    public static GameMode Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        panel.SetActive(false);
        
        //Debug.Log("Start被调用");
    }

    public void OnPlayerDie()
    {
        //Time.timeScale = 0;
        panel.SetActive(true);
        //Debug.Log(t);
        Text t = panel.transform.Find("GameOver").GetComponent<Text>();
        //panel.gameObject.SetActive(true);
        t.text = "菜";
    }

    public void OnRestart()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnPlayerWin()
    {
        //Time.timeScale = 0;
        //panel.gameObject.SetActive(true);
        panel.SetActive(true);
        Text t = panel.transform.Find("GameOver").GetComponent<Text>();
        t.text = "你通关了！！！";
    }
}
