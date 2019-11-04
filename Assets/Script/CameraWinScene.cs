using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWinScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < 65)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 20, Space.World);

            transform.Translate(Vector3.up * Time.deltaTime * 1.4f, Space.World);
        }
    }
}
