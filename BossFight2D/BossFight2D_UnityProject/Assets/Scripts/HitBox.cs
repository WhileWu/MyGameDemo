using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TriEnter");
        if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<Boss>().GetHit(1);
        }
    }
}
