using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleSpriteSwap : MonoBehaviour {
  public Sprite offSprite;
  public Sprite onSprite;
  public Toggle targetToggle;
  public bool defaultState;

	// Use this for initialization
	void Start () {
    if (targetToggle == null) {
      targetToggle = GetComponent<Toggle>();
    }
    targetToggle.toggleTransition = Toggle.ToggleTransition.None;
    targetToggle.onValueChanged.AddListener(SwapSprite);
    Image targetImage = targetToggle.targetGraphic as Image;
    if (targetImage != null) {
      if (!defaultState) {
        targetImage.overrideSprite = offSprite;
      } else {
        targetImage.overrideSprite = onSprite;
      }
    }
	}

	// Update is called once per frame
	void Update () {

	}

  void SwapSprite(bool value)
  {
    Image targetImage = targetToggle.targetGraphic as Image;
    if (targetImage != null) {
      if (value) {
        targetImage.overrideSprite = onSprite;
      } else {
        targetImage.overrideSprite = offSprite;
      }
    }
  }


}
