using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {

  public int HEALTH_COUNT;
  public int MOVEMENT_COUNT;
  public float standardHealthBonus;
  public float standardMovementBonus;
  public GameObject health;
  public GameObject movement;

  public UIController UI;

	// Use this for initialization
	void Start () {
    float x;
    float y;

    for (int i = 0; i < HEALTH_COUNT; i++) {
      x = RandomHorizontalCoordinates()[Random.Range(0,1)];
      y = RandomHorizontalCoordinates()[Random.Range(0,1)];
      var newHealth = Instantiate(health, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
      newHealth.transform.parent = GetComponent<Transform>();
      newHealth.GetComponent<HealthController>().setHealthBonus(standardHealthBonus);
    }

    for (int i = 0; i < MOVEMENT_COUNT; i++) {
      x = RandomHorizontalCoordinates()[Random.Range(0,1)];
      y = RandomHorizontalCoordinates()[Random.Range(0,1)];
      var newMovement = Instantiate(movement, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
      newMovement.transform.parent = GetComponent<Transform>();
      newMovement.GetComponent<MovementPickupController>().setMovementBonus(standardMovementBonus);
    }

	}

	// Update is called once per frame
	void Update () {

	}

  float[] RandomHorizontalCoordinates()
  {
    float[] coordinates = new float[2];
    coordinates[0] = Random.Range(UI.getLeftBoundary(), UI.getLeftSafety());
    coordinates[1] = Random.Range(UI.getRightSafety(), UI.getRightBoundary());
    return coordinates;
  }

  float[] RandomVerticalCoordinates()
  {
    float[] coordinates = new float[2];
    coordinates[0] = Random.Range(UI.getBottomBoundary(), UI.getBottomSafety());
    coordinates[1] = Random.Range(UI.getTopSafety(), UI.getTopBoundary());
    return coordinates;
  }

}
