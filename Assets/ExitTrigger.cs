using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private void OnMouseDown()
    {
        //Debug.Log("quit");
        Application.Quit();
    }
}
