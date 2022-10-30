using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deadSound;

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
            SoundManager.Instance.PlaySound(hitSound);
        }
        else
        {
            if (dead) return;

            _animator.SetTrigger(Die);
            SoundManager.Instance.PlaySound(deadSound);
            GetComponent<PlayerMovement>().enabled = false;
            dead = true;
            if (GameState.IsSinglePlayer)
            {
                _uiManager.GameOver();
            }
            else
            {
                // flag to know that game was multiplayer prior to one player dying
                // required when restarting the game
                GameState.WasMultiplayer = true; 
                GameState.IsSinglePlayer = true;
                GameState.CurrentCharacterIndex = GameState.CurrentCharacterIndex == 0 ? 1 : 0;
            }
        }
    }
}