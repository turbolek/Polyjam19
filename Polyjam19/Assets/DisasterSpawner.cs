using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterSpawner : MonoBehaviour
{
    public GameObject disasterPrefab;
    Disaster disaster;
    Apartment apartment;
    public float disasterChancePerSecond = 0.05f;
    float timer = 0f;
    public float signalingTime = 3f;
    bool signaling = false;
    public Sprite signalingSprite;
    Sprite idleSprite;
    SpriteRenderer spriteRenderer;

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
                StartCoroutine(SignalDisaster());
            }
        }
        else
            timer += Time.deltaTime;
    }

    public void SetApartment(Apartment ap)
    {
        apartment = ap;
    }

    void SpawnDisaster()
    {
        GameObject disasterGO = Instantiate(disasterPrefab);
        disaster = disasterGO.GetComponent<Disaster>();
        disasterGO.transform.position = transform.position;
        disaster.apartment = apartment;
        disaster.SetSpawner(this);
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
        timer = 0f;
        spriteRenderer.sprite = idleSprite;
    }
}
