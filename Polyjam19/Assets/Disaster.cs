using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disaster : MonoBehaviour
{
    public Item.Type requiredItemType;
    [HideInInspector]
    public Apartment apartment;


    public void Interact (Item item)
    {
        if (item == null)
            return;
        if (item.type != requiredItemType)
            return;

        Destroy(gameObject);
    }
}
