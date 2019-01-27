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
    public float yOffset = 0f;
    AudioSource audioSource;

    void Start()
    {
        insideScaler = GetComponent<InsideScaler>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void StartUsing()
    {
        animator.SetBool("working", true);
        audioSource.Play();
    }

    public void StopUsing()
    {
        animator.SetBool("working", false);
        audioSource.Stop();
    }
    
}
