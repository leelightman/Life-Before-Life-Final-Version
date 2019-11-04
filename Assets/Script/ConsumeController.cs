using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumeController : MonoBehaviour
{
    public Item targetItem;
    public GameObject descriptionUI;
    public GameObject[] outcomes = new GameObject[1];
    public GameObject instructionText;
    public GameObject DialogText;

    public GameObject OldSign;
    public GameObject NewSign;

    public string stateToUse;

    private void Start()
    {
        //VQ
        if (OldSign != null)
        {
            OldSign.SetActive(true);
            NewSign.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "player")
        {
            if (descriptionUI.activeSelf == true && descriptionUI.GetComponentsInChildren<Text>()[1].text.ToString() == targetItem.name && Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < 7; i++)
                {
                    //Debug.Log(Inventory.instance.itemList[i].itemName);
                    if (Inventory.instance.itemList[i].itemName == targetItem.name)
                    {
                        if (stateToUse == "" || Inventory.instance.itemList[i].isAncient == (stateToUse == "Ancient"))
                        {
                            InstructionController.underInstruction = false;
                            descriptionUI.SetActive(false);
                            instructionText.SetActive(false);

                            if (Inventory.instance.itemList[i].currentStack != 1)
                            {
                                Inventory.instance.itemList[i].currentStack -= 1;
                            }
                            else
                            {
                                JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(Inventory.instance.emptySlot), Inventory.instance.itemList[i]);
                                Inventory.instance.UpdateSlotUI();
                            }

                            for (int j = 0; j < outcomes.Length; j++)
                            {
                                outcomes[j].SetActive(!outcomes[j].activeSelf);
                            }

                            break;
                        }
                        else
                        {
                            if (DialogText != null)
                            {
                                InstructionController.underInstruction = false;
                                descriptionUI.SetActive(false);
                                instructionText.SetActive(false);

                                DialogController.dialogActive = true;
                                DialogText.SetActive(true);
                                DialogText.GetComponent<Text>().text = "\"The key is too rusty. Btw, do you know oil is a type of natrual sealant for metal?\"";
                            }
                        }

                    }
                }

                

                //VQ
                if(OldSign != null)
                {
                    OldSign.SetActive(false);
                    NewSign.SetActive(true);
                }
            }
        }
    }
}
