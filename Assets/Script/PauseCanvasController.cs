using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvasController : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject gameCanvas;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc;

    //private void Start()
    //{
    //    pauseMenu = GameObject.Find("PauseCanvas");
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            fpc.SetControllable(false);
        }
    }
}
