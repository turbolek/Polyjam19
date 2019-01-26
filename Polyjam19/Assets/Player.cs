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

    Disaster activeDisaster;
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
            else if (activeItem != null && currentItem == null)
                PickUpItem(activeItem);
            else if (activeDisaster != null)
                activeDisaster.Interact(currentItem);
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

        Disaster disaster = collider.GetComponent<Disaster>();
        if (disaster != null)
            activeDisaster = disaster;
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

        Disaster disaster = collider.GetComponent<Disaster>();
        if (disaster != null)
            activeDisaster = null;
    }

    void EnterApartment(Apartment apartment)
    {
        currentApartment = apartment;
        insideScaler.ScaleToApartment(apartment);
        leftBorder = apartment.leftBorder;
        rightBorder = apartment.rightBorder;
        transform.position = new Vector3(transform.position.x, apartment.floorTransform.position.y, apartment.floorTransform.position.z);
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
        item.transform.position = new Vector3(item.transform.position.x, currentApartment.floorTransform.position.y, currentApartment.floorTransform.position.z);
        item.currentApartment = currentApartment;
        item.insideScaler.ScaleToApartment(currentApartment);
    }
}
