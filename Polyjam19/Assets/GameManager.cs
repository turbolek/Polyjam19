using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static BuildingGenerator buildingGenerator;
    public GameObject playerPrefab;
    public GameObject[] itemPrefabs;

    void Awake()
    {
        buildingGenerator = FindObjectOfType<BuildingGenerator>();
    }
}
