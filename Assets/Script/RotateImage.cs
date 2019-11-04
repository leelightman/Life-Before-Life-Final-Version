using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateImage : MonoBehaviour
{
    private RectTransform rt;
    public float speed = 30;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }


    void Update()
    {
        rt.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime);
    }
}
