using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float startingHealth;
    [SerializeField] protected float maxMovingDistance;
    
    protected Animator _animator;
    private float _currentHealth;
    protected float _leftEdge;
    protected float _rightEdge;
    private readonly float _hurtCooldown = 0.2f;
    private float _hurtTimeElapsed = Mathf.Infinity;
    protected Vector3 _currentScale;
    
    protected static readonly int Run = Animator.StringToHash("run");
    
    protected void Awake()
    {
        _currentScale = transform.localScale;
        _currentHealth = startingHealth;
        var position = transform.position;
        _leftEdge = position.x - maxMovingDistance;
        _rightEdge = position.x + maxMovingDistance;
        _animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        if (_hurtTimeElapsed >= _hurtCooldown)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        _hurtTimeElapsed += Time.deltaTime;
    }
    
    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

        if (col.collider.CompareTag("Fireball"))
        {
            TakeDamage(col.collider.GetComponent<Fireball>().Damage);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

        if (col.CompareTag("Fireball"))
        {
            TakeDamage(col.GetComponent<Fireball>().Damage);
        }
    }

    private void TakeDamage(float dmg)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - dmg, 0, startingHealth);

        if (_currentHealth == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            _hurtTimeElapsed = 0;
        }
    }
}