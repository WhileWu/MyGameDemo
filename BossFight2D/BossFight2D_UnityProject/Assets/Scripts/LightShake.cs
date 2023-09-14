using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightShake : MonoBehaviour
{
    Light2D light2D;
    public float maxLight = 1.4f;
    public float minLight = 0.7f;
    public float changeLightTime = 3f;

    float changeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        changeSpeed = (maxLight - minLight) / changeLightTime;
        light2D = GetComponent<Light2D>();
        StartCoroutine(CoLight());
    }

    IEnumerator CoLight()
    {
        while (true)
        {
            while (light2D.intensity < maxLight)
            {
                light2D.intensity += changeSpeed * Time.deltaTime;
                yield return null;
            }
            while (light2D.intensity > maxLight)
            {
                light2D.intensity -= changeSpeed * Time.deltaTime;
                yield return null;
            }

        }
    }

}
