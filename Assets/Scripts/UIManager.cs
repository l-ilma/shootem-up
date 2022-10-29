using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject[] healthBars;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        if (GameState.IsSinglePlayer) SetCurrentPlayerHealthBar();
    }

    private void SetCurrentPlayerHealthBar()
    {
       GameState.SetActiveObject(healthBars);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        if (GameState.WasMultiplayer)
        {
            GameState.IsSinglePlayer = false;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}