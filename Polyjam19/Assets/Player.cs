using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float leftBorder;
    public float rightBorder;
    BaseDoor activeDoor;

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

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (activeDoor != null)
                activeDoor.Enter(this);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (activeDoor != null)
            {
                activeDoor.Exit(this);
            }
        }

        Move(distance * directionValue);
    }

    void Move(float distance)
    {
        float newPosition = transform.position.x + distance;
        if (newPosition > leftBorder && newPosition < rightBorder)
            transform.Translate(new Vector3(distance, 0f, 0f));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("trigger entered");
        BaseDoor door = collider.GetComponent<BaseDoor>();
        if (door != null)
            activeDoor = door;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("trigger exit");
        BaseDoor door = collider.GetComponent<BaseDoor>();
        if (door != null)
            activeDoor = null;
    }
}

    
