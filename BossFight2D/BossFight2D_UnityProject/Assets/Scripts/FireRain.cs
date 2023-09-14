using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRain : MonoBehaviour
{
    public float speed = 7;
    bool isExplode = false;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isExplode)
        {
            transform.position += speed * Time.deltaTime * Vector3.down;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger("Explode");
        isExplode = true;
        //Destroy(gameObject);
        Invoke("DestoryThis", 1);
    }

    void DestoryThis()
    {
        Destroy(gameObject);
    }
}
