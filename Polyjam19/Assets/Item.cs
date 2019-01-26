using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Disaster.Type counteredDisasterType;
    [HideInInspector]
    public Apartment currentApartment;
    [HideInInspector]
    public InsideScaler insideScaler;

    void Start()
    {
        insideScaler = GetComponent<InsideScaler>();
    }
    
}
