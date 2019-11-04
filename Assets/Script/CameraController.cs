using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10;
    public float period = 6.0f;

    private float startTime;
    public Vector3[] positionArray = new Vector3[5];
    public Vector3[] rotationArray = new Vector3[5];
    private int index;

    private void Start()
    {
        index = 0;
    }

    private void OnEnable()
    {
        startTime = Time.time;
        transform.position = positionArray[0];
        transform.rotation = Quaternion.Euler(rotationArray[0]);
    }

    void Update()
    {
        if (index == 0)
        {
            transform.RotateAround(transform.position, new Vector3(0, 1, 0), -speed * Time.deltaTime);
        }
        if (index == 1)
        {
            transform.RotateAround(new Vector3(70, 1, 45.3f), new Vector3(0, 1, 0), speed * Time.deltaTime);
        }
        if (index == 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.15f * speed * Time.deltaTime, transform.position.z);
        }
        if (index == 3)
        {
            transform.RotateAround(transform.position, new Vector3(0, 1, 0), speed * Time.deltaTime);
        }
        if (index == 4)
        {
            transform.position = new Vector3(transform.position.x + 0.5f * speed * Time.deltaTime, transform.position.y , transform.position.z);
        }

        if (Time.time - startTime > period)
        {
            startTime = Time.time;
            changePosition();
        }

    }

    void changePosition()
    {
        index++;
        if(index == positionArray.Length)
        {
            index = 0;
        }
        transform.position = positionArray[index];
        transform.rotation = Quaternion.Euler(rotationArray[index]);
    }
}
