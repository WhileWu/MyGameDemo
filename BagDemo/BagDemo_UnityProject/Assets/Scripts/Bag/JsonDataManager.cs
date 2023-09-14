using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemJsonData
{
    public int id { get; }
    public int type { get; }
    public string name { get; }
    public int atk { get; }
    public int def { get; }
    public string modelPath { get; }
    public string imagePath { get; }
    public float crit { get; }

    public ItemJsonData(int id, int type, string name, int atk, int def, string modelPath, string imagePath, float crit)
    {
        this.id = id;
        this.type = type;
        this.name = name;
        this.atk = atk;
        this.def = def;
        this.modelPath = modelPath;
        this.imagePath = imagePath;
        this.crit = crit;
    }
}
public class JsonDataManager
{
    // C#单例
    private static JsonDataManager instance;
    public static JsonDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new JsonDataManager();
                //instance初始化
                instance.InitItemJsonData();
            }
            return instance;
        }
    }
    // Dictionary,保存所有道具数据, <道具ID， ItemJsonData>
    Dictionary<int, ItemJsonData> items;
    //把Json文件的内容，填到字典里
    private void InitItemJsonData()
    {
        items = new Dictionary<int, ItemJsonData>();
        string str = Resources.Load<TextAsset>("Json/ItemData").text;
        JsonData jd = JsonMapper.ToObject(str);
        for (int i = 0;i < jd.Count;++i)
        {
            //int id, int type, string name, int atk, int def, string modelPath, string imagePath, float crit
            int id = int.Parse(jd[i]["ID"].ToString());
            int type = int.Parse(jd[i]["Type"].ToString());
            string name = jd[i]["Name"].ToString();
            int atk = int.Parse(jd[i]["Attack"].ToString());
            int def = int.Parse(jd[i]["Defense"].ToString());
            string modelPath = jd[i]["ModelPath"].ToString();
            string imagePath = jd[i]["ImagePath"].ToString();
            float cirt = float.Parse(jd[i]["Crit"].ToString());
            items.Add(id, new ItemJsonData(id, type, name, atk, def, modelPath, imagePath, cirt));
            //Debug.Log("id" + id);
        }
    }

    public ItemJsonData GetItemJson(int id)
    {
        if (items.ContainsKey(id))
        {
            return items[id];
        }
        Debug.LogError($"道具ID{id} 不存在!");
        return null;
    }
}
