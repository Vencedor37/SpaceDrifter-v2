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
  private bool overlapping = false;
  private float overlapTime = 0;
  public float myWidth;
  public float myHeight;

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

  void OnCollisionStay(Collision collisionInfo)
  {
    if (!overlapping) {
      overlapping = true;
    }
    overlapTime += Time.deltaTime;
    if (overlapTime > 2) {
      GetComponent<GameObject>().SetActive(false);  
    }
  }

  public void Resize(float screenWidth, float screenHeight)
  {
    Vector3 scale = transform.localScale;
    float oldWidth = scale.x;
    float oldHeight = scale.y;
    float newWidth = oldWidth * myWidth/myHeight * screenWidth/screenHeight;
    float newHeight = oldHeight * myWidth/myHeight * screenWidth/screenHeight;
    scale.x = newWidth;
    scale.y = newHeight;
    transform.localScale = scale;
  }


}
