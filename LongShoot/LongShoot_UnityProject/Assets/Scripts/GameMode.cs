using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public GameObject panel;

    public static GameMode Instance { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //开始前关闭面板
        panel.SetActive(false);
    }

    public void OnPlayerDie()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        panel.gameObject.SetActive(true);
        Text t = panel.transform.Find("EndText").GetComponent<Text>();
        t.text = "通关失败";
       // Debug.Log("OnPlayerDie");

    }

    public void OnRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void OnPlayerWin()
    {
        Time.timeScale = 0;
        panel.gameObject.SetActive(true);
        Text t = panel.transform.Find("EndText").GetComponent<Text>();
        t.text = "恭喜通关";
    }

    public void OnReturn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

}
