using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    [SerializeField] private GameObject[] possiblePlayers;
    [SerializeField] private GameObject winScreen;

    private readonly List<GameObject> _playersPassedLevel = new List<GameObject>();
    private List<GameObject> _alivePlayers = new List<GameObject>();
    [SerializeField] private AudioClip winSound;

    private void Awake()
    {
        _alivePlayers = GameState.GetAlivePlayers(possiblePlayers).ToList();
    }

    private void Update()
    {
        var activePlayers = GameState.GetAlivePlayers(possiblePlayers);
        if (activePlayers.Length != _alivePlayers.Count)
        {
            _alivePlayers = activePlayers.ToList();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!_playersPassedLevel.Contains(col.gameObject))
            {
                _playersPassedLevel.Add(col.gameObject);
            }
            if (GameState.IsSinglePlayer)
            {
                LoadNextLevelOrFinishGame();
            }
            else
            {
                if (_playersPassedLevel.Count == _alivePlayers.Count)
                {
                    LoadNextLevelOrFinishGame();
                }
                else
                {
                    col.GetComponent<PlayerMovement>().enabled = false;
                    col.GetComponent<PlayerAttack>().enabled = false;
                    col.GetComponent<Animator>().SetBool("grounded", true);
                    col.GetComponent<Animator>().SetBool("walk", false);
                }
            }
        }
    }

    private void LoadNextLevelOrFinishGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            winScreen.SetActive(true);
            _playersPassedLevel.ForEach(player =>
            {
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<PlayerAttack>().enabled = false;
                player.GetComponent<Animator>().SetTrigger("win");
                SoundManager.Instance.PlaySound(winSound);
            });
        }
        else
        {
            if (GameState.WasMultiplayer && _alivePlayers.Count == 1)
            {
                // when starting the new level, the game is single player only
                // when restarting only single player is loaded
                GameState.WasMultiplayer = false;
            }
            GameState.CurrentLevel++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}