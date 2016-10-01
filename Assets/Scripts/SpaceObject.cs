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
  private bool sprayedRecently = false;
  public float sprayCooldown = 1f;

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
    if (GetComponent<Rigidbody2D>() != null) {
      Vector3 newVelocity = Random.onUnitSphere * Random.Range(MIN_SPEED, MAX_SPEED);
      GetComponent<Rigidbody2D>().velocity = newVelocity;
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

  public void AdjustSize(float xScale, float yScale) {
    Vector3 scale = transform.localScale;
    scale.x *= xScale;
    scale.y *= yScale;
    transform.localScale = scale;
  }

  public void AdjustSize(float scale) {
    AdjustSize(scale, scale);
  }

  public void AdjustSpeed(float minSpeed, float maxSpeed) {
    MAX_SPEED = maxSpeed;
    MIN_SPEED = minSpeed;
  }

  public void AdjustMass(float mass) {
    GetComponent<Rigidbody2D>().mass = mass;
  }

  public void StartSpray(Vector3 force)
  {
    if (!sprayedRecently) {
      StartCoroutine(Spray(force));
    }
  }

  public IEnumerator Spray(Vector3 force)
  {
    Debug.Log("spraying");
    sprayedRecently = true;
    Vector3 toApply = force *= -1;
    GetComponent<Rigidbody2D>().AddForce(toApply);
    yield return new WaitForSeconds(sprayCooldown);
    sprayedRecently = false;
    yield return null;
  }


}
