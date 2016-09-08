using UnityEngine;
using System.Collections;

public class RandomStartingMovement : MonoBehaviour {
  private const float MAX_SPEED = 6.0f;
  private const float MIN_SPEED = 2.5f;

	// Use this for initialization
	void Start () {
    GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * Random.Range(MIN_SPEED, MAX_SPEED);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
