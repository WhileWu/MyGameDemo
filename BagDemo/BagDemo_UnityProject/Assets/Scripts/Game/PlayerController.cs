using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCharacter character;

    void Start()
    {
        character = GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        bool jump = Input.GetKeyDown(KeyCode.Space);
        character.Move(h, z, jump);

        if(h !=0 || z !=0)
        {
            var dir = Vector3.right * h + Vector3.forward * z;
            character.Turn(dir, 10);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            character.Jump();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            character.CatchBox();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            character.PickUp();
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            character.AddRandomItem();
        }
#endif
    }

}
