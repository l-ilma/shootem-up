using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Transform fireballStartPosition;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private KeyCode fire;

    private float _timeElapsed = Mathf.Infinity;
    private PlayerMovement _playerMovement;
    private Animator _animator;
    
    private static readonly int Attacking = Animator.StringToHash("attack");

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameState.IsSinglePlayer)
        {
            if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter))
                && _playerMovement.CanAttack() && _timeElapsed >= attackCooldown)
            {
                Attack();
            }
        }
        else
        {
            if (Input.GetKey(fire) && _playerMovement.CanAttack() && _timeElapsed >= attackCooldown)
            {
                Attack();
            }
        }
        _timeElapsed += Time.deltaTime;
    }

    private void Attack()
    {
        _timeElapsed = 0;
        _animator.SetTrigger(Attacking);

        int index = GetAvailableFireballIndex();
        if (index != -1)
        {
            fireballs[index].transform.position = fireballStartPosition.position;
            fireballs[index].GetComponent<Fireball>().Activate(Mathf.Sign(transform.localScale.x));
        }
    }

    private int GetAvailableFireballIndex()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }

        return -1;
    }
}