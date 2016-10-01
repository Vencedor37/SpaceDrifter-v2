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
    if (player.getHasMovementUpgrade()) {
      SpaceObject otherSpace = other.gameObject.GetComponent<SpaceObject>();
      if (other.gameObject.CompareTag(asteroidTag)) {
        otherSpace.StartSpray(player.getLastForceApplied());
      } else if (other.gameObject.CompareTag(blastTag)) {
        //probably need smarter blast handling
        BlastObject otherBlast = (BlastObject)otherSpace;
        otherBlast.Repel(player.getLastForceApplied());
      } else if (other.gameObject.CompareTag(movementTag)) {
        otherSpace.StartSpray(player.getLastForceApplied());
      } else if (other.gameObject.CompareTag(healthTag)) {
        otherSpace.StartSpray(player.getLastForceApplied());
      }
    }
  }
}
