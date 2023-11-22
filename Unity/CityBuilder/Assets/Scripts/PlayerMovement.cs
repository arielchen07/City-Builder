using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    Vector3 moveDir;
    void Start()
    {
        
    }

    void Update()
    {
        moveDir = new Vector3(0,0,0);
        if (Input.GetKey(KeyCode.W)){
            moveDir = new Vector3(1,0,0);
        }
    }
}
