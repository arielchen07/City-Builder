using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text quantityTxt;

    [SerializeField]
    private Image borderImage;

    public event Action<UIInventoryItem> ItemOnClicked,
        ItemOnDropped, ItemBeginDrag, ItemEndDrag,
        RightMouseBtnOnClick;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }

    private void Deselect()
    {
        borderImage.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityTxt.text = quantity + "";
        empty = false;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    public void OnBeginDrag()
    {
        if (empty)
            return;
        ItemBeginDrag?.Invoke(this);
    }

    public void OnDrop()
    {
        ItemOnDropped?.Invoke(this);
    }

    public void OnEndDrag()
    {
        ItemEndDrag?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data)
    {
        if (empty)
            return;
        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            RightMouseBtnOnClick?.Invoke(this);
        }
        else
        {
            ItemOnClicked?.Invoke(this);
        }
    }
}
