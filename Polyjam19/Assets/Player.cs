using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float leftBorder;
    public float rightBorder;

    public float speed = 2f;


    void Update()
    {
        float deltaTime = Time.deltaTime;
        float distance = speed * deltaTime;
        float directionValue = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            directionValue = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            directionValue = 1f;
        }

        Move(distance * directionValue);
    }

    void Move(float distance)
    {
        float newPosition = transform.position.x + distance;
        if (newPosition > leftBorder && newPosition < rightBorder)
            transform.Translate(new Vector3(distance, 0f, 0f));
    }
}
