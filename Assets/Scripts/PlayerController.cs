using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

  public float maxSpeed;
  public float startingMoveCapacity;
  public float maxMoveCapacity;
  public float moveCost;

  public float maxHealth;
  public float startingHealth;
  public float healthLossRate;
  public float healthLossAmount;

  private Rigidbody2D rigidBody;
  private Vector3 pressPosition;
  private Vector3 releasePosition;
  private Vector3 currentPosition;
  private float healthLossCounter;
  private float currentMoveCapacity;
  private float currentHealth;

	// Use this for initialization
	void Start () {
    rigidBody = GetComponent<Rigidbody2D>();
    currentHealth = startingHealth;
    currentMoveCapacity = startingMoveCapacity;
	}

	// Update is called once per frame
	void Update ()
  {
    UpdateHealth();
	}


	void FixedUpdate ()
  {
    Transform transform = GetComponent<Transform>();
    if (rigidBody.velocity.magnitude > maxSpeed) {
      float breaksMagnitude = rigidBody.velocity.magnitude - maxSpeed;
      Vector3 breaksForce = Vector3.ClampMagnitude(rigidBody.velocity, breaksMagnitude);
      breaksForce *= -1;
      rigidBody.AddForce(breaksForce);
    }


    if (isInputPressed()) {
      pressPosition = Input.mousePosition;
    }

    if (isInputCurrentlyDown()) {
      currentPosition = Input.mousePosition;

      Vector3 dir = currentPosition;
      float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    if (isInputReleased()) {
      releasePosition = Input.mousePosition;
      Vector3 impulse = releasePosition - pressPosition;
      impulse *= -1;
      UpdateMovement(impulse);
    }
  }

  private void UpdateHealth()
  {
    healthLossCounter += Time.deltaTime;
    if (healthLossCounter >= (healthLossRate)) {
      healthLossCounter = 0;
      if ((currentHealth - healthLossAmount) >= 0) {
        currentHealth -= healthLossAmount;
      } else {
        currentHealth = 0;
      }
    }
  }

  private void UpdateMovement(Vector3 movement)
  {
    float moveCostAmount = magnitudeToMoveCostAmount(movement.magnitude);
    if (currentMoveCapacity - moveCostAmount >= 0) {
      rigidBody.AddForce(movement);
      currentMoveCapacity -= moveCostAmount;
    } else if (currentMoveCapacity > 0) {
      float reducedMovementMagnitude = moveCostAmountToMagnitude(currentMoveCapacity);
      Vector3 reducedMovement = Vector3.ClampMagnitude(movement, reducedMovementMagnitude);
      rigidBody.AddForce(reducedMovement);
      currentMoveCapacity = 0;
    }
  }

  private float magnitudeToMoveCostAmount(float magnitude)
  {
    return (magnitude * moveCost) / 100;
  }

  private float moveCostAmountToMagnitude(float costAmount)
  {
    return (100 * costAmount) / moveCost;
  }


  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("HealthPickup")) {
      HealthController healthController = other.gameObject.GetComponent<HealthController>();
      if (currentHealth + healthController.getHealthBonus() <= maxHealth) {
        currentHealth += healthController.getHealthBonus();
      } else {
        currentHealth = maxHealth;
      }
      healthLossCounter = 0;
      Destroy(other.gameObject);
    }

    if (other.gameObject.CompareTag("MovementPickup")) {
      MovementPickupController movementPickupController = other.gameObject.GetComponent<MovementPickupController>();
      if (currentMoveCapacity + movementPickupController.getMovementBonus() <= maxMoveCapacity) {
        currentMoveCapacity += movementPickupController.getMovementBonus();
      } else {
        currentMoveCapacity = maxMoveCapacity;
      }
      Destroy(other.gameObject);
    }
  }

  public bool isInputPressed()
  {
    if (Input.GetMouseButtonDown(0)) {
      return true;
    }
    return false;
  }

  bool isInputReleased()
  {
    if (Input.GetMouseButtonUp(0)) {
      return true;
    }
    return false;
  }


  public bool isInputCurrentlyDown()
  {
    if (Input.GetMouseButton(0)) {
      return true;
    }
    return false;
  }

  public Vector3 getPressPosition()
  {
    return pressPosition;
  }

  public Vector3 getReleasePosition()
  {
    return releasePosition;
  }

  public Vector3[] getLinePoints()
  {
    Vector3[] points = new Vector3[2];
    points[0] = pressPosition;
    points[1] = currentPosition;
    return points;
  }

  public float getCurrentHealth()
  {
    return currentHealth;
  }

  public float getCurrentMoveCapacity()
  {
    return currentMoveCapacity;
  }




}

