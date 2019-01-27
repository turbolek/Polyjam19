using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    private Scrollbar bar = null;

    private void OnEnable()
    {
        GameManager.onHealtchUpdate += UpdateHealthBar;
    }

    private void OnDisable()
    {
        GameManager.onHealtchUpdate += UpdateHealthBar;
    }

    private void UpdateHealthBar(float currentValue, float startValue)
    {
        bar.size = currentValue / startValue;
    }
}
