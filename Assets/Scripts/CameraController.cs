using UnityEngine;

public class CameraController : MonoBehaviour {
    
    [SerializeField] private Transform[] players;
    [SerializeField] private float minSizeY = 5f;
    
    [SerializeField] private float aheadDistance = 3f;
    [SerializeField] private float cameraSpeed = 1.5f;
    
    private float _lookAhead;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    
    private void Update() {
        if (GameState.IsSinglePlayer)
        {
            SetCameraFollowPlayer();
        }
        else
        {
            SetCameraPos();
            SetCameraSize();
        }
    }

    private void SetCameraPos() {
        Vector3 middle = (players[0].position + players[1].position) * 0.5f;
 
        transform.position = new Vector3(
            middle.x,
            middle.y,
            transform.position.z
        );
    }
 
    private void SetCameraSize() {
        // horizontal size is based on actual screen ratio
        float minSizeX = minSizeY * Screen.width / Screen.height;
 
        // multiplying by 0.5, because the orthographicSize is actually half the height
        float width = Mathf.Abs(players[0].position.x - players[1].position.x) * 0.5f + 10f;
        float height = Mathf.Abs(players[0].position.y - players[1].position.y) * 0.5f;
 
        // computing the size
        float camSizeX = Mathf.Max(width, minSizeX);
        _camera.orthographicSize = Mathf.Max(height,
            camSizeX * Screen.height / Screen.width, minSizeY);
    }

    private void SetCameraFollowPlayer()
    {
        var playerPosition = players[GameState.CurrentCharacterIndex].position;
        transform.position = new Vector3(playerPosition.x + _lookAhead, playerPosition.y + 1, transform.position.z);
        _lookAhead = Mathf.Lerp(_lookAhead, aheadDistance * players[0].localScale.x, Time.deltaTime * cameraSpeed);
    }
}