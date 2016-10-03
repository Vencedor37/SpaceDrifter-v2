using UnityEngine;
using System.Collections;

public class AspectRatioCheck : MonoBehaviour {

  public float screenWidth;
  public float screenHeight;
  public float targetAspect;
  public GameObject safeZone;

  void Start ()
  {
    screenWidth = Screen.width;
    screenHeight = Screen.height;
    ResizeCamera();
    ResizeSafeZone();
    InvokeRepeating("CheckResolutionChange", 0.5f, 0.5f);
  }

	// Update is called once per frame
	void Update () {

	}

  void ResizeCamera()
  {
    Camera camera = GetComponent<Camera>();

    float TARGET_WIDTH = 960.0f;
    float TARGET_HEIGHT = 540.0f;
    int PIXELS_TO_UNITS = 15; // 1:1 ratio of pixels to units
    float useValue;

    float desiredRatio;
    float currentRatio;
    if (Screen.width > Screen.height) {
      desiredRatio = TARGET_HEIGHT / TARGET_WIDTH;
      currentRatio = (float)Screen.height/(float)Screen.width;
      useValue = TARGET_WIDTH;
    } else {
      desiredRatio = TARGET_WIDTH / TARGET_HEIGHT;
      currentRatio = (float)Screen.width/(float)Screen.height;
      useValue = TARGET_HEIGHT;
    }

    if(currentRatio >= desiredRatio)
    {
      // Our resolution has plenty of width, so we just need to use the height to determine the camera size
      camera.orthographicSize = useValue / 4 / PIXELS_TO_UNITS;
    }
    else
    {
      // Our camera needs to zoom out further than just fitting in the height of the image.
      // Determine how much bigger it needs to be, then apply that to our original algorithm.
      float differenceInSize = desiredRatio / currentRatio;
      camera.orthographicSize = useValue / 4 / PIXELS_TO_UNITS * differenceInSize;
    }
  }

  public void ResizeSafeZone()
  {
    SpriteRenderer spriteRenderer = safeZone.GetComponent<SpriteRenderer>();
    Camera camera = GetComponent<Camera>();
    float worldScreenHeight = camera.orthographicSize * 2;
    float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

    safeZone.transform.localScale = new Vector3(
      worldScreenWidth / spriteRenderer.sprite.bounds.size.x * 1.20f,
      worldScreenHeight / spriteRenderer.sprite.bounds.size.y * 1.20f, 1);

  }

  void CheckResolutionChange()
  {
    if (Screen.width != screenWidth || Screen.height != screenHeight) {
      screenWidth = Screen.width;
      screenHeight = Screen.height;
      ResizeCamera();
      ResizeSafeZone();
    }
  }
}
