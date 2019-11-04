using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Image menuMask;
    public Image gameMask;
    public float transformPeriod = 1;
    public GameObject gameCanvas;
    
    public GameObject[] objs = new GameObject[1];

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc;

    private bool onStart = false;
    private float ratio = 0;
    private float passedPeriod = 0;
    private float startTime = -1.0f;
    //private MenuController mainMenu;
    private GameObject pauseCanvas;

    private void Start()
    {
        //mainMenu = GameObject.Find("MenuCanvas").GetComponent<MenuController>();
    }

    public void GameStart()
    {
        //onStart = true;
        SettingObject();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        pauseCanvas = GameObject.Find("PauseCanvas");
        pauseCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        fpc.SetIsRotate();
        fpc.SetControllable(true);
    }

    public void ReturnMain()
    {
        SceneManager.LoadScene("test");
    }

    private void Update()
    {
        if (onStart)
        {
            if (startTime < 0)
            {
                startTime = Time.time;
            }
            ratio = (Time.time - startTime) / transformPeriod;
            if (ratio >= 1)
            {
                //TODO - respawn, camera setting, canvas setting
            }
            else
            {
                menuMask.color = new Color(1, 1, 1, ratio);
            }

        }
    }

    private void SettingObject()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            if ( objs[i].activeSelf)
            {
                objs[i].SetActive(false);
            }
            else
            {
                objs[i].SetActive(true);
            }
        }
    }
}
