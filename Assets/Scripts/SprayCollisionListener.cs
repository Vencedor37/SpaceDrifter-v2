using UnityEngine;
using System.Collections;

public class SprayCollisionListener : MonoBehaviour {
  public PlayerController player;
  private string blastTag = "Blast";
  private string asteroidTag = "Enemy";
  private string healthTag = "HealthPickup";
  private string movementTag = "MovementPickup";

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  public void OnChildTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag(asteroidTag)) {
      Debug.Log("sprayed asteroid");
    } else if (other.gameObject.CompareTag(blastTag)) {
      Debug.Log("sprayed laser blast");
    } else if (other.gameObject.CompareTag(movementTag)) {
      Debug.Log("sprayed fire extinguisher");
    } else if (other.gameObject.CompareTag(healthTag)) {
      Debug.Log("sprayed oxygen tank");
    }
  }
}
