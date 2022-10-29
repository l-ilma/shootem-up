using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 7;
    [SerializeField] private float damage = 1;
    
    private bool _hit = false;
    private Animator _animator;
    private float _direction;
    private float _lifetime;
    private readonly float _maxLifeTime = 5f;
    private BoxCollider2D _boxCollider2D;
    
    private static readonly int Explode = Animator.StringToHash("explode");
    
    public float Damage { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        Damage = damage;
    }

    private void Update()
    {
        if (!_hit)
        {
            transform.Translate(speed * Time.deltaTime * _direction, 0, 0);
        }

        _lifetime += Time.deltaTime;
        if (_lifetime >= _maxLifeTime)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Fireball") || col.CompareTag("Finish")) return;
        _hit = true;
        _boxCollider2D.enabled = false;
        _animator.SetTrigger(Explode);
    }

    public void Activate(float dir)
    {
        _direction = dir;
        gameObject.SetActive(true);
        _boxCollider2D.enabled = true;
        _hit = false;
        _lifetime = 0;

        if (Mathf.Sign(transform.localScale.x) != _direction)
        {
            var localScale = transform.localScale;
            localScale = new Vector3(-localScale.x, localScale.y, localScale.y);
            transform.localScale = localScale;
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
