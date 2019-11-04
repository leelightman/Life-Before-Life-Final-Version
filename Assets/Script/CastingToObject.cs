using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastingToObject : MonoBehaviour
{
    public static string selectedObject = "";
    public float distance;
    public string internalObject;
    public RaycastHit theObject;
    public GameObject instructionText;

    private GameObject lastSelectedObject;

    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out theObject))
        {
            selectedObject = theObject.transform.gameObject.name;
            internalObject = theObject.transform.gameObject.name;

            if (theObject.distance < distance && theObject.transform.gameObject.tag == "Collectable")
            {
                if (lastSelectedObject != null && lastSelectedObject != theObject.transform.gameObject)
                {
                    RecoverShader();
                }

                lastSelectedObject = theObject.transform.gameObject;
                // Debug.Log("Sight on " + selectedObject);
                if (theObject.transform.gameObject.GetComponent<Selected>() != null)
                    theObject.transform.gameObject.GetComponent<Selected>().ApplyOutline();
                if (theObject.transform.gameObject.GetComponent<SelectLadder>() != null)
                    theObject.transform.gameObject.GetComponent<SelectLadder>().ApplyOutline();
                instructionText.GetComponent<Text>().text = "Press R to collect";
                instructionText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.R))
                {
                    instructionText.GetComponent<Text>().text = "";
                    instructionText.SetActive(false);
                    if (theObject.transform.gameObject.GetComponent<Selected>() != null)
                        theObject.transform.gameObject.GetComponent<Selected>().Collect();
                    if (theObject.transform.gameObject.GetComponent<SelectLadder>() != null)
                        theObject.transform.gameObject.GetComponent<SelectLadder>().Collect();
                    theObject.transform.gameObject.SetActive(false);
                }
            }
            else if (theObject.distance < distance && theObject.transform.gameObject.tag == "ObjectGrid")
            {
                if (lastSelectedObject != null && lastSelectedObject != theObject.transform.gameObject)
                {
                    RecoverShader();
                }

                lastSelectedObject = theObject.transform.gameObject;
                // Debug.Log("Sight on " + selectedObject);
                if (theObject.transform.gameObject.GetComponent<GridSelected>() != null)
                    theObject.transform.gameObject.GetComponent<GridSelected>().ApplyOutline();


            }
            else
            {
                RecoverShader();
                if (!InstructionController.underInstruction)
                {
                    instructionText.GetComponent<Text>().text = "";
                    instructionText.SetActive(false);
                }
                lastSelectedObject = null;
            }
        }
    }

    void RecoverShader()
    {
        if (lastSelectedObject != null && lastSelectedObject.GetComponent<Selected>() != null)
            lastSelectedObject.GetComponent<Selected>().ApplyOriginalShader();
        if (lastSelectedObject != null && lastSelectedObject.GetComponent<SelectLadder>() != null)
            lastSelectedObject.GetComponent<SelectLadder>().ApplyOriginalShader();
        if (lastSelectedObject != null && lastSelectedObject.GetComponent<GridSelected>() != null)
            lastSelectedObject.GetComponent<GridSelected>().ApplyOriginalShader();
    }

}
