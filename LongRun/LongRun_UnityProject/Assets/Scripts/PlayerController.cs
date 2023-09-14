using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCha cha;
    bool changeColor;
    bool jump;
    bool isRun;

    // Start is called before the first frame update
    void Start()
    {
        cha = GetComponent<PlayerCha>();
        changeColor = false;
        isRun = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            changeColor = true;
        }
        if (Input.GetKey(KeyCode.D)) { isRun = true; }
        else                         { isRun = false; }

    }

    private void FixedUpdate()
    {
        cha.Move(isRun, jump, changeColor);
        jump = false;
        changeColor = false;
    }
}
