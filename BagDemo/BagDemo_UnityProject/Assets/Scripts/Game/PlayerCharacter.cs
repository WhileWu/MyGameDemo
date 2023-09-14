using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public Transform catchObj;
    public Transform catchPos;
    //总道具数量
    public const int N = 20;
    List<ItemData> items = new List<ItemData>();
    //查找第一个为空的格子 
    public int FindFirstEmpty()
    {
        for (int i = 0;i < items.Count;++i)
        {
            if (items[i] == null) { return i; }
        }
        if(items.Count == N)
        {
            //背包彻底满了
            return -1;
        }
        //列表长度不足，扩容
        items.Add(null);
        return items.Count - 1;
    }
    public void CatchBox()
    {
        if(catchObj != null)
        {
            //如果有抓取物,释放
            catchObj.SetParent(null);
            catchObj.GetComponent<Rigidbody>().isKinematic = false;
            catchObj = null;
            animator.SetBool("Catch", false);
        }
        else
        {
            //如果没有抓取物，发射射线尝试抓取
            var dist = cc.radius;

            //接收射线返回的信息
            RaycastHit hit;
            //打一条线段起点、终点
            Debug.DrawLine(transform.position, transform.position + transform.forward * (dist + 1), Color.red, 10f);
            //打一条射线起点、方向、长度
            if(Physics.Raycast(transform.position,transform.forward,out hit , dist + 1))
            {
                if(hit.collider.CompareTag("GrabBox"))
                {
                    catchObj = hit.transform;
                    catchObj.SetParent(catchPos);
                    catchObj.localPosition = Vector3.zero;
                    catchObj.localRotation = Quaternion.identity;
                    catchObj.GetComponent<Rigidbody>().isKinematic = true;
                    animator.SetBool("Catch", true);
                }
            }
        }
    }

    //捡周围道具
    public void PickUp()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1);
        foreach (var c in colliders)
        {
            if (c.CompareTag("Model"))
            {
                ModelInfo info = c.GetComponent<ModelInfo>();
                ItemData item = info.itemData;
                
                Destroy(c.gameObject);
                
                //item加到背包列表里
                int index = FindFirstEmpty();
                if (index == -1)
                {
                    Debug.Log("背包满了");
                    return;
                }
                items[index] = item;
                //刷新界面
                UIManager.Instance.SetItem(index, item);
            }
        }
    }

    public void AddRandomItem()
    {
        //测试添加道具
        ItemData item = WorldItemManager.Instance.CreateItem(Random.Range(1001, 1005));
        //UIManager.Instance.SetItem(i, item);
        //item加到背包列表里
        int index = FindFirstEmpty();
        if(index == -1)
        {
            Debug.Log("背包满了");
            return;
        }
        items[index] = item;
        //刷新界面
        UIManager.Instance.SetItem(index, item);
    }

    public  void SwapItem(ItemData item, int to)
    {
        int from = items.IndexOf(item);
        if (from == -1)
        {
            Debug.LogError($"道具不存在! {item.autoId} {item.Json.name}");
            return;
        }
        //to有可能超出items的长度,扩容
        for (int i = items.Count;i <= to;++i)
        {
            items.Add(null);
        }

        //交换逻辑
        items[from] = items[to];
        items[to] = item;

        //刷新界面
        UIManager.Instance.SetItem(from, items[from]);
        UIManager.Instance.SetItem(to, items[to]);
    }

    public void RestoreItem(ItemData item)
    {
        int index = items.IndexOf(item);
        if (index == -1)
        {
            Debug.LogError($"道具不存在! {item.autoId} {item.Json.name}");
            return;
        }
        UIManager.Instance.SetItem(index, item);
    }

    public void removeItem(ItemData item)
    {
        int index = items.IndexOf(item);
        if(index == -1)
        {
            Debug.LogError($"道具不存在! {item.autoId} {item.Json.name}");
            return;
        }
        items[index] = null;
        WorldItemManager.Instance.removeItem(item.autoId);

        //刷新界面
        UIManager.Instance.SetItem(index, null);
    }
}
