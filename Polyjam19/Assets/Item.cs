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
    Animator animator;

    void Start()
    {
        insideScaler = GetComponent<InsideScaler>();
        animator = GetComponent<Animator>();
    }

    public void StartUsing()
    {
        animator.SetBool("working", true);
    }

    public void StopUsing()
    {
        animator.SetBool("working", false);
    }
    
}
