using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemManager : MonoBehaviour
{
    int counter = 1;
    //Unity����
    public static WorldItemManager Instance { get; private set; }
    //����ʽд��
    private void Awake()
    {
        Instance = this;
    }

    Dictionary<int, ItemData> allItems = new Dictionary<int, ItemData>();

    // Start is called before the first frame update
    void Start()
    {
        ////������ӵ���
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
        //��ӵ�ȫ�������ֵ���
        allItems.Add(item.autoId, item);
        return item;
    }

    public void removeItem(int autoID)
    {
        allItems.Remove(autoID);
    }
}
