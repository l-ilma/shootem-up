using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float movingDistance;
    [SerializeField] private MovingDirection movingDirection;
    [SerializeField] private Transform playerContainer;
    [SerializeField] private float speed;
    [SerializeField] private bool startMovementFromLeftOrUp;

    private bool _movingLeftOrUp;
    private float _leftOrUpEdge;
    private float _rightOrUpEdge;
    private int _coordinate;

    protected void Awake()
    {
        _movingLeftOrUp = startMovementFromLeftOrUp;
        var position = transform.position;
        _coordinate = movingDirection == MovingDirection.Horizontal ? 0 : 1;

        _leftOrUpEdge = position[_coordinate] - movingDistance;
        _rightOrUpEdge = position[_coordinate] + movingDistance;
    }

    protected void Update()
    {
        var position = transform.position;
        if (_movingLeftOrUp)
        {
            if (position[_coordinate] > _leftOrUpEdge)
            {
                transform.position = movingDirection == MovingDirection.Horizontal
                    ? new Vector3(transform.position.x - speed * Time.deltaTime, position.y, position.z)
                    : new Vector3(transform.position.x, position.y - speed * Time.deltaTime, position.z);
            }
            else
            {
                _movingLeftOrUp = false;
            }
        }
        else
        {
            if (position[_coordinate] < _rightOrUpEdge)
            {
                transform.position = movingDirection == MovingDirection.Horizontal
                    ? new Vector3(position.x + speed * Time.deltaTime, position.y, position.z)
                    : new Vector3(position.x, position.y + speed * Time.deltaTime, position.z);
            }
            else
            {
                _movingLeftOrUp = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
    
        if (col.collider.CompareTag("Player"))
        {
            col.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.transform.SetParent(playerContainer);
        }
    }
}
