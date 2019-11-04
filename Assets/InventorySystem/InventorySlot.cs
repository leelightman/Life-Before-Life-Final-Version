using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDragHandler
{

    public GameObject icon;
    public Text stack;

    private int slotIndex;

    void Start()
    {
        slotIndex = transform.GetSiblingIndex();
    }

    public void UpdateSlot()
    {
        if(Inventory.instance.itemList[slotIndex].itemType != ItemTypes.EmptySlot)
        {
            if (Inventory.instance.itemList[slotIndex].isAncient)
            {
                icon.GetComponent<Image>().sprite = Inventory.instance.itemList[slotIndex].ancientIcon;
            }
            else
            {
                icon.GetComponent<Image>().sprite = Inventory.instance.itemList[slotIndex].modernIcon;
            }

            if (Inventory.instance.itemList[slotIndex].currentStack > 1)
            {
                stack.text = "x" + Inventory.instance.itemList[slotIndex].currentStack.ToString();
            }
            else
            {
                stack.text = "";
            }
            icon.SetActive(true);
        }
        else
        {
            icon.SetActive(false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Item droppedItem = Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()];
        if(eventData.pointerDrag.transform.parent.name == gameObject.name)
        {
            return;
        }
        if(Inventory.instance.itemList[slotIndex].itemType == ItemTypes.EmptySlot)
        {
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(droppedItem), Inventory.instance.itemList[slotIndex]);
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(Inventory.instance.emptySlot), Inventory.instance.itemList[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetSiblingIndex()]);
            Inventory.instance.UpdateSlotUI();
        }
        else
        {
            Item tempItem = Inventory.instance.itemList[slotIndex];
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(droppedItem), Inventory.instance.itemList[slotIndex]);
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(tempItem), droppedItem);
            Inventory.instance.UpdateSlotUI();
        }
    }
}
