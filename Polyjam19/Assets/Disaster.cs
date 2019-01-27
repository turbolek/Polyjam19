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
    public float damagePerSecond = 0.1f;

    public enum Type
    {
        Fire = 1,
        Water = 2,
        Rat = 3
    }

    public void Interact(Player player, Item item)
    {
        if (item == null)
            return;
        if (item.counteredDisasterType != type)
            return;
        FixDisaster(player);

    }

    void FixDisaster(Player player)
    {
        StartCoroutine(FixingCoroutine(player));

    }

    IEnumerator FixingCoroutine(Player player)
    {

        growRate = -5f * growRate;
        player.FightDisaster(type);
        spawner.Reset();
        while (level > 0)
            yield return new WaitForSeconds(1f);
        player.Idle();
        Destroy(gameObject);
    }

    public void SetSpawner(DisasterSpawner sp)
    {
        spawner = sp;
    }

    void Update()
    {
        level = Mathf.Clamp(level + growRate * Time.deltaTime, 0f, 1f);
        transform.localScale = Vector3.one + new Vector3(level, level, 0f);
        GameManager.HealthPoints -= Time.deltaTime * damagePerSecond;
    }
}
