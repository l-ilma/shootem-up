using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode jump;
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D _body;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;

    private static readonly int Walk = Animator.StringToHash("walk");
    private static readonly int Grounded = Animator.StringToHash("grounded");
    private static readonly int Airborne = Animator.StringToHash("jump");

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Move();
        
        if (GameState.IsSinglePlayer)
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && IsGrounded())
            {
                Jump();
            }
        }
        else
        {
            if (Input.GetKey(jump) && IsGrounded())
            {
                Jump();
            }
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (GameState.IsSinglePlayer)
        {
           MoveSinglePlayer(horizontalInput);
        }
        else
        {
            MoveMultiplayer(horizontalInput);
        }
        _animator.SetBool(Grounded, IsGrounded());
    }

    private void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, jumpSpeed);
        _animator.SetTrigger(Airborne);
        SoundManager.Instance.PlaySound(jumpSound);
    }

    private bool IsGrounded()
    {
        var bounds = _boxCollider2D.bounds;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bounds.center, bounds.size, 0,
            Vector2.down, 0.03f, groundLayer);
        return raycastHit2D.collider != null;
    }

    private bool OnWall()
    {
        var bounds = _boxCollider2D.bounds;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bounds.center, bounds.size, 0,
            new Vector2(_body.transform.localScale.x, 0), 0.01f, wallLayer);
        return raycastHit2D.collider != null;
    }

    private void MoveSinglePlayer(float horizontalInput)
    {
        _body.velocity = new Vector2(horizontalInput * speed, _body.velocity.y);
        if (horizontalInput > 0.01f)
        {
            _body.transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            _body.transform.localScale = new Vector3(-1, 1, 1);
        }
        _animator.SetBool(Walk, horizontalInput != 0);
    }

    private void MoveMultiplayer(float horizontalInput)
    {
        if (Input.GetKey(left))
        {
            _body.transform.localScale = new Vector3(-1, 1, 1);
            _animator.SetBool(Walk, true);
            _body.velocity = new Vector2(horizontalInput * speed, _body.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            _body.transform.localScale = Vector3.one;
            _animator.SetBool(Walk, true);
            _body.velocity = new Vector2(horizontalInput * speed, _body.velocity.y);
        }
        else
        {
            _body.velocity = new Vector2(0, _body.velocity.y);
            _animator.SetBool(Walk, false);
        }
    }

    public bool CanAttack()
    {
        return !OnWall();
    }
}