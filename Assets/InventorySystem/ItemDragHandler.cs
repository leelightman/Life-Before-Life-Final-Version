using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // public Item item;

    private Transform originalParent;
    private bool isDragging;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Inventory.instance.itemList[transform.parent.GetSiblingIndex()].itemType != ItemTypes.EmptySlot)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                isDragging = true;
                originalParent = transform.parent;
                transform.SetParent(transform.parent.parent);
                //GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Inventory.instance.itemList[originalParent.transform.GetSiblingIndex()].itemType != ItemTypes.EmptySlot && eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = false;
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            //GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
