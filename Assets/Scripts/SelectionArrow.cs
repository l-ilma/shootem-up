using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform _rectTransform;
    private int _currentOptionIndex;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            ChangeOption(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            ChangeOption(1);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            Interact();
        }
    }

    private void ChangeOption(int optionIndex)
    {
        _currentOptionIndex += optionIndex;
        if (_currentOptionIndex < 0)
        {
            _currentOptionIndex = options.Length - 1;
        }
        else if (_currentOptionIndex >= options.Length)
        {
            _currentOptionIndex = 0;
        }

        _rectTransform.position = new Vector3(_rectTransform.position.x, options[_currentOptionIndex].position.y, 0);
    }

    private void Interact()
    {
        options[_currentOptionIndex].GetComponent<Button>().onClick.Invoke();
    }
}