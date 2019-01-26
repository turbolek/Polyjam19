using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disaster : MonoBehaviour
{
    public Item.Type requiredItemType;
    public Apartment apartment;
    DisasterSpawner spawner;


    public void Interact (Item item)
    {
        if (item == null)
            return;
        if (item.type != requiredItemType)
            return;
        FixDisaster();
        
    }

    void FixDisaster()
    {
        spawner.Reset();
        Destroy(gameObject);
    }

    public void SetSpawner(DisasterSpawner sp)
    {
        spawner = sp;
    }
}
