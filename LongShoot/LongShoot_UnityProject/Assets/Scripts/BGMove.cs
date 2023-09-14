using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public Material mat;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().sharedMaterial; 
    }

    // Update is called once per frame
    void Update()
    {
        mat.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
    }
}
