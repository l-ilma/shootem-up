using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    [SerializeField] private GameObject[] players;

    private readonly List<GameObject> _playersPassedLevel = new List<GameObject>();
    
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
                if (_playersPassedLevel.Count == players.Length)
                {
                    LoadNextLevelOrFinishGame();
                }
                else
                {
                    col.GetComponent<PlayerMovement>().enabled = false;
                }
            }
        }
    }

    private void LoadNextLevelOrFinishGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            // win condition
                    
        }
        else
        {
            GameState.CurrentLevel++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}