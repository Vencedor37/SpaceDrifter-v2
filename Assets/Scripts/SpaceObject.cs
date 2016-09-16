using UnityEngine;
using System.Collections;

public class SpaceObject : MonoBehaviour {
  public float healthBonus   = 0;
  public int   pointsBonus   = 0;
  public float damage        = 0;
  public float movementBonus = 0;
  public float MAX_SPEED;
  public float MIN_SPEED;
  public bool destroyOnCollision;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  public float getHealthBonus()
  {
    return healthBonus;
  }

  public void setHealthBonus(float value)
  {
    healthBonus = value;
  }

  public int getPointsBonus()
  {
    return pointsBonus;
  }

  public void setPointsBonus(int value)
  {
    pointsBonus = value;
  }

  public float getDamage()
  {
    return damage;
  }

  public void setDamage(float value)
  {
    damage = value;
  }

  public float getMovementBonus()
  {
    return movementBonus;
  }

  public void setMovementBonus(float value)
  {
    movementBonus = value;
  }

  public void startMoving()
  {
    if (MAX_SPEED > 0) {
      GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * Random.Range(MIN_SPEED, MAX_SPEED);
    }
  }


}
