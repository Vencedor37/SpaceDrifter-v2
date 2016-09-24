using UnityEngine;
using System.Collections;

public class SpriteScaleController : MonoBehaviour {
  public float screenWidth;
  public float screenHeight;
  public PlayerController player;

	// Use this for initialization
	void Start () {
    screenWidth = Screen.width;
    screenHeight = Screen.height;
    ResizeSprites();
    InvokeRepeating("CheckResolutionChange", 0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void ResizeSprites ()
  {
    player.Resize(screenWidth, screenHeight);
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    foreach (GameObject enemy in enemies) {
      enemy.GetComponent<SpaceObject>().Resize(screenWidth, screenHeight);
    }
  }

  void CheckResolutionChange()
  {
    if (Screen.width != screenWidth || Screen.height != screenHeight) {
      screenWidth = Screen.width;
      screenHeight = Screen.height;
      ResizeSprites();
    }
  }
}
