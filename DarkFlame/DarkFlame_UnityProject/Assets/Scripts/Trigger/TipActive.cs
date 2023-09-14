using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 篝火旁边小火堆的提示激活
/// </summary>
public class TipActive : MonoBehaviour
{
    PlayerCharacter player;
    public string tip;                 //指定的提示字符串
    UIManager ui;                    

    private void Start()
    {
        ui = UIManager.Instance;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }

    /// <summary>
    /// 监听玩家按下交互的按钮，处于移动状态,激活提示内容的UI
    /// </summary>
    private void Update()
    {
        if (ui.isTipActive)
        {
            ui.tipText.gameObject.SetActive(true);
            if (Input.GetButtonDown("React") && player.state == PlayerState.Move)
            {
                ui.tipContent.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 在小火堆的旁边，激活按键E的提示
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ui.isTipActive = true;
            Debug.Log("React," + tip);
            //把文本框中的\n全部转换为换行符
            tip = tip.Replace("\\n", "\n");
            Debug.Log(tip);
            ui.tipContent.GetComponentInChildren<Text>().text = tip;
            Debug.Log(ui.tipContent.GetComponentInChildren<Text>().text);
        }
    }
    /// <summary>
    /// 离开小火堆后，要关闭按键提示和提示内容
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (ui.isTipActive && other.gameObject.CompareTag("Player"))
        {
            ui.isTipActive = false;
            ui.tipText.gameObject.SetActive(false);
            ui.tipContent.gameObject.SetActive(false);
        }
    }
}
