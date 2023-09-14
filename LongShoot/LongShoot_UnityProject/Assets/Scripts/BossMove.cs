using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    enum State
    {
        Down,
        LeftRight,
    }
    State state = State.Down;
    public float minY = 2.6f;
    public float minX = -2.3f;
    public float maxX = 2.3f;
    public float speed = 3.0f;
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Down: {
                    transform.position += Vector3.down * speed * Time.deltaTime;
                    if(transform.position.y < minY)
                    {
                        state = State.LeftRight;
                    }
                    break;
                }
            case State.LeftRight: {
                    transform.position += Vector3.left * speed * Time.deltaTime;
                    if (transform.position.x < minX)
                    {
                        speed = -Mathf.Abs(speed);
                    }
                    if (transform.position.x > maxX)
                    {
                        speed = +Mathf.Abs(speed);
                    }
                    break;
                }
        }
        
    }
}
