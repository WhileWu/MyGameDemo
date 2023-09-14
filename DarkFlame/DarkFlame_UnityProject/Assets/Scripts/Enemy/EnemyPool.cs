using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˶����ö�ٲ�����������ƣ��������⴫�δ���������ַ������λ�ȡgameobject������д�����
/// </summary>
public enum EnemyObj{
    Bear,
    SkeletonNormal
}

/// <summary>
/// ���˵Ķ���أ�����gc, ����ģʽ
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
            Debug.Log("����Instance");
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
            Debug.Log("ȱ��" + enemyObj);
            GameObject go = pool[enemyObj].Dequeue();
            go.SetActive(true);
            ResetEnemy();                           //��Ҫ��������һЩ�ֶ�
            return go;
        }
        Debug.Log("����" + enemyObj);
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
