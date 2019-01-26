using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Type type;
    [HideInInspector]
    public Apartment currentApartment;
    [HideInInspector]
    public InsideScaler insideScaler;

    void Start()
    {
        insideScaler = GetComponent<InsideScaler>();
    }

    public enum Type
    {
        FireDistinguisher = 1,
        Wrench = 2,
        RatTrap = 3
    }


}
