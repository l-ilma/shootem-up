using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] players;

    private void Awake()
    {
        if (GameState.IsSinglePlayer) GameState.SetActiveObject(players);
        for (int i = 0; i < players.Length - 1; i++)
        {
            Physics2D.IgnoreCollision(players[i].GetComponent<BoxCollider2D>(),
                players[i+1].GetComponent<BoxCollider2D>());
        }
    }

    private void Update()
    {
        if (GameState.IsSinglePlayer && GameState.GetActivePlayerObjects(players).Length > 1)
        {
            GameState.CurrentCharacterIndex = !players[0].GetComponent<PlayerMovement>().enabled ? 1 : 0;
            GameState.SetActiveObject(players);
        }
    }
}