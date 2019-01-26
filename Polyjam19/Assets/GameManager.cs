using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static BuildingGenerator buildingGenerator;
    public GameObject playerPrefab;
    public GameObject[] itemPrefabs;
    public float timeLimit = 180;
    public static float healthPoints = 100;
    public Text healthLabel;
    public Text timeLabel;

    void Awake()
    {
        buildingGenerator = FindObjectOfType<BuildingGenerator>();
    }

    void Update()
    {
        int healthInt = (int)Mathf.Ceil(healthPoints);
        healthLabel.text = "HP: " + healthInt.ToString();
        int timeInt = (int)Mathf.Ceil(timeLimit);
        timeLabel.text = timeInt.ToString() + " s";
        timeLimit -= Time.deltaTime;

        if (healthPoints <= 0)
            GameOver(false);

        if (timeLimit <= 0)
            GameOver(true);

    }

    void GameOver(bool victory)
    {
        if (victory)
            Debug.Log("Victory");
        else
            Debug.Log("Game Over");
    }
}
