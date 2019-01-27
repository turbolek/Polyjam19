using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterSpawner : MonoBehaviour
{
    public GameObject disasterPrefab;
    Disaster disaster;
    [HideInInspector]
    public Apartment apartment;
    float disasterChancePerSecond = 0.01f;
    float timer = 0f;
    float signalingTime = 30f;
    [HideInInspector]
    public bool signaling = false;
    public GameObject signalObject;
    SpriteRenderer spriteRenderer;
    Coroutine signalingCoroutine;

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if (!signaling && (apartment.waterSpawner.signaling || apartment.ratSpawner.signaling || apartment.fireSpawner.signaling))
        {
            Reset();
            return;
        }

        if (disaster != null || signaling)
            return;


        if (timer > 1f)
        {
            timer = 0f;
            float diceRoll = Random.value;
            if (diceRoll < disasterChancePerSecond)
            {
                signalingCoroutine = StartCoroutine(SignalDisaster());
            }
        }
        else
            timer += Time.deltaTime;
    }

    public void SetApartment(Apartment ap)
    {
        apartment = ap;
    }

    public void SpawnDisaster()
    {
        GameObject disasterGO = Instantiate(disasterPrefab);
        disaster = disasterGO.GetComponent<Disaster>();
        disasterGO.transform.position = new Vector3(apartment.floorTransform.position.x, apartment.floorTransform.position.y, apartment.floorTransform.position.z + 0.1f);
        disaster.SetSpawner(this);
        apartment.SetDisaster(disaster);
    }

    IEnumerator SignalDisaster()
    {
        signaling = true;
        signalObject.SetActive(true); ;

        while (signaling && timer < signalingTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        SpawnDisaster();
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
