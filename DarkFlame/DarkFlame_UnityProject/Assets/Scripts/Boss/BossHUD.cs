using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 处理Boss的抬头显示或者说是血条
/// </summary>
public class BossHUD : MonoBehaviour
{
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    BossCharacter boss;

	void Awake ()
    {
        healthSlider = gameObject.transform.Find("Sld_BossHealth").GetComponent<Slider>();
        boss = GetComponentInParent<BossCharacter>();
        healthSlider.gameObject.SetActive(false);
    }
	
	void Update ()
    {
        healthSlider.value = boss.health;
	}
}
