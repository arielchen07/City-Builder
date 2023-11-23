using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursor_default;
    [SerializeField] private Texture2D cursor_placing;
    [SerializeField] private Texture2D cursor_deleting;
    

    public static CursorManager cursorManager;
    private void Awake(){
        if (cursorManager != null && cursorManager != this){
            Destroy(this);
        } else {
            cursorManager = this;
        }
    }
    public void SetCursorMode(string mode){
        Texture2D currCursor;
        Vector2 hotspot;
        switch (mode) {
            case "placing":
            hotspot = new Vector2(cursor_placing.width/2, cursor_placing.height * 0.785f);
            currCursor = cursor_placing;
            break;
            case "deleting":
            hotspot = new Vector2(cursor_deleting.width/2, cursor_deleting.height * 0.785f);
            currCursor = cursor_deleting;
            break;
            default:
            hotspot = Vector2.zero;
            currCursor = cursor_default;
            break;
        }
        Cursor.SetCursor(currCursor, hotspot, CursorMode.Auto);
    }
}
