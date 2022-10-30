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
            if (position.x > LeftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, position.y,
                    position.z);
                transform.localScale = new Vector3(-1 * CurrentScale.x, CurrentScale.y, 1);
                Animator.SetBool(Run, true);
            }
            else
            {
                startMovementFromRightToLeft = false;
                transform.localScale = CurrentScale;
            }
        }
        else
        {
            if (position.x < RightEdge)
            {
                transform.position = new Vector3(position.x + speed * Time.deltaTime, position.y, position.z);
                transform.localScale = CurrentScale;
                Animator.SetBool(Run, true);
            }
            else
            {
                startMovementFromRightToLeft = true;
                transform.localScale = new Vector3(-1 * CurrentScale.x, CurrentScale.y, 1);
            }
        }
    }
}