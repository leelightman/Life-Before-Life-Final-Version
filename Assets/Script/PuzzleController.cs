using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleController : MonoBehaviour
{
    public GameObject descriptionUI;
    public GameObject[] puzzlePref = new GameObject[8];
    public string puzzleName;
    public int slotIndex = -1;
    public GameObject parent;
    public GameObject instructionText;
    public bool[] puzzleStatue = { false, true, true, false, true, true, false, true };

    private string slotLookingAt;
    private GameObject slot;
    private GameObject newPuzzle;
    private int complete;

    private void Update()
    {
        slotLookingAt = CastingToObject.selectedObject;
        if (slotLookingAt.Contains("puzzleSlot"))
        {
            slot = GameObject.Find(slotLookingAt);
            if (descriptionUI.activeSelf == true && descriptionUI.GetComponentsInChildren<Text>()[1].text.ToString() == "Puzzle")
            {
                slotIndex = Inventory.instance.openedSlot;
                puzzleName = Inventory.instance.itemList[Inventory.instance.openedSlot].itemName;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    int index = Inventory.instance.itemList[Inventory.instance.openedSlot].itemName[6] - '1';

                    int posIndex = slotLookingAt[10] - '1';

                    if (index == posIndex)
                    {
                        puzzleStatue[index] = true;
                    }

                    newPuzzle = Instantiate(puzzlePref[index], slot.transform.position, slot.transform.rotation, parent.transform);

                    if ((posIndex - index) % 2 == 0)
                    {
                        newPuzzle.transform.Rotate(new Vector3(0, 90 * (posIndex - index) / 2, 0));
                    }
                    else
                    {
                        if (index % 2 == 1)
                        {
                            newPuzzle.transform.Rotate(new Vector3(0, 90 * (posIndex - index - 1) / 2 - 180, 0));
                        }
                        else
                        {
                            newPuzzle.transform.Rotate(new Vector3(0, 90 * (posIndex - index - 1) / 2 - 90, 0));
                        }
                    }

                    Selected newPuzzleSelected = newPuzzle.GetComponent<Selected>();
                    newPuzzleSelected.itemInOtherTime = slot;
                    newPuzzle.SetActive(true);
                    slot.SetActive(false);

                    InstructionController.underInstruction = false;
                    descriptionUI.SetActive(false);
                    instructionText.SetActive(false);
                    JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(Inventory.instance.emptySlot), Inventory.instance.itemList[Inventory.instance.openedSlot]);
                    Inventory.instance.UpdateSlotUI();

                }
            }
        }

        complete = 0;
        for (int i = 0; i < puzzleStatue.Length; i++)
        {
            if (puzzleStatue[i])
                complete += 1;
        }
        if (complete == 8)
        {
            //TODO - the end
            SceneManager.LoadScene("Win Scene");
        }
    }

}
