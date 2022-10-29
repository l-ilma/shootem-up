﻿using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    private static readonly int Die = Animator.StringToHash("die");
    private static readonly int Hurt = Animator.StringToHash("hurt");

    public float CurrentHealth { get; private set; }
    private Animator _animator;
    private UIManager _uiManager;
    private bool dead = false;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        _animator = GetComponent<Animator>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            _animator.SetTrigger(Hurt);
        }
        else
        {
            if (dead) return;

            _animator.SetTrigger(Die);
            GetComponent<PlayerMovement>().enabled = false;
            dead = true;
            if (GameState.IsSinglePlayer)
            {
                _uiManager.GameOver();
            }
            else
            {
                GameState.WasMultiplayer = true;
                GameState.IsSinglePlayer = true;
            }
        }
    }
}