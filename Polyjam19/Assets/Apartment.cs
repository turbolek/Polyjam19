using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apartment : MonoBehaviour
{
    public float leftBorder;
    public float rightBorder;
    public float insideScale = .75f;
    public Transform floorTransform;
    public DisasterSpawner[] disasterSpawners;

    [SerializeField]

    void Start()
    {
        for (int i =0; i < disasterSpawners.Length; i++)
        {
            disasterSpawners[i].SetApartment(this);
        }
    }

    public void Enter(Player player)
    {
        player.currentApartment = this;
        player.insideScaler.ScaleToApartment(this);
        player.leftBorder = leftBorder;
        player.rightBorder = rightBorder;
        player.transform.position = new Vector3(transform.position.x, floorTransform.position.y, transform.position.z);
    }


}
