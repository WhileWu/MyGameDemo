using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人对象的枚举参数，防呆设计，尽量避免传参传错，如果用字符串传参获取gameobject，容易写错参数
/// </summary>
public enum EnemyObj{
    Bear,
    SkeletonNormal
}

/// <summary>
/// 敌人的对象池，减少gc, 单例模式
/// </summary>
public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }
    public Dictionary<EnemyObj, Queue<GameObject>> pool;
    public Dictionary<EnemyObj, GameObject> prefabs;

    public List<EnemyObj> listEnemyObj;
    public List<GameObject> listObj;

    public int id = 1;

    private void Awake()
    {
        Debug.Log("Awake Pool");
        if (Instance != null)
        {
            Debug.Log("存在Instance");
            Debug.Log(this.id);
            //Destroy(this);
            //return;
        }
        Instance = this;
        pool = new Dictionary<EnemyObj, Queue<GameObject>>();
        prefabs = new Dictionary<EnemyObj, GameObject>();
        //listEnemyObj = new List<EnemyObj>();
        //listObj = new List<GameObject>();

        for (int i = 0;i < listEnemyObj.Count;++i)
        {
            pool[listEnemyObj[i]] = new Queue<GameObject>();
            prefabs[listEnemyObj[i]] = listObj[i];
        }
        DontDestroyOnLoad(gameObject);
    }

    public GameObject GetObj(EnemyObj enemyObj)
    {
        if(pool[enemyObj].Count != 0)
        {
            Debug.Log("缺少" + enemyObj);
            GameObject go = pool[enemyObj].Dequeue();
            go.SetActive(true);
            ResetEnemy();                           //需要重新设置一些字段
            return go;
        }
        Debug.Log("存在" + enemyObj);
        return Instantiate(prefabs[enemyObj]);
    }

    private void ResetEnemy()
    {
        
    }

    public void DestoryObj(GameObject gameObj, EnemyObj enemyObj)
    {
        gameObj.SetActive(false);
        pool[enemyObj].Enqueue(gameObj);
        gameObj.transform.SetParent(gameObject.transform);
    }
}
