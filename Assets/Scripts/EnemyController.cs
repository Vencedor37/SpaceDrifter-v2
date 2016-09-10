using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

  public int ENEMY_COUNT;
  public float standardDamage;
  private UIController UI;
  public GameObject enemy;


	// Use this for initialization
	void Start () {
    GameObject UIControl = GameObject.Find("UIController");
    UI = UIControl.GetComponent<UIController>();
    AddEnemies();
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

  void AddEnemies()
  {
    float x;
    float y;
    int childCount = GetComponent<Transform>().childCount;
    while (childCount < ENEMY_COUNT) {
      x = RandomHorizontalCoordinates()[Random.Range(0,1)];
      y = RandomHorizontalCoordinates()[Random.Range(0,1)];
      var newEnemy = Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
      newEnemy.transform.parent = GetComponent<Transform>();
      newEnemy.GetComponent<Enemy>().setDamage(standardDamage);
      childCount ++;
    }
  }

}
