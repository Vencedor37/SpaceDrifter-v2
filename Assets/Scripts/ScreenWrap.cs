using UnityEngine;
using System.Collections;

public class ScreenWrap : MonoBehaviour {
  private UIController UI;
  private bool isWrappingX;
  private  bool isWrappingY;

	// Use this for initialization
	void Start () {
    GameObject UIControl = GameObject.Find("UIController");
    UI = UIControl.GetComponent<UIController>();
	}

	// Update is called once per frame
	void Update () {

    Wrap();
	}

  void Wrap()
  {
    Transform transform = GetComponent<Transform>();
    Vector3 position = transform.position;

    if (isWrappingX && isWrappingY) {
      return;
    }

    Vector3 newPosition = position;


    if (!isOutsideHorizontalBounds(position) && !isOutsideVerticalBounds(position)) {
      isWrappingX = false;
      isWrappingY = false;
    }

    if (!isWrappingX && isOutsideHorizontalBounds(position)) {
      newPosition.x = -newPosition.x;

      isWrappingX = true;
    }

    if (!isWrappingY && isOutsideVerticalBounds(position)) {
      newPosition.y = -newPosition.y;

      isWrappingY = true;
    }

    transform.position = newPosition;
  }

  bool isOutsideHorizontalBounds(Vector3 position)
  {
    return (position.x < UI.getLeftBoundary() || position.x > UI.getRightBoundary());
  }

  bool isOutsideVerticalBounds(Vector3 position)
  {
    return (position.y < UI.getBottomBoundary() || position.y > UI.getTopBoundary());
  }
}
