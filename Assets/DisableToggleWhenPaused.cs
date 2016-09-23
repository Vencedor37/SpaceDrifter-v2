using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisableToggleWhenPaused : MonoBehaviour {
  public Toggle targetToggle;
  public UIController UI;

	// Use this for initialization
	void Start () {
    if (targetToggle == null) {
      targetToggle = GetComponent<Toggle>();
    }

	}

	// Update is called once per frame
	void Update () {
    if (UI.isPaused) {
      targetToggle.interactable = false;
    } else {
      targetToggle.interactable = true;
    }

	}
}
