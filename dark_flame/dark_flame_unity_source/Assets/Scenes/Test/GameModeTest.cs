using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeTest : MonoBehaviour
{
    public static GameModeTest Instance { get; private set; }
    List<GameObject> allEnemies;
    public GameObject Enemies;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("存在Instance");
            Destroy(this);
            return;
        }
        Instance = this;
        allEnemies = new List<GameObject>();
    }

    private void Start()
    {
        // 保存所有敌人，并隐藏
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach (GameObject go in enemies)
        //{
        //    go.tag = "EnemyCopy";
        //    go.transform.position += new Vector3(10000, 0, 0);
        //    go.SetActive(false);
        //    allEnemies.Add(go);
        //}
        //CreateEnemiesByClone();
        CreateEnemiesByPool();
    }
    int cnt = 2;

    void CreateEnemiesByClone()
    {
        for (int i = 0;i < cnt;++i)
        {
            foreach (GameObject go in allEnemies)
            {
                GameObject enemy = Instantiate(go, go.transform.parent);
                enemy.transform.position -= new Vector3(10000, 0, 0);
                enemy.tag = "Enemy";
                enemy.SetActive(true);
            }
        }
    }

    void CreateEnemiesByPool()
    {
        for (int i = 0;i < cnt;++i)
        {
            GameObject bear = EnemyPool.Instance.GetObj(EnemyObj.Bear);
            GameObject skeletonNormal = EnemyPool.Instance.GetObj(EnemyObj.SkeletonNormal);
            bear.transform.position -= new Vector3(10000, 0, 0);
            skeletonNormal.transform.position -= new Vector3(10000, 0, 0);
            bear.transform.SetParent(Enemies.transform);
            skeletonNormal.transform.SetParent(Enemies.transform);
            skeletonNormal.SetActive(true);
            bear.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("StartPage");
    }

    private void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            Debug.Log("重新加载场景");
            //收回所有敌人
            for(int i = 0;i < Enemies.transform.childCount;++i)
            {
                GameObject go = Enemies.transform.GetChild(i).gameObject;
                EnemyPool.Instance.DestoryObj(go, go.GetComponent<EnemyData>().enemyObj);
            }
            SceneManager.LoadScene("TestEnemyPool");
        }
    }
}
