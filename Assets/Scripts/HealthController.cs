using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {
  private float healthBonus;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void setHealthBonus(float value)
  {
    healthBonus = value;
  }

  public float getHealthBonus()
  {
    return healthBonus;
  }
}
