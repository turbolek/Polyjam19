using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disaster : MonoBehaviour
{
    public Type type;
    [HideInInspector]
    public Apartment apartment;
    public float growRate = 0.05f;
    DisasterSpawner spawner;
    public float level = 0f;
    float timer = 0f;

    public enum Type
    {
        Fire = 1,
        Water = 2,
        Rat = 3
    }

    public void Interact(Item item)
    {
        if (item == null)
            return;
        if (item.counteredDisasterType != type)
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

    void Update()
    {
        level = Mathf.Clamp(growRate * timer, 0f, 1f);
        timer += Time.deltaTime;
        transform.localScale = Vector3.one + new Vector3(level, level, 0f);
    }
}
