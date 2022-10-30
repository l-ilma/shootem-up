using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject gameModeScreen;
    [SerializeField] private GameObject playerSelectionScreen;
    [SerializeField] private Image[] characters;

    private void Awake()
    {
        gameModeScreen.SetActive(true);
        playerSelectionScreen.SetActive(false);
        GameState.CurrentCharacterIndex = 0;
    }

    private void Update()
    {
        if (playerSelectionScreen.activeInHierarchy)
        {
            ChooseCharacter();
        }
    }

    private void ChooseCharacter()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (GameState.CurrentCharacterIndex <= 0)
            {
                GameState.CurrentCharacterIndex = 0;
                return;
            }

            characters[GameState.CurrentCharacterIndex--].transform.localScale = new Vector3(1, 1, 1);
            characters[GameState.CurrentCharacterIndex].transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (GameState.CurrentCharacterIndex >= characters.Length - 1)
            {
                GameState.CurrentCharacterIndex = characters.Length - 1;
                return;
            }
            characters[GameState.CurrentCharacterIndex++].transform.localScale = new Vector3(1, 1, 1);
            characters[GameState.CurrentCharacterIndex].transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(GameState.CurrentLevel + 1);
        }
    }
    

    public void SinglePlayer()
    {
        gameModeScreen.SetActive(false);
        GameState.IsSinglePlayer = true;
        GameState.WasMultiplayer = false;
        playerSelectionScreen.SetActive(true);
    }
    
    public void MultiPlayer()
    {
        gameModeScreen.SetActive(false);
        GameState.IsSinglePlayer = false;
        SceneManager.LoadScene(2);
    }
}