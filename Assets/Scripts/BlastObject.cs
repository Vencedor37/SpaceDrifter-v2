using UnityEngine;
using System.Collections;

public class BlastObject : SpaceObject {
  public Transform playerT;
  public PlayerController player;
  public float offScreenCheckFrequency = 1;
  public float accuracyFactor = 1;
  public bool isRepelled = false;
  public bool allCollisions = false;

	// Use this for initialization
	void Start () {
    InvokeRepeating("CheckNeedsDestroying", offScreenCheckFrequency, offScreenCheckFrequency);
//    InvokeRepeating("HomeToPlayer", accuracyFactor, accuracyFactor);
	}

	// Update is called once per frame
	void Update () {

	}

  void OnTriggerEnter2D(Collider2D other)
  {
    if (allCollisions) {
      if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("HealthPickup")
          || other.gameObject.CompareTag("MovementPickup")) {
        Destroy(gameObject);
      }
    }
    if (isRepelled && GetComponent<Renderer>().isVisible) {
      if (other.gameObject.CompareTag("EnemySpaceship")) {
        other.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        other.gameObject.GetComponent<Spaceship>().StartCoroutine("BlowUp");
        Destroy(gameObject);
      }
    }
  }

  void CheckNeedsDestroying()
  {
    if (gameObject.activeSelf && !GetComponent<Renderer>().isVisible) {
      Destroy(gameObject);
    }
    if (GetComponent<Rigidbody2D>().velocity == Vector2.zero) {
      Destroy(gameObject);
    }
  }

  IEnumerator DestroyMe() {
    yield return new WaitForSeconds(2f);
    if (!GetComponent<Renderer>().isVisible) {
      Destroy(gameObject);
    }
  }

  public void Repel(Vector3 impulse)
  {
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
    Vector3 dir = impulse;
    Vector3 force = dir.normalized *-1.25f * rigidbody.velocity.magnitude;
    rigidbody.velocity = force;
    isRepelled = true;
  }

}
