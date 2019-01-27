using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apartment : MonoBehaviour
{
    public float leftBorder;
    public float rightBorder;
    public float insideScale = .75f;
    public Transform floorTransform;
    public Transform doorTransform;
    public DisasterSpawner fireSpawner;
    public DisasterSpawner waterSpawner;
    public DisasterSpawner ratSpawner;
    Disaster disaster;
    float disasterLevel = 0f;

    [SerializeField]

    void Start()
    {
        if (fireSpawner != null)
            fireSpawner.SetApartment(this);
        if (fireSpawner != null)
            waterSpawner.SetApartment(this);
        if (fireSpawner != null)
            ratSpawner.SetApartment(this);
    }

    public void Enter(Player player)
    {
        player.currentApartment = this;
        player.insideScaler.ScaleToApartment(this);
        player.leftBorder = leftBorder;
        player.rightBorder = rightBorder;
        player.transform.position = new Vector3(doorTransform.position.x, floorTransform.position.y, floorTransform.position.z);
    }

    public void SetDisaster(Disaster dis)
    {
        disaster = dis;
        dis.apartment = this;
    }

    void Update()
    {
        if (disaster != null && disaster.level >= 1f)
        {
            InfectNeighbour();
        }
    }

    void InfectNeighbour()
    {
        Apartment neighbour = null;
        switch (disaster.type)
        {
            case Disaster.Type.Fire:
                neighbour = GameManager.buildingGenerator.GetUpperNeighbour(this);

                break;
            case Disaster.Type.Water:
                break;
            case Disaster.Type.Rat:
                break;
        }

        if (neighbour != null)
        {
            neighbour.ForceDisaster(disaster.type);
        }
    }

    public void ForceDisaster(Disaster.Type disasterType)
    {
        if (disaster != null)
            return;

        switch (disasterType)
        {
            case Disaster.Type.Fire:
                fireSpawner.SpawnDisaster();
                break;
            case Disaster.Type.Water:
                waterSpawner.SpawnDisaster();
                break;
            case Disaster.Type.Rat:
                ratSpawner.SpawnDisaster();
                break;
        }
    }
}
