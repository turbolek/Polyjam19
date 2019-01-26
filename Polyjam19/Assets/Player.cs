using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float leftBorder;
    public float rightBorder;
    public GameObject itemSlot;
    [HideInInspector]
    public Item currentItem;
    [HideInInspector]
    public Apartment currentApartment;
    Item activeItem;
    [HideInInspector]
    public InsideScaler insideScaler;
    BaseDoor activeDoor;

    public float speed = 2f;

    void Start()
    {
        insideScaler = GetComponent<InsideScaler>();
    }

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
            {
                activeDoor.Enter(this);
            }
            else if (activeItem != null)
            {
                PickUpItem(activeItem);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (activeDoor != null && currentApartment != null)
            {
                activeDoor.Exit(this);
            }
            else if (currentItem != null)
            {
                DropItem(currentItem);
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

        Item item = collider.GetComponent<Item>();
        if (item != null)
            activeItem = item;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        BaseDoor door = collider.GetComponent<BaseDoor>();
        if (door != null)
            activeDoor = null;

        Item item = collider.GetComponent<Item>();
        if (item != null)
        {
            activeItem = null;
        }
    }
    
    void PickUpItem(Item item)
    {
        currentItem = item;
        item.transform.parent = itemSlot.transform;
        item.transform.position = itemSlot.transform.position;
    }

    void DropItem(Item item)
    {
        currentItem = null;
        item.transform.parent = null;
        item.transform.position = new Vector3(item.transform.position.x, transform.position.y, item.transform.position.z);
        item.currentApartment = currentApartment;
        item.insideScaler.ScaleToApartment(currentApartment);
    }
}
