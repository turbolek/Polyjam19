using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float leftBorder;
    public float rightBorder;
    public GameObject itemSlot;
    public Item currentItem;
    Door activeDoor;
    Item activeItem;

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
            else if (activeItem != null)
                PickUpItem(activeItem);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (activeDoor != null)
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
        Door door = collider.GetComponent<Door>();
        if (door != null)
            activeDoor = door;

        Item item = collider.GetComponent<Item>();
        if (item != null)
            activeItem = item;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Door door = collider.GetComponent<Door>();
        if (door != null)
            activeDoor = null;

        Item item = collider.GetComponent<Item>();
        if (door != null)
            activeItem = null;
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
        rightBorder = 7.5f;
    }

    void PickUpItem(Item item)
    {
        currentItem = item;
        item.transform.parent = itemSlot.transform;
        item.transform.position = itemSlot.transform.position;
    }
}
