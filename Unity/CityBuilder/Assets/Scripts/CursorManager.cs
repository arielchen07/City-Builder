using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The CursorManager class is responsible for controlling the cursor sprite.<br/>
/// This class is a singleton and is globally accessible.
/// The cursor changes sprite when the user is placing objects and roads, or deleting roads. <br/>
/// </summary>
public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursor_default;
    [SerializeField] private Texture2D cursor_placing;
    [SerializeField] private Texture2D cursor_deleting;


    public static CursorManager cursorManager;
    private void Awake()
    {
        if (cursorManager != null && cursorManager != this)
        {
            Destroy(this);
        }
        else
        {
            cursorManager = this;
        }
    }
    /// <summary>
    /// Changes the cursor's sprite depending on the inputted mode.<br/>
    /// Changes the cursor's center depending on which sprite is being used.<br/>
    /// </summary>
    /// <param name="mode">The mode of placement the user is using, i.e placing or deleting</param>
    public void SetCursorMode(string mode)
    {
        Texture2D currCursor;
        Vector2 hotspot;
        switch (mode)
        {
            case "placing":
                hotspot = new Vector2(cursor_placing.width / 2, cursor_placing.height * 0.785f);
                currCursor = cursor_placing;
                break;
            case "deleting":
                hotspot = new Vector2(cursor_deleting.width / 2, cursor_deleting.height * 0.785f);
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
