using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : Enemy
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackRange = 4f;
    
    private const float AttackCooldown = 3f;
    private bool _canAttack;
    private float _attackTimeElapsed = Mathf.Infinity;
    private Vector3 _attackDestination;
    private bool _isAttacking;

    private new void Awake()
    {
        base.Awake();
        _isAttacking = false;
    }

    private new void Update()
    {
        base.Update();
        _attackTimeElapsed += Time.deltaTime;

        if (_isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, _attackDestination, 
                speed * Time.deltaTime);
            if (Vector3.Distance(transform.position,  _attackDestination) < 0.001f)
            {
                Stop();
            }
        }
        else
        {
            if (_attackTimeElapsed > AttackCooldown)
            {
                FindPlayer();
            }
        }
    }

    private void FindPlayer()
    {
        var directions = GetScanningDirections();
        foreach (var direction in directions)
        {
            var position = transform.position;
            Debug.DrawRay(position, direction, Color.red);
            var hit = Physics2D.Raycast(position, direction, 
                attackRange, playerLayer);
            
            if (hit.collider != null && !_isAttacking)
            {
                var playerPosition = hit.transform.position;
                SetScale(playerPosition);
                
                _isAttacking = true;
                Animator.SetBool(Run, true);
                var destinationX = Mathf.Clamp(playerPosition.x, LeftEdge, RightEdge);
                _attackDestination = new Vector3(destinationX, transform.position.y, transform.position.z);
                _attackTimeElapsed = 0;
            }
        }
    }

    private List<Vector3> GetScanningDirections()
    {
        return new List<Vector3>(){transform.right * attackRange, -transform.right * attackRange};
    }

    private void Stop()
    {
        _isAttacking = false;
        Animator.SetBool(Run, false);
    }

    private void SetScale(Vector3 playerPosition)
    {
        float x;
        if (playerPosition.x > transform.position.x) // player on the right side
        {
            x = Mathf.Sign(CurrentScale.x) == 1 ? CurrentScale.x : -1 * CurrentScale.x;
        }
        else // player on the left side
        {
            x = Mathf.Sign(CurrentScale.x) == 1 ? -1 * CurrentScale.x : CurrentScale.x;
        }
        CurrentScale = new Vector3(x, CurrentScale.y, CurrentScale.z);
        transform.localScale = CurrentScale;
    }

    protected new void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
        
        if (_isAttacking)
        {
            Stop();
        }
    }
}