using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    List<Vector3> poses;  //�ƶ��±������
    int cnt;              //���������ƶ���һ��ѭ�� �� �ƶ��������±�
    // Start is called before the first frame update
    void Start()
    {
        //��ȡ�ƶ����±꣬�����ҽű��������������
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


