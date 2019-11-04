using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public Item[] itemList = new Item[7];
    public InventorySlot[] inventorySlots = new InventorySlot[7];
    public Item emptySlot;
    public GameObject DescriptionPanel;
    public GameObject descriptionImage;
    public int openedSlot = -1;

    public Item testItem1;
    public Item testItem2;
    public Item testItem3;
    public GameObject instructionText;

    private string[] keyList = {"1", "2", "3", "4", "5", "6", "7"};
    private string currentTimeState = "Modern";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
        //DontDestroyOnLoad(this);
    }

    private void Start()
    {
        ResetAllSlots();

        //Add(testItem1);
        //Add(testItem2);
        //Add(testItem3);
        UpdateSlotUI();
    }

    private void ResetAllSlots()
    {
        for(int i = 0; i < itemList.Length; i++)
        {
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(emptySlot), itemList[i]);
        }
    }

    private bool Add(Item item)
    {
        //for (int i = 0; i < itemList.Length; i++)
        //{
        //    if(itemList[i].itemName == item.itemName)
        //    {
        //        if(itemList[i].currentStack < itemList[i].maxStack)
        //        {
        //            itemList[i].currentStack++;
        //            UpdateSlotUI();
        //            return true;
        //        }
        //    }
        //}

        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i].itemType == ItemTypes.EmptySlot)
            {
                JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(item), itemList[i]);
                UpdateSlotUI();
                return true;
            }
        }
        return false;
    }

    public void UpdateSlotUI()
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].UpdateSlot();
        }
    }

    public void AddItem(Item item)
    {
        bool hasAdded = Add(item);
        if (hasAdded)
        {
            UpdateSlotUI();
        }
    }

    public void ItemDateChange(string timeState)
    {

        for(int i = 0; i < itemList.Length; i++)
        {
            if (timeState == "Modern")
            {
                itemList[i].isAncient = false;
            }
            else
            {
                itemList[i].isAncient = true;
            }
        }
        UpdateSlotUI();
    }

    private void Update()
    {
        for(int i = 0; i < keyList.Length; i++)
        {
            if (Input.GetKeyDown(keyList[i]) && itemList[i].itemType != ItemTypes.EmptySlot)
            {
                // Debug.Log("Get " + keyList[i] + " Press");
                openedSlot = i;
                DescriptionPanel.SetActive(true);
                Text[] textList = DescriptionPanel.GetComponentsInChildren<Text>();
                textList[0].text = itemList[i].consumeAction + " (Press E)";
                if (itemList[i].itemType == ItemTypes.Puzzle)
                {
                    textList[1].text = "Puzzle";
                    textList[2].text = "";
                    descriptionImage.SetActive(true);
                    descriptionImage.GetComponent<Image>().sprite = itemList[i].descriptionIcon;
                }
                else
                {
                    descriptionImage.SetActive(false);
                    textList[1].text = itemList[i].itemName;
                    if (itemList[i].isAncient)
                    {
                        textList[2].text = itemList[i].itemAncientDescription;

                    }
                    else
                    {
                        textList[2].text = itemList[i].itemModernDescription;
                    }
                }

                InstructionController.underInstruction = true;
                DialogController.dialogActive = false;
                instructionText.SetActive(true);
                instructionText.GetComponent<Text>().text = "Press Q to quit";
                
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            openedSlot = -1;
            InstructionController.underInstruction = false;
            DescriptionPanel.SetActive(false);
            instructionText.SetActive(false);
        }
    }

    public void changeTimeState()
    {
        DescriptionPanel.SetActive(false);
        instructionText.SetActive(false);
        DialogController.dialogActive = false;

        if (currentTimeState == "Modern")
        {
            currentTimeState = "Ancient";
            ItemDateChange(currentTimeState);
        }
        else
        {
            currentTimeState = "Modern";
            ItemDateChange(currentTimeState);
        }
    }
}
