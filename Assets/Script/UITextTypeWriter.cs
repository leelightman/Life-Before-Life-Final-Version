using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class UITextTypeWriter : MonoBehaviour 
{
    public float TextDelayTime;
    private float CurDelayTime;

    Text txt;
    string story;

    void Awake()
    {
        txt = GetComponent<Text>();
        story = txt.text;
        txt.text = "";

        //StartCoroutine("PlayText");

        CurDelayTime = 0f;
    }

    /*
    IEnumerator Delay()
    {
        Debug.Log("wait 2 sec");
        yield return new WaitForSeconds(5f);
    }
    */

    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.4f);
        }
    }

    private void Update()
    {
        if (CurDelayTime < TextDelayTime)
        {
            CurDelayTime += Time.deltaTime;
            if (CurDelayTime >= TextDelayTime)
            {
                StartCoroutine("PlayText");
            }
        }
    }
}