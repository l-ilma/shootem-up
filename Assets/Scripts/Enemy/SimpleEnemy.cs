using UnityEngine;

public class SimpleEnemy : Enemy
{
    [SerializeField] private bool startMovementFromRightToLeft;

    protected new void Update()
    {
        base.Update();
        if (maxMovingDistance > 0) Move();
    }

    private void Move()
    {
        var position = transform.position;
        if (startMovementFromRightToLeft)
        {
            if (position.x > _leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, position.y,
                    position.z);
                transform.localScale = new Vector3(-1 * _currentScale.x, _currentScale.y, 1);
                _animator.SetBool(Run, true);
            }
            else
            {
                startMovementFromRightToLeft = false;
                transform.localScale = _currentScale;
            }
        }
        else
        {
            if (position.x < _rightEdge)
            {
                transform.position = new Vector3(position.x + speed * Time.deltaTime, position.y, position.z);
                transform.localScale = _currentScale;
                _animator.SetBool(Run, true);
            }
            else
            {
                startMovementFromRightToLeft = true;
                transform.localScale = new Vector3(-1 * _currentScale.x, _currentScale.y, 1);
            }
        }
    }
}