using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemManager : MonoBehaviour
{
    int counter = 1;
    //Unity单例
    public static WorldItemManager Instance { get; private set; }
    //饿汉式写法
    private void Awake()
    {
        Instance = this;
    }

    Dictionary<int, ItemData> allItems = new Dictionary<int, ItemData>();

    // Start is called before the first frame update
    void Start()
    {
        ////测试添加道具
        //for (int i = 0;i < 10;++i)
        //{
        //    ItemData item = CreateItem(Random.Range(1001, 1005));
        //    //Debug.Log(UIManager.Instance);
        //    UIManager.Instance.SetItem(i, item);
        //}
    }
    public ItemData CreateItem(int jsonID)
    {
        ItemData item = new ItemData();
        item.autoId = counter;
        ++counter;
        item.jsonId = jsonID;
        item.quality = Random.Range(1, 5);
        item.addAtk = Random.Range(10, 100);
        //添加到全部道具字典中
        allItems.Add(item.autoId, item);
        return item;
    }

    public void removeItem(int autoID)
    {
        allItems.Remove(autoID);
    }
}
