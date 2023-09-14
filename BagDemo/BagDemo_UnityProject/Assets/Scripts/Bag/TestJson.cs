using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        JsonDataManager jdm = JsonDataManager.Instance;
        var item1001 = jdm.GetItemJson(1001);
        Debug.Log(item1001.modelPath);
        Debug.Log(item1001.imagePath);
        Debug.Log(item1001.atk);
        Debug.Log(item1001.crit);
        Debug.Log(item1001.def);
        Debug.Log(item1001.name);
        Debug.Log(item1001.type);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
