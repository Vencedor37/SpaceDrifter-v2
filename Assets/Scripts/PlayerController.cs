using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

  private Rigidbody2D rigidBody;
  private Vector3 pressPosition;
  private Vector3 releasePosition;
  private Vector3 currentPosition;

	// Use this for initialization
	void Start () {
    rigidBody = GetComponent<Rigidbody2D>();

	}

	// Update is called once per frame
	void Update ()
  {
    if (isInputPressed()) {
      pressPosition = Input.mousePosition;
    }

    if (isInputCurrentlyDown()) {
      currentPosition = Input.mousePosition;
    }


    if (isInputReleased()) {
      releasePosition = Input.mousePosition;
      Vector3 impulse = releasePosition - pressPosition;
      impulse *= -1;
      rigidBody.AddForce(impulse);
    }
    Transform transform = GetComponent<Transform>();
    Debug.Log("player at: " + transform.position.x + ", " + transform.position.y);

	}

  public bool isInputPressed()
  {
    if (Input.GetMouseButtonDown(0)) {
      return true;
    }
    return false;
  }

  bool isInputReleased()
  {
    if (Input.GetMouseButtonUp(0)) {
      return true;
    }
    return false;
  }

  public bool isInputCurrentlyDown()
  {
    if (Input.GetMouseButton(0)) {
      return true;
    }
    return false;
  }

  public Vector3 getPressPosition()
  {
    return pressPosition;
  }

  public Vector3 getReleasePosition()
  {
    return releasePosition;
  }

  public Vector3[] getLinePoints()
  {
    Vector3[] points = new Vector3[2];
    points[0] = pressPosition;
    points[1] = currentPosition;
    return points;
  }


}

