using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject dialogText;

    private float curtime = float.MaxValue;
    private float delayTime = 0;

    public static bool dialogActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DialogTrigger")
        {
            DialogContentContainer dcc = other.gameObject.GetComponent<DialogContentContainer>();
            dialogText.GetComponent<Text>().text = dcc.content;
            delayTime = dcc.delayTime;
            curtime = Time.time;
            other.gameObject.SetActive(false);
            dialogActive = true;
            if (other.gameObject.GetComponent<DialogContentContainer>().oneTime)
            { 
                Destroy(other.gameObject);
            }

        }
    }

    private void Update()
    {
        if (dialogActive && Time.time - curtime > delayTime)
        {
            dialogText.SetActive(true);
            curtime = float.MaxValue;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            dialogActive = false;
        }
        if (!dialogActive)
        {
            dialogText.SetActive(false);
        }
    }
}
