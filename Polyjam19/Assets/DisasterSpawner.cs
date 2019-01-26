using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterSpawner : MonoBehaviour
{
    public GameObject disasterPrefab;
    Disaster disaster;
    [HideInInspector]
    public Apartment apartment;
    public float disasterChancePerSecond = 0.05f;
    float timer = 0f;
    public float signalingTime = 3f;
    [HideInInspector]
    public bool signaling = false;
    public Sprite signalingSprite;
    Sprite idleSprite;
    SpriteRenderer spriteRenderer;
    Coroutine signalingCoroutine;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        idleSprite = spriteRenderer.sprite;
    }

    void Update()
    {
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
        spriteRenderer.sprite = signalingSprite;

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
        spriteRenderer.sprite = idleSprite;
        signaling = false;

    }
}
