using UnityEngine;

public class FramesPerSecond : MonoBehaviour
{
  // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  // Members

  // --------------------------------------------------
  // Settings

  // Frames to skip till we begin counting fps drops, scene setup sometimes drops fps
  int framesToSkip = 500;

  // Frametime limit, above indicates a fps drop (40 ms = 25 fps)
  float msecLimit = 40;
  
  // --------------------------------------------------
  // Stats

  float msec = 0.0f; 
  float fps = 0.0f;
  float fpsSmooth = 0.0f;
  int belowLimitCounter = 0;

  // --------------------------------------------------
  // GUI elements

  [SerializeField] GameObject uiText = null;

  // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  // Methods

  // Called on the frame when a script is enabled just before any of the Update methods
  void Start()
  {
    // The number of VSyncs that should pass between each frame. Use 'Don't Sync' (0) to not wait for VSync.
    QualitySettings.vSyncCount = 0;

    // Limit frame rate to X
    Application.targetFrameRate = 90;
  }

  // Called every frame
  void Update()
  {
    // Use the timeScale-independent interval in seconds from the last frame to the current one to calculate stats
    this.msec = Time.unscaledDeltaTime * 1000f;
    this.fps = 1.0f / Time.unscaledDeltaTime;
    this.fpsSmooth = this.fpsSmooth + ((this.fps - this.fpsSmooth) * 0.08f);

    // Update UI
    string text = $"{this.msec:0.0} ms, {this.fpsSmooth:0} fps, {this.belowLimitCounter}";
    this.uiText.GetComponent<UnityEngine.UI.Text>().text = text;

    // Wait some frames before starting to count fps drops
    if(this.framesToSkip > 0)
    {
      this.framesToSkip--;
      this.belowLimitCounter = this.framesToSkip;
      return;
    }

    // Count fps drops
    if(this.msec > this.msecLimit)
    {
      this.belowLimitCounter++;
    }
  }
}