using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    
    [DllImport("__Internal")]
    private static extern void CloseWindow();

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
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            CloseWindow();
        }
        else
        {
            Application.Quit();
        }
    }
}