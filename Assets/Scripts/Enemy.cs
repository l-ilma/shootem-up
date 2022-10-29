using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movingDistance;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float startingHealth;

    private Animator _animator;
    private bool _movingLeftOrUp;
    private float _leftOrTopEdge;
    private float _rightOrBottomEdge;
    private Vector3 _currentScale;
    private float _currentHealth;
    private readonly float _hurtCooldown = 0.2f;
    private float _timeElapsed = Mathf.Infinity;

    private static readonly int Run = Animator.StringToHash("run");

    protected void Awake()
    {
        _currentScale = transform.localScale;
        _currentHealth = startingHealth;
        var position = transform.position;
        _leftOrTopEdge = position.x - movingDistance;
        _rightOrBottomEdge = position.x + movingDistance;
        _animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        if (movingDistance > 0) Move();

        if (_timeElapsed >= _hurtCooldown)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        _timeElapsed += Time.deltaTime;
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
            _timeElapsed = 0;
        }
    }

    private void Move()
    {
        var position = transform.position;
        if (_movingLeftOrUp)
        {
            if (position.x > _leftOrTopEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, position.y,
                    position.z);
                transform.localScale = new Vector3(-1 * _currentScale.x, _currentScale.y, 1);
                _animator.SetBool(Run, true);
            }
            else
            {
                _movingLeftOrUp = false;
                transform.localScale = _currentScale;
            }
        }
        else
        {
            if (position.x < _rightOrBottomEdge)
            {
                transform.position = new Vector3(position.x + speed * Time.deltaTime, position.y, position.z);
                transform.localScale = _currentScale;
                _animator.SetBool(Run, true);
            }
            else
            {
                _movingLeftOrUp = true;
                transform.localScale = new Vector3(-1 * _currentScale.x, _currentScale.y, 1);
            }
        }
    }
}