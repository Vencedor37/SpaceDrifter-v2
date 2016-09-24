using UnityEngine;
using System.Collections;

public class BounceAwayFromCollision : MonoBehaviour {
  public float force = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnCollisionEnter2D(Collision2D other)
  {
    Transform transform = GetComponent<Transform>();
    Vector3 dir = (Vector3)other.contacts[0].point - (Vector3)transform.position;
    dir = -dir.normalized;
    GetComponent<Rigidbody2D>().AddForce(dir*force);
  }
}
