using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FllowCam : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }


    private void FixedUpdate()
    {
        Vector3 to = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, to, 0.4f);
    }
}
