using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //�ӵ�ģ��
    // Start is called before the first frame update
    public Transform prefabBullet;//ָ���ӵ�����
    [SerializeField]
    public List<Transform> skills;//ָ������

    public float minTime;
    public float maxTime;

    void Start()
    {
        skills = new List<Transform>();
        StartCoroutine(Fire());
        if (skills.Count > 0) { StartCoroutine(useSkill()); }
    }


    IEnumerator Fire()
    {
        while (true)
        {
            float t = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(t);
            //�����ӵ�
            Vector3 pos = transform.position + new Vector3(0, -0.5f, 0);
            Instantiate(prefabBullet, pos, Quaternion.identity);
        }
    }

    IEnumerator useSkill()
    {
        yield return new WaitForSeconds(4.5f);
    }
}
