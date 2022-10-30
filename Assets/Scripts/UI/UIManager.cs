using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        if (winScreen != null) winScreen.SetActive(false);
    }
    
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        if (GameState.WasMultiplayer) // if the game was multiplayer, load both players on restart
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
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        } 
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Application.OpenURL("about:blank");
        }
        else
        {
            Application.Quit();
        }
    }
}