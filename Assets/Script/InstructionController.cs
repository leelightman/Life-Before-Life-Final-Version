using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionController : MonoBehaviour
{
    public GameObject DialogText;

    public static bool underInstruction = false;

    private bool firstTimeCollect = true;
    private bool firstTimeQuit = true;

    private void Update()
    {
        if (firstTimeCollect)
        {
            if (Inventory.instance.itemList[0].itemType != ItemTypes.EmptySlot)
            {
                //underInstruction = true;
                firstTimeCollect = false;
                DialogController.dialogActive = true;
                DialogText.GetComponent<Text>().text = "\"Press 1 and check the item you just collect.\"";
                DialogText.SetActive(true);
            }
        }

        if (firstTimeQuit && Input.GetKey(KeyCode.Q) && Inventory.instance.itemList[0].itemName == "Apple")
        {
            firstTimeQuit = false;
            DialogController.dialogActive = true;
            DialogText.GetComponent<Text>().text = "\"Now find a proper place to plant that apple. Emmm...maybe a pit?\"";
            DialogText.SetActive(true);
        }
    }
}
