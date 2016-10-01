using UnityEngine;
using System.Collections;

public class BlastObject : SpaceObject {
  public float offScreenCheckFrequency = 1;
  public bool isRepelled = false;

	// Use this for initialization
	void Start () {
    InvokeRepeating("CheckOffScreen", offScreenCheckFrequency, offScreenCheckFrequency);
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

  void CheckOffScreen()
  {
    if (!GetComponent<Renderer>().isVisible) {
      StartCoroutine(DestroyMe());
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
    Vector3 force = dir.normalized *-1 * rigidbody.velocity.magnitude; 
    rigidbody.velocity = force;
    isRepelled = true;
  }
}
