using UnityEngine;
using System.Collections;

public class IncreaseSpaceObjects : MonoBehaviour {

  public SpaceObjectController controller;
  private float timer;
  public float incrementTime;
  public int incrementAmount;
  private int objectLimit;

	// Use this for initialization
	void Start () {
    timer = 0;
    objectLimit = controller.poolCount;
	}
	
	// Update is called once per frame
	void Update () {
    AddToTimer();
	}

  void AddToTimer()
  {
    timer += Time.deltaTime; 
    if (timer >= incrementTime) {
      if (incrementAmount + controller.activeCount <= objectLimit) {
        controller.AddActive(incrementAmount);
      }
      timer = 0;
    }
  }
}
