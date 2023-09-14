using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敌人抬头显示或者说是血条显示
/// </summary>
public class EnemyHUD : MonoBehaviour
{
    public EnemyCharacter enemy;
    public Image fillImage;
    public Slider healthSlider;
    RectTransform rectTran;
    public float height = 2.5f;

    void Start()
    {
        enemy = GetComponentInParent<EnemyCharacter>();
        healthSlider = GetComponentInChildren<Slider>();
        rectTran = healthSlider.GetComponent<RectTransform>();
        fillImage = gameObject.transform.Find("Sld_Health/Fill Area/Fill").GetComponent<Image>();
        fillImage.color = Color.red;
    }

    /// <summary>
    /// 根据敌人死亡的状态显示血条
    /// 实时获取敌人的位置加上相应高度再转换成屏幕坐标，更新敌人的血条数值
    /// </summary>
    void Update()
    {
        var headPos = new Vector3(enemy.gameObject.transform.position.x, enemy.gameObject.transform.position.y + height, enemy.gameObject.transform.position.z);
        Vector2 pos = Camera.main.WorldToScreenPoint(headPos);
        rectTran.position = pos;

        if (enemy.state != EnemyState.Die)
        {
            healthSlider.gameObject.SetActive(true);
        }
        else
        {
            healthSlider.gameObject.SetActive(false);
        }

        healthSlider.value = enemy.health;
    }
}
