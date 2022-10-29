using System;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private GameObject[] healthBars;

    private void Awake()
    {
        if (!GameState.IsSinglePlayer)
        {
            for (int i = 0; i < healthBars.Length; i++)
            {
                healthBars[i].SetActive(i == healthBars.Length - 1);
            }
        }
        else
        {
            for (int i = 0; i < healthBars.Length; i++)
            {
                healthBars[i].SetActive(i == GameState.CurrentCharacterIndex);
            }
        }
    }
    
}