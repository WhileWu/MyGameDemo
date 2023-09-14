using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    List<Vector3> poses;  //移动下标的容器
    int cnt;              //计数器，移动的一轮循环 和 移动容器的下标
    // Start is called before the first frame update
    void Start()
    {
        //获取移动的下标，在所挂脚本的物体的子物体
        poses = new List<Vector3>();
        for(int i = 0;i < transform.childCount;i++)
        {
            poses.Add(transform.GetChild(i).position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, poses[cnt], 3 * Time.deltaTime);
        if(Vector3.Distance( transform.position, poses[cnt] ) < 0.1f) {   cnt++;   }
        if(cnt == transform.childCount)                                                        {  cnt = 0;  }
    }
}


