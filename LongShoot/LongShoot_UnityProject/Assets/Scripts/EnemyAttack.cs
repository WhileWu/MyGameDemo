using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //子弹模型
    // Start is called before the first frame update
    public Transform prefabBullet;//指定子弹类型
    [SerializeField]
    public List<Transform> skills;//指定技能

    public float minTime;
    public float maxTime;

    void Start()
    {
        skills = new List<Transform>();
        StartCoroutine(Fire());
        if (skills.Count > 0) { StartCoroutine(useSkill()); }
    }


    IEnumerator Fire()
    {
        while (true)
        {
            float t = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(t);
            //生成子弹
            Vector3 pos = transform.position + new Vector3(0, -0.5f, 0);
            Instantiate(prefabBullet, pos, Quaternion.identity);
        }
    }

    IEnumerator useSkill()
    {
        yield return new WaitForSeconds(4.5f);
    }
}
