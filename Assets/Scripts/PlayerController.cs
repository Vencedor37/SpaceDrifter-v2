using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

  public Camera mainCamera;
  public bool debugMode;
  public bool alive;
  public float maxSpeed;
  public float startingMoveCapacity;
  public float maxMoveCapacity;
  public float moveCost;

  public SpaceObjectController healthPickupController;
  public int activeHealthCount;
  public SpaceObjectController movementPickupController;
  public int activeMovementCount;
  public SpaceObjectController enemyController;
  public int enemiesPerLevel;

  public float maxHealth;
  public float startingHealth;
  public float healthLossRate;
  public float healthLossAmount;
  private float healthLossCounter = 0;
  private float currentHealth;

  public int startingScore;
  public float scoreIncreaseRate;
  public int scoreIncreaseAmount;
  public int scoreCheckpoint;
  public int scoreMultiplier = 1;

  public int checkPointHealthBonus;
  public int checkPointMovementBonus;
  private float scoreIncreaseCounter = 0;
  private int currentScore;
  private int scoreCheckpointTracker = 0;
  private int highScore;
  string highScoreKey = "HighScore";


  private Rigidbody2D rigidBody;
  private Vector3 pressPosition;
  private Vector3 releasePosition;
  private Vector3 currentPosition;
  private float currentMoveCapacity;
  private bool beatHighScore = false;

  private bool releaseOccurred = false;

  public GameObject itemDrawZone;
  public GameObject safetyZone;

  public bool showBonus = false;
  public int bonusAmount;
  public string bonusType;

  void Awake() {
    Application.targetFrameRate = 60;
  }

	// Use this for initialization
	void Start () {
    Time.timeScale = 1.0F;
    alive = true;
    rigidBody = GetComponent<Rigidbody2D>();
    currentHealth = startingHealth;
    currentMoveCapacity = startingMoveCapacity;
    currentScore = startingScore;
    highScore = PlayerPrefs.GetInt(highScoreKey,0);
    activeHealthCount = healthPickupController.activeCount;
    activeMovementCount = movementPickupController.activeCount;

    GameObject[] debugVisible = GameObject.FindGameObjectsWithTag("VisibleInDebugMode");
    if (!debugMode) {
      foreach (GameObject debugObject in debugVisible) {
        debugObject.GetComponent<SpriteRenderer>().enabled = false;
      }
    } else {
      foreach (GameObject debugObject in debugVisible) {
        debugObject.GetComponent<SpriteRenderer>().enabled = true;
      }
    }

	}

	// Update is called once per frame
	void Update ()
  {

    if (isInputPressed()) {
      pressPosition = Input.mousePosition;
    }

    if (isInputCurrentlyDown()) {
      currentPosition = Input.mousePosition;
      Vector3 dir = mainCamera.ScreenToWorldPoint(currentPosition) - transform.position;
      Debug.Log("current: " + dir.x + ", player: " + transform.position.x);
      float xDiff = currentPosition.x - pressPosition.x;
      if (Mathf.Abs(xDiff) > 20) {
        if (currentPosition.x - pressPosition.x > 0) {
          GetComponent<SpriteRenderer>().flipX = false;
        } else {
          GetComponent<SpriteRenderer>().flipX = true;
        }
        Quaternion newRotation = Quaternion.LookRotation(transform.position - currentPosition);
        newRotation.y = 0.0f;
        newRotation.x = 0.0f;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (GetComponent<SpriteRenderer>().flipX) {
          angle -= 180;
        }

        if (currentMoveCapacity > 0 && alive) {
          transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
          //        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8f);
        }
      }
    }

    if (isInputReleased()) {
      releasePosition = Input.mousePosition;
      releaseOccurred = true;
    }

    UpdateHealth();
    UpdateScore();
	}


	void FixedUpdate ()
  {
    if (rigidBody.velocity.magnitude > maxSpeed) {
      float breaksMagnitude = rigidBody.velocity.magnitude - maxSpeed;
      Vector3 breaksForce = Vector3.ClampMagnitude(rigidBody.velocity, breaksMagnitude);
      breaksForce *= -1;
      rigidBody.AddForce(breaksForce);
    }

    if (releaseOccurred) {
      Vector3 impulse = releasePosition - pressPosition;
      impulse *= -1;
      UpdateMovement(impulse);
      releaseOccurred = false;
    }
  }

  private void UpdateHealth()
  {
    healthLossCounter += Time.deltaTime;
    if (healthLossCounter >= healthLossRate) {
      healthLossCounter = 0;
      TakeDamage(healthLossAmount);
    }
  }

  private void UpdateScore()
  {
    scoreIncreaseCounter += Time.deltaTime;
    if (scoreIncreaseCounter >= scoreIncreaseRate) {
      scoreIncreaseCounter = 0;
      if (!showBonus) {
        StartCoroutine(IncreaseScore(scoreIncreaseAmount, 0));
      }
    }
  }

  private IEnumerator IncreaseScore(int amount, float time)
  {
    int modifiedScore = amount * scoreMultiplier;
    yield return new WaitForSeconds(time);
    currentScore += modifiedScore;
    showBonus = false;
    IncreaseScoreCheckpointTracker(modifiedScore);
    yield return null;
  }

  private void IncreaseScoreCheckpointTracker(int amount)
  {
    scoreCheckpointTracker += amount;
    if (scoreCheckpointTracker >= scoreCheckpoint * scoreMultiplier) {
      StartBonus(0, "Level Up!");
      scoreCheckpointTracker = 0;
      scoreMultiplier ++;
      activeHealthCount = healthPickupController.AddActive(checkPointHealthBonus);
      activeMovementCount = movementPickupController.AddActive(checkPointMovementBonus);
      enemyController.AddActive(enemiesPerLevel);
      currentHealth = maxHealth;
      currentMoveCapacity = maxMoveCapacity;
      healthLossCounter = 0;
    }
  }


  private void TakeDamage(float damage)
  {
    if ((currentHealth - damage) >= 0) {
      currentHealth -= damage;
    } else {
      currentHealth = 0;
    }
    if (currentHealth <= 0) {
      alive = false;
      GameOver();
    }
  }

  private void UpdateMovement(Vector3 movement)
  {
    Vector3 force = movement;
    float moveCostAmount = magnitudeToMoveCostAmount(movement.magnitude);
    if (currentMoveCapacity - moveCostAmount >= 0) {
      currentMoveCapacity -= moveCostAmount;
      rigidBody.angularVelocity = 0.0f;
      rigidBody.AddForce(force);
    } else if (currentMoveCapacity > 0) {
      float reducedMovementMagnitude = moveCostAmountToMagnitude(currentMoveCapacity);
      force = Vector3.ClampMagnitude(movement, reducedMovementMagnitude);
      currentMoveCapacity = 0;
      rigidBody.angularVelocity = 0.0f;
      rigidBody.AddForce(force);
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
      SpaceObject healthController = other.gameObject.GetComponent<SpaceObject>();
      if (healthController.destroyOnCollision) {
        other.gameObject.SetActive(false);
        activeHealthCount --;
      }
      if (currentHealth + healthController.getHealthBonus() <= maxHealth) {
        currentHealth += healthController.getHealthBonus();
      } else {
        currentHealth = maxHealth;
      }
      if (healthController.getPointsBonus() > 0) {
        StartBonus(healthController.getPointsBonus(), "Oxygen");
      }
      healthLossCounter = 0;
    }

    if (other.gameObject.CompareTag("MovementPickup")) {
      SpaceObject movementPickupController = other.gameObject.GetComponent<SpaceObject>();
      if (movementPickupController.destroyOnCollision) {
        other.gameObject.SetActive(false);
        activeMovementCount --;
      }
      if (currentMoveCapacity + movementPickupController.getMovementBonus() <= maxMoveCapacity) {
        currentMoveCapacity += movementPickupController.getMovementBonus();
      } else {
        currentMoveCapacity = maxMoveCapacity;
      }

      if (movementPickupController.getPointsBonus() > 0) {
        StartBonus(movementPickupController.getPointsBonus(), "Fuel");
      }
    }

    if (other.gameObject.CompareTag("PointsPickup")) {
      SpaceObject pointsPickupController = other.gameObject.GetComponent<SpaceObject>();
      if (pointsPickupController.destroyOnCollision) {
        other.gameObject.SetActive(false);
      }
      StartBonus(pointsPickupController.getPointsBonus(), "Star Dust");
    }
  }

  public void StartBonus(int amount, string type)
  {
    showBonus = true;
    bonusAmount = amount;
    bonusType = type;
    StartCoroutine(IncreaseScore(bonusAmount, 1f));
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Enemy")) {
      EnemyCollision(other);
    }
  }


  void EnemyCollision(Collision2D other)
  {
    float force = 50;
    Transform transform = GetComponent<Transform>();
    Vector3 dir = (Vector3)other.contacts[0].point - (Vector3)transform.position;
    dir = -dir.normalized;
    GetComponent<Rigidbody2D>().AddForce(dir*force);
    SpaceObject enemyControl = other.gameObject.GetComponent<SpaceObject>();
    TakeDamage(enemyControl.getDamage());
  }

  public void GameOver()
  {
    if (currentScore > highScore) {
      beatHighScore = true;
      highScore = currentScore;
      PlayerPrefs.SetInt(highScoreKey, highScore);
      PlayerPrefs.Save();
    }
    Time.timeScale = 0;
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

  public int getCurrentScore()
  {
    return currentScore;
  }

  public int getHighScore()
  {
    return highScore;
  }

  public bool getBeatHighScore()
  {
    return beatHighScore;
  }

  public Bounds getDrawBounds()
  {
    return itemDrawZone.GetComponent<SpriteRenderer>().bounds;
  }

  public Bounds getSafetyBounds()
  {
    return safetyZone.GetComponent<SpriteRenderer>().bounds;
  }

  public Vector3 getRandomPosition(bool avoidCentre)
  {
    float x;
    float y;
    float z = 0;

    Bounds drawBounds = itemDrawZone.GetComponent<SpriteRenderer>().bounds;
    Vector3 drawMin = drawBounds.min;
    Vector3 drawMax = drawBounds.max;

    if (!avoidCentre) {
      x = Random.Range(drawMin.x, drawMax.x);
      y = Random.Range(drawMin.y, drawMax.y);

    } else {

      Bounds safeBounds = safetyZone.GetComponent<SpriteRenderer>().bounds;
      Vector3 safeMin = safeBounds.min;
      Vector3 safeMax = safeBounds.max;

      bool goAboveOrBelowSafety = (Random.Range(0,2) == 1);
      if (goAboveOrBelowSafety) {
        // don't need to worry about x
        x = Random.Range(drawMin.x, drawMax.x);
        bool goAbove = (Random.Range(0,2) == 1);
        if (goAbove) {
          y = Random.Range(safeMax.y, drawMax.y);
        } else {
          //go below
          y = Random.Range(drawMin.y, safeMin.y);
        }
      } else {
        // goLeftOrRightOfSafety
        // don't need to worry about y
        y = Random.Range(drawMin.y, drawMax.y);
        bool goLeft = (Random.Range(0,2) == 1);
        if (goLeft) {
          x = Random.Range(drawMin.x, safeMin.x);
        } else {
          // go right
          x = Random.Range(safeMax.x, drawMax.x);
        }
      }
    }
    Vector3 pos = new Vector3(x, y, z);

    return pos;
  }


}
