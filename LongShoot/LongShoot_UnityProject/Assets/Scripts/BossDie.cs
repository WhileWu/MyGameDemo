using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie : MonoBehaviour
{
    GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (obj.GetComponent<EnemyBeHit>().hp <= 0)
        {
            //Debug.Log("BossËÀÍö");
            GameMode.Instance.OnPlayerWin();
        }
    }
}
