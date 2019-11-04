using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPointScript : MonoBehaviour
{
    public GameObject NextFinalPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            //Debug.Log("final");
            if (NextFinalPoint)
            {
                NextFinalPoint.SetActive(true);
                NextFinalPoint.GetComponent<AudioSource>().Play();
                //Debug.Log(NextFinalPoint.GetComponent<AudioSource>());
            }
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
