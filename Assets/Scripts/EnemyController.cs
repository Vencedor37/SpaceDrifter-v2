using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

  private const int ENEMY_COUNT = 40;
  private UIController UI;
  public Transform enemy;

	// Use this for initialization
	void Start () {
    GameObject UIControl = GameObject.Find("UIController");
    UI = UIControl.GetComponent<UIController>();
    float x;
    float y;

    for (int i = 0; i < ENEMY_COUNT; i++) {
      x = RandomHorizontalCoordinates()[Random.Range(0,1)];
      y = RandomHorizontalCoordinates()[Random.Range(0,1)];
      var newEnemy = Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
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
