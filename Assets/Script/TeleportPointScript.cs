using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPointScript : MonoBehaviour
{
    public bool SwitchEnviLight;
    public bool NeedButton;
    public KeyCode TheButton;
    public Vector3 Target;
    private bool IsTrigger;


    // Start is called before the first frame update
    void Start()
    {
        IsTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)//进点传送
    {
        if (other.tag == "player")
        {
            IsTrigger = true;
        }
        if (!NeedButton)
        {
            other.GetComponent<teleportScript>().SetMove(Target);

            AudioSource AS = this.gameObject.GetComponent<AudioSource>();
            if (AS)
            {
                AS.Play();
            }
            if (SwitchEnviLight)
            {
                other.GetComponent<teleportScript>().TurnOnOffEnviLight();
                //EnviLight.SetActive(!EnviLight.activeSelf);
            }
        }
    }

    private void OnTriggerStay(Collider other)//按键传送
    {

        if (NeedButton && IsTrigger && Input.GetKeyDown(TheButton))
        {
            other.GetComponent<teleportScript>().SetMove(Target);

            AudioSource AS = this.gameObject.GetComponent<AudioSource>();
            if (AS)
            {
                AS.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            IsTrigger = false;
        }
    }
}
