using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public int jsonId;
    public int autoId;
    public int quality;
    public int addAtk;

    public ItemJsonData Json
    {
        get
        {
            return JsonDataManager.Instance.GetItemJson(jsonId);
        }
    }
}
