using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static BuildingGenerator buildingGenerator;
    void Start()
    {
        buildingGenerator = FindObjectOfType<BuildingGenerator>();
    }
}
