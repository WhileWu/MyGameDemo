using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //多个怪物的预制体
    public List<Transform> commonMonsters;
    public List<Transform> guardMonsters;
    public Transform bossMonsters;

    public float minTime;
    public float maxTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateCommon());
        StartCoroutine(CreateGuard());
        StartCoroutine(CreateBoss());
    }

    IEnumerator CreateCommon()
    {
        while (true)
        {
            //等一段时间
            float t = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(t);
            //创建怪物
            int rnd = Random.Range(0, commonMonsters.Count);
            Vector3 pos = new Vector3(Random.Range(-2.0f, 2.0f), transform.position.y, 0);
            Instantiate(commonMonsters[rnd], pos, Quaternion.identity);
        }
    }

    IEnumerator CreateGuard()
    {
        yield return new WaitForSeconds(10 * maxTime);
        foreach(var elem in guardMonsters)
        {
            Instantiate(elem, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator CreateBoss()
    {
        yield return new WaitForSeconds(10 * maxTime + 2.0f);
        Transform boss = Instantiate(bossMonsters, transform.position, Quaternion.identity);
    }
}
