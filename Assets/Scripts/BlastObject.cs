using UnityEngine;
using System.Collections;

public class BlastObject : SpaceObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("HealthPickup")
       || other.gameObject.CompareTag("MovementPickup")) {
      Destroy(gameObject);
    }
  }
}
