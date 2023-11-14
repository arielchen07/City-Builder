using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public float cameraSensitivity;
    public float zoomSensitivity;
    public Camera cam;
    Vector3 moveDir;
    float camSize = 5;
    public bool isLocked = false;
    // WASD controls for the camera
    void Update()
    {
        moveDir = new Vector3(0, 0, 0);
        Vector3 WInput = new Vector3(0, 0, 0);
        Vector3 SInput = new Vector3(0, 0, 0);
        Vector3 AInput = new Vector3(0, 0, 0);
        Vector3 DInput = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            WInput = new Vector3(1, 0, 1) * Mathf.Sqrt(2);
        }
        if (Input.GetKey(KeyCode.S))
        {
            SInput = new Vector3(-1, 0, -1) * Mathf.Sqrt(2);
        }
        if (Input.GetKey(KeyCode.A))
        {
            AInput = new Vector3(-1, 0, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            DInput = new Vector3(1, 0, -1);
        }
        moveDir = WInput + SInput + AInput + DInput; //has issues with normalizing movement when for example w and a are pressed at the same time, but if normalized, the sqrt2 multiplier to w and s will stop working
        if(!isLocked){
            transform.Translate(0.05f * cameraSensitivity * moveDir); //0.05 is an arbitrary number to scale down the movement speeed, so that a moveSpeed of 1 is manageable
        }
        
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            camSize -= Input.mouseScrollDelta.y * zoomSensitivity;
            camSize = Mathf.Clamp(camSize, 1, 10);
            cam.orthographicSize = camSize;
        }
    }

    public void ZoomToItem(Vector3 targetPos){
        Vector3 targetCameraPos = new Vector3(targetPos.x - 5, 5, targetPos.z - 5);
        StartCoroutine(MoveToTarget(targetCameraPos));
        isLocked = true;
    }

    IEnumerator MoveToTarget(Vector3 targetPos){
        float moveTime = 0.5f;
        float counter = 0f;
        Vector3 startingPos = transform.position;
        while (counter < moveTime){
            transform.position = Vector3.Lerp(startingPos, targetPos, counter/moveTime);
            counter += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }
}
