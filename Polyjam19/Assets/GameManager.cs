using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static BuildingGenerator buildingGenerator;
    public GameObject playerPrefab;
    public GameObject[] itemPrefabs;
    public float timeLimit = 180;
    private static float startHealth = 0;

    public static float healthPoints = 100;
    public static float HealthPoints
    {
        get { return healthPoints; }
        set
        {
            healthPoints = value;
            if (onHealtchUpdate != null)
            {
                onHealtchUpdate.Invoke(healthPoints, startHealth);
            }
        }
    }
    public Text healthLabel;
    public Text timeLabel;

    public static UnityAction<float, float> onHealtchUpdate = null;



    void Awake()
    {
        buildingGenerator = FindObjectOfType<BuildingGenerator>();
    }

    private void Start()
    {
        startHealth = healthPoints;

        if (onHealtchUpdate != null)
        {
            onHealtchUpdate.Invoke(healthPoints, startHealth);
        }
    }

    void Update()
    {
        int healthInt = (int)Mathf.Ceil(healthPoints);
        healthLabel.text = healthInt.ToString();
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
