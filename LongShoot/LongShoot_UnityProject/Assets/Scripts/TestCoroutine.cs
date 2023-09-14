using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    static int i = 0;
    private void Start()
    {
  
            StartCoroutine(PrintI());
            StartCoroutine(AddI());
            StartCoroutine(MinusI());
    }

    IEnumerator PrintI()
    {
        while (true)
        {
            Debug.Log($"打印i = {i}");
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator AddI()
    {
        while (true)
        {
            i++;
            Debug.Log($"增加i, i = {i}");
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator MinusI()
    {
        while (true)
        {
            i--;
            Debug.Log($"减少i, i = {i}");
            yield return new WaitForSeconds(1.2f);
        }
    }

}
