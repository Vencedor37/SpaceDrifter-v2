using UnityEngine;
using System.Collections;

public class SprayChildCollision : MonoBehaviour {
  public SprayCollisionListener parent;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  void OnTriggerEnter2D(Collider2D other)
  {
    parent.OnChildTriggerEnter2D(other);
  }
}
