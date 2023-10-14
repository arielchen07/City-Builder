using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraController : MonoBehaviour
{
    [SerializeField] public float cameraSensitivity;
    [SerializeField] public float zoomSensitivity;
    public Camera cam;
    Vector3 moveDir;
    float camSize = 5;
    // WASD controls for the camera
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(0,0,0);
        Vector3 WInput = new Vector3(0,0,0);
        Vector3 SInput = new Vector3(0,0,0);
        Vector3 AInput = new Vector3(0,0,0);
        Vector3 DInput = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.W)){
            WInput = new Vector3(1,0,1) * Mathf.Sqrt(2);
        }
        if(Input.GetKey(KeyCode.S)){
            SInput = new Vector3(-1,0,-1) * Mathf.Sqrt(2);
        }
        if(Input.GetKey(KeyCode.A)){
            AInput = new Vector3(-1,0,1);
        }
        if(Input.GetKey(KeyCode.D)){
            DInput = new Vector3(1,0,-1);
        }
        camSize -= Input.mouseScrollDelta.y * zoomSensitivity;
        camSize = Mathf.Clamp(camSize, 1, 10);
        cam.orthographicSize = camSize;
        moveDir = WInput + SInput + AInput + DInput; //has issues with normalizing movement when for example w and a are pressed at the same time, but if normalized, the sqrt2 multiplier to w and s will stop working
        transform.Translate(moveDir * cameraSensitivity * 0.05f); //0.05 is an arbitrary number to slow down the movement, so that a moveSpeed of 1 is manageable
    }
}
