using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCharacter player;
    [HideInInspector]
    public float h, v;
    [HideInInspector]
    public bool isJump;

    public bool attack;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        isJump = Input.GetButtonDown("Jump");
        attack = Input.GetKeyDown(KeyCode.J);
    }
}
