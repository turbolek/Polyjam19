using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterSpawner : MonoBehaviour
{
    public GameObject disasterPrefab;
    Disaster disaster;
    [HideInInspector]
    public Apartment apartment;
    float breakChancePerSecond = 0.015f;
    float disasterChancePerSecond = 0.005f;
    float timer = 0f;
    [HideInInspector]
    public bool signaling = false;
    public GameObject signalObject;
    SpriteRenderer spriteRenderer;
    Coroutine signalingCoroutine;
    [HideInInspector]
    public bool broken = false;

    void Start()
    {
        Reset();
    }

    public void Fix(Player player)
    {
        broken = false;
        signalObject.SetActive(false);
        player.FixSpawner();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (disaster != null || (!broken && apartment.HasBrokenSpawner()))
            return;

        if (timer >= 1f)
        {
            timer = 0f;
            RollDice();
        }
    }

    public void SetApartment(Apartment ap)
    {
        apartment = ap;
    }

    void Break()
    {
        broken = true;
        signalObject.SetActive(true);
    }

    public void SpawnDisaster()
    {
        GameObject disasterGO = Instantiate(disasterPrefab);
        disaster = disasterGO.GetComponent<Disaster>();
        disasterGO.transform.position = new Vector3(apartment.floorTransform.position.x, apartment.floorTransform.position.y, apartment.floorTransform.position.z + 0.1f);
        disaster.SetSpawner(this);
        apartment.SetDisaster(disaster);

        switch (disaster.type)
        {
            case Disaster.Type.Fire:
                GameManager.fireCount++;
                break;
            case Disaster.Type.Water:
                GameManager.waterCount++;
                break;
            case Disaster.Type.Rat:
                GameManager.cockroachCount++;
                break;


        }
    }

    void RollDice()
    {
        float diceRoll = Random.value;
        if (broken && diceRoll < disasterChancePerSecond)
        {
            SpawnDisaster();
        }
        if (!broken && diceRoll < breakChancePerSecond)
            Break();
    }

    public void Reset()
    {
        if (signalingCoroutine != null)
            StopCoroutine(signalingCoroutine);
        timer = 0f;
        signalObject.SetActive(false);
        signaling = false;

    }
}
