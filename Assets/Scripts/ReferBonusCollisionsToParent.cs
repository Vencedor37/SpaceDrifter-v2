using UnityEngine;
using System.Collections;

public class ReferBonusCollisionsToParent : MonoBehaviour {
  
  public BonusCollisionListener parent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnTriggerExit2D (Collider2D other) {
    parent.onChildTriggerExit2D(other, name);
  }
}
