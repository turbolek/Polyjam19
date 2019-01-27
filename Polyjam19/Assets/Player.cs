using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Player : MonoBehaviour
{
    public float leftBorder;
    public float rightBorder;
    public GameObject itemSlot;
    [HideInInspector]
    public Item currentItem;
    [HideInInspector]
    public Apartment currentApartment;
    public bool isBusy = false;
    Animator animator;

    List<DisasterSpawner> activeDisasterSpawners = new List<DisasterSpawner>();
    Disaster activeDisaster;
    List<Item> activeItems = new List<Item>();

    [HideInInspector]
    public InsideScaler insideScaler;
    BaseDoor activeDoor;

    public float speed = 2f;

    public void Init()
    {
        insideScaler = GetComponent<InsideScaler>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isBusy)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (activeDisaster != null && activeDisaster.apartment == currentApartment)
            {
                activeDisaster.Interact(this, currentItem);
                return;
            }

            if (activeDisasterSpawners.Count > 0)
            {
                DisasterSpawner spawner = activeDisasterSpawners.Find(ds => ds.signaling && ds.apartment == currentApartment);
                if (spawner != null)
                {
                    spawner.Reset();
                    return;
                }
            }

            if (activeDoor != null)
            {
                activeDoor.Enter(this);
                return;
            }

            if (activeItems.Count > 0 && currentItem == null)
            {
                PickUpItem(activeItems[0]);
                return;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (activeDoor != null && currentApartment != null)
            {
                activeDoor.Exit(this);
                return;
            }

            if (currentItem != null)
            {
                DropItem(currentItem);
                return;
            }
        }

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
        animator.SetBool("walk", Mathf.Abs(distance) > 0f);
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
        if (item != null && !activeItems.Contains(item))
            activeItems.Add(item);

        Disaster disaster = collider.GetComponent<Disaster>();
        if (disaster != null)
            activeDisaster = disaster;

        DisasterSpawner disasterSpawner = collider.GetComponent<DisasterSpawner>();
        if (disasterSpawner != null && !activeDisasterSpawners.Contains(disasterSpawner))
            activeDisasterSpawners.Add(disasterSpawner);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        BaseDoor door = collider.GetComponent<BaseDoor>();
        if (door != null && door == activeDoor)
            activeDoor = null;

        Item item = collider.GetComponent<Item>();
        if (activeItems.Contains(item))
        {
            activeItems.Remove(item);
        }

        Disaster disaster = collider.GetComponent<Disaster>();
        if (disaster != null)
            activeDisaster = null;

        DisasterSpawner disasterSpawner = collider.GetComponent<DisasterSpawner>();
        if (activeDisasterSpawners.Contains(disasterSpawner))
        {
            activeDisasterSpawners.Remove(disasterSpawner);
        }
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
        item.transform.position = new Vector3(item.transform.position.x, currentApartment.floorTransform.position.y + item.yOffset * currentApartment.insideScale, currentApartment.floorTransform.position.z);
        item.currentApartment = currentApartment;
        item.insideScaler.ScaleToApartment(currentApartment);
    }

    public void FightDisaster(Disaster.Type disasterType)
    {
        isBusy = true;
        currentItem.StartUsing();
    }

    public void Idle()
    {
        isBusy = false;
        currentItem.StopUsing();
    }
}
