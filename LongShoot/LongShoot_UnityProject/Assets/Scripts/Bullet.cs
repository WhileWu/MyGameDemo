using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 6;
    public Transform explPrefab;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        if (explPrefab)
        {
            Instantiate(explPrefab, transform.position, Quaternion.identity);
        }
    }
}
