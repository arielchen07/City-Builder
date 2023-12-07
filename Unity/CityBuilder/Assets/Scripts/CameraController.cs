using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

/// <summary>
/// The CameraController class is responsible for controlling the camera. <br/>
/// This class is attached to the Camera Container Game Object.
/// </summary>
public class CameraController : MonoBehaviour
{
    public float cameraSensitivity;
    public float zoomSensitivity;
    public Camera cam;
    Vector3 moveDir;
    float camSize = 5;
    public bool isLocked = false;
    public float xBound;
    public float zBound;

    /// <summary>
    /// Gets the user's WASD inputs and moves the camera accordingly.
    /// The camera is angled, so movement has to be projected onto the correct movement plane.
    /// </summary>
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
        //has issues with normalizing movement when for example w and a are pressed at the same time, but if normalized, the sqrt2 multiplier to w and s will stop working
        moveDir = WInput + SInput + AInput + DInput; 
        if (!isLocked)
        {
            //0.05 is an arbitrary number to scale down the movement speeed, so that a moveSpeed of 1 is manageable. 
            //The actual movement speed of the camera can be adjusted by changing the cameraSensitivity attribute.
            transform.Translate(0.05f * cameraSensitivity * moveDir * camSize); 
        }
        //if the cursor is not over a UI element, i.e the inventory, make the camera zoom in or out via the scroll wheel
        //the scroll is limited from 1 to 10.
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            camSize -= Input.mouseScrollDelta.y * zoomSensitivity;
            camSize = Mathf.Clamp(camSize, 1, 10);
            cam.orthographicSize = camSize;
        }
        float camX = transform.position.x;
        float camZ = transform.position.z;
        //limit the camera's position so it stays on the map. The x and z bounds are adjustable in the editor for maps of different sizes.
        //If the map size becomes dynamic, change x and z bounds to be a factor of the map size.
        transform.position = new Vector3(Mathf.Clamp(camX, -(xBound + 1) * 10f / (camSize + 1),
        (xBound - 5) * 10f / camSize), transform.position.y,
        Mathf.Clamp(camZ, -(zBound + 1) * 10f / (camSize + 1), (zBound - 5) * 10f / camSize));
    }
    /// <summary>
    /// Moves camera to focus on an object the player selected and disables camera movement until the user clicks off the object.
    /// Called by SelectObject in PlacementSystem.
    /// </summary>
    /// <param name="targetPos">The position the selected object is at.</param>
    public void ZoomToItem(Vector3 targetPos)
    {
        Vector3 targetCameraPos = new Vector3(targetPos.x - 5, 5, targetPos.z - 5);
        StartCoroutine(MoveToTarget(targetCameraPos));
        isLocked = true;
    }
    /// <summary>
    /// The coroutine that moves the camera to the desired position. Called by ZoomToItem.
    /// </summary>
    /// <param name="targetPos">The target position to move the camera to.</param>
    /// <returns></returns>T
    IEnumerator MoveToTarget(Vector3 targetPos)
    {
        float moveTime = 0.5f;
        float counter = 0f;
        Vector3 startingPos = transform.position;
        while (counter < moveTime)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, counter / moveTime);
            counter += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }
}
