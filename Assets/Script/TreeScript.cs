using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public Vector3 Target;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "player")
        {

            if (Input.GetKeyDown(KeyCode.C))
            {

                other.GetComponent<teleportScript>().SetMove(Target);
                //Debug.Log(other.GetComponent<teleportScript>());
                AudioSource AS = this.gameObject.GetComponent<AudioSource>();
                if (AS)
                {
                    AS.Play();
                }
            }
        }
    }
}
