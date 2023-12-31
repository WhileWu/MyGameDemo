﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家进入boss的门后的触发器
/// </summary>
public class CloseTrigger : MonoBehaviour
{
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameMode.Instance.EnterBossArea();
        }
        // 隐藏自身，保证只关闭一次boss的大门
        gameObject.SetActive(false);
    }
}
