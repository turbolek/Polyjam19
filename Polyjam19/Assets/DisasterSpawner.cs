using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterSpawner : MonoBehaviour
{
    public GameObject disasterPrefab;
    Disaster disaster;
    Apartment apartment;
    public float disasterChancePerSecond = 0.05f;
    float rollTimer = 0f;

    void Update()
    {
        if (disaster != null)
            return;

        if (rollTimer > 1f)
        {
            rollTimer = 0f;
            float diceRoll = Random.value;
            if (diceRoll < disasterChancePerSecond)
            {
                GameObject disasterGO = Instantiate(disasterPrefab);
                disaster = disasterGO.GetComponent<Disaster>();
                disasterGO.transform.position = transform.position;
                disaster.apartment = apartment;
            }
        }
        else
            rollTimer += Time.deltaTime;
    }

    public void SetApartment(Apartment ap)
    {
        apartment = ap;
    }
}
