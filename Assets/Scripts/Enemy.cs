using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
  private float damage;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  public void setDamage(float value)
  {
    damage = value;
  }

  public float getDamage()
  {
    return damage;
  }
}
