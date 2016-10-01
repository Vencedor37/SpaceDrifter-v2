using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour {
  private LineRenderer lineRenderer;
  public float lineBaseWidth;
  public float lineTopWidth;
  public float LEFT_BOUNDARY   = -100;
  public float RIGHT_BOUNDARY  =  100;
  public float TOP_BOUNDARY    =  100;
  public float BOTTOM_BOUNDARY = -100;

  public float LEFT_SAFETY     = -10;
  public float RIGHT_SAFETY    =  10;
  public float TOP_SAFETY      =  10;
  public float BOTTOM_SAFETY   = -10;

  private GameObject[] gameOverUI;

  public bool isFastForward = false;
  public bool isPaused = false;
  public float fastSpeed = 2.0f;
  public float normalSpeed = 1.0f;
  public bool quickHealthLoss = false;

  public PlayerController playerController;
  public Camera mainCamera;
  public Color lineColour1;
  public Color lineColour2;
  public Color fullHealthColour;
  public Shader lineShader;
  public Slider healthSlider;
  public Slider extraHealthSlider;
  public Image healthFill;
  public Animator healthAnimator;


  public Slider movementSlider;
  public Slider extraMovementSlider;
  public Image movementFill;
  public Animator movementAnimator;

  public Text scoreText;
  public Text bonusText;
  public Text highScoreText;
  public Text levelText;
  public Text gameOverSubtitle;
  public Text pauseText;

  public Text movementCountText;
  public Text healthCountText;

  public Toggle fastForwardToggle;
  public Toggle pauseToggle;


	// Use this for initialization
	void Start () {
    lineRenderer = gameObject.AddComponent<LineRenderer>();
    lineRenderer.SetColors(lineColour1, lineColour2);
    lineRenderer.material = new Material(lineShader);
    lineRenderer.SetVertexCount(2);
    lineRenderer.useWorldSpace = true;
    lineRenderer.sortingLayerName = "UI";

    gameOverUI = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
    bonusText.enabled = false;
    HideGameOverUI();
	}

	// Update is called once per frame
  void Update () {
    if (playerController.getAlive()&& !isPaused) {
      LineRenderer lineRenderer = GetComponent<LineRenderer>();
      if (playerController.isInputCurrentlyDown()) {
        lineRenderer.SetWidth(lineBaseWidth, lineTopWidth);
        Vector3[] points = playerController.getLinePoints();
        points[0].z = 1;
        points[1].z = 1;
        points[0] = mainCamera.ScreenToWorldPoint(points[0]);
        points[1] = mainCamera.ScreenToWorldPoint(points[1]);
        lineRenderer.SetPositions(points);
      } else {
        lineRenderer.SetWidth(0.0F, 0.0F);
      }
    }
    UpdateHealthSlider();
    UpdateMovementSlider();
    UpdateScoreText();
    UpdateBonusText();
    UpdatePauseDisplay();

    if (playerController.getIsGameOver()) {
      ShowGameOverUI();
      if (playerController.getCauseOfDeath() == "oxygen") {
        gameOverSubtitle.text = "Ran out of oxygen";
      } else if (playerController.getCauseOfDeath() == "asteroid") {
        gameOverSubtitle.text = "Hit by an asteroid";
      }
    } else {
      CheckSpeed();
    }

	}

  private void UpdatePauseDisplay()
  {
    if (isPaused && !playerController.getIsGameOver()) {
      pauseText.enabled = true;
    } else {
      pauseText.enabled = false;
    }

  }

  private void UpdateHealthSlider()
  {
    if (playerController.HasExtraHealthBar()) {
      extraHealthSlider.gameObject.SetActive(true);
      healthSlider.value = playerController.getStartingMaxHealth();
      extraHealthSlider.value = playerController.GetExtraHealthAmount();
    } else {
      extraHealthSlider.gameObject.SetActive(false);
      healthSlider.value = playerController.getCurrentHealth();
      healthAnimator.SetFloat("Health", playerController.getCurrentHealth());
      if (healthSlider.value <= 50 && healthSlider.value > 30) {
        healthFill.color = Color.yellow;
      } else if (healthSlider.value < 30) {
        if (!quickHealthLoss) {
          playerController.RescaleHealthRates(.4f);
          quickHealthLoss = true;
        }
        healthFill.color = Color.red;
      } else if (healthSlider.value > 50) {
        healthFill.color = fullHealthColour;
      }
    }
    if (healthSlider.value > 30 && quickHealthLoss) {
      playerController.RescaleHealthRates(2.5f);
    }
  }


  private void UpdateMovementSlider()
  {
    if (playerController.HasExtraMovementBar()) {
      extraMovementSlider.gameObject.SetActive(true);
      movementSlider.value = playerController.getStartingMaxMoveCapacity();
      extraMovementSlider.value = playerController.GetExtraMoveAmount();
    } else {
      extraMovementSlider.gameObject.SetActive(false);
      movementSlider.value = playerController.getCurrentMoveCapacity();
      movementAnimator.SetFloat("Movement", playerController.getCurrentMoveCapacity());
      movementFill.enabled = (movementSlider.value > 0);
    }
    if (playerController.getCurrentMoveCapacity() <= 0) {
      lineRenderer.SetColors(Color.red, Color.black);
    } else {
      lineRenderer.SetColors(lineColour1, lineColour2);
    }
  }

  /*
  public void UpdateCounterText()
  {
    healthCountText.text = "X " + playerController.activeHealthCount;
    movementCountText.text = "X " + playerController.activeMovementCount;
  }
  */

  public void HideGameOverUI()
  {
    foreach (GameObject gameObject in gameOverUI) {
      gameObject.SetActive(false);
    }
  }

  public void ShowGameOverUI()
  {
    if (playerController.getBeatHighScore()) {
      highScoreText.text = "New High Score!";
    } else {
      highScoreText.text = "High Score: " + playerController.getHighScore();
    }
    foreach (GameObject gameObject in gameOverUI) {
      gameObject.SetActive(true);
    }
  }

  void OnApplicationFocus( bool focusStatus )
	{
    if (!focusStatus && !isPaused) {
      pauseToggle.isOn = true;
    }
	}


  public void CheckSpeed()
  {
    if (isPaused || !playerController.getAlive()) {
      Time.timeScale = 0.0f;
    } else if (isFastForward) {
      Time.timeScale = fastSpeed;
    } else {
      Time.timeScale = normalSpeed;
    }
  }

  public void toggleSpeed()
  {
    isFastForward = !isFastForward;
  }

  public void togglePause()
  {
    isPaused = !isPaused;
    if (isPaused && isFastForward) {
      fastForwardToggle.isOn = false;
    }
  }

  public void RestartGame()
  {
    SceneManager.LoadScene("Main");
  }

  private void UpdateScoreText()
  {
    int score = playerController.getCurrentScore();
    scoreText.text = "Score: " + score;
    levelText.text = "Bonus: x" + playerController.getScoreMultiplier();
  }

  public void UpdateBonusText()
  {
    if (playerController.getShowBonus()) {
      if (playerController.getBonusAmount() > 0) {
        bonusText.text = playerController.getBonusType() + " +" + playerController.getBonusAmount() * playerController.getScoreMultiplier();
      } else {
        bonusText.text = playerController.getBonusType();
      }
      bonusText.enabled = true;
    } else {
      bonusText.enabled = false;
    }
  }

  public float getLeftBoundary()
  {
    return LEFT_BOUNDARY;
  }

  public float getRightBoundary()
  {
    return RIGHT_BOUNDARY;
  }

  public float getTopBoundary()
  {
    return TOP_BOUNDARY;
  }

  public float getBottomBoundary()
  {
    return BOTTOM_BOUNDARY;
  }

  public float getLeftSafety()
  {
    return LEFT_SAFETY;
  }

  public float getRightSafety()
  {
    return RIGHT_SAFETY;
  }

  public float getTopSafety()
  {
    return TOP_SAFETY;
  }

  public float getBottomSafety()
  {
    return BOTTOM_SAFETY;
  }

}
