using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float leftBorder;
    public float rightBorder;
    Door activeDoor;

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
                EnterApartment(activeDoor.apartment);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            ExitApartment();
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
        Door door = collider.GetComponent<Door>();
        if (door != null)
            activeDoor = door;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("trigger exit");
        Door door = collider.GetComponent<Door>();
        if (door != null)
            activeDoor = null;
    }

    void EnterApartment(Apartment apartment)
    {
        transform.localScale = new Vector3(0.75f, 0.75f, 1f);
        leftBorder = apartment.leftBorder;
        rightBorder = apartment.rightBorder;
    }

    void ExitApartment()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        leftBorder = -1.5f;
        rightBorder = 4.5f;
    }
}
