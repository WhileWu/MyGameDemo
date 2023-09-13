using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 篝火的激活
/// </summary>
public class FireActive : MonoBehaviour
{
    PlayerCharacter player;

    bool near;          //玩家是否在篝火附近

	void Start ()
    {
        near = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }

    /// <summary>
    /// 监听玩家按下交互按钮并且处于篝火附近和移动状态,执行坐在篝火的逻辑
    /// </summary>
    private void Update()
    {
        if (near && Input.GetButtonDown("React") && player.state == PlayerState.Move)
        {
            AudioManager.Instance.AudioPlayOnce("Fire" ,transform.position);
            player.Sit(transform, true);
            UIManager.Instance.tipFireText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 进入篝火触发器，玩家处于篝火附近，激活相关提示文本
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("enter");
        if (other.gameObject.CompareTag("Player"))
        {
            near = true;
            if (player.state != PlayerState.Sit)
            {
                UIManager.Instance.tipFireText.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 离开篝火触发器，撤销一些状态，如玩家不在篝火附近，关闭文本框
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit");
        if (other.gameObject.CompareTag("Player"))
        {
            near = false;
            UIManager.Instance.tipFireText.gameObject.SetActive(false);
        }
    }

}
