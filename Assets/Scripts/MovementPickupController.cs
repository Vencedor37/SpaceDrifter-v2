using UnityEngine;
using System.Collections;

public class MovementPickupController : MonoBehaviour {
  private float movementBonus;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  public void setMovementBonus(float value)
  {
    movementBonus = value;
  }

  public float getMovementBonus()
  {
    return movementBonus;
  }
}
