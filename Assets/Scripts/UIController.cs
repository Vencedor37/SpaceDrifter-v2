using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour {
  private LineRenderer lineRenderer;
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
  public float fastSpeed = 2.0f;
  public float normalSpeed = 1.0f;
  public bool quickHealthLoss = false;


  public PlayerController playerController;
  public Camera mainCamera;
  public Color lineColour1;
  public Color lineColour2;
  public Shader lineShader;
  public Slider healthSlider;
  public Image healthFill;


  public Slider movementSlider;
  public Image movementFill;

  public Text scoreText;
  public Text highScoreText;

  public Text movementCountText;
  public Text healthCountText;


	// Use this for initialization
	void Start () {
    lineRenderer = gameObject.AddComponent<LineRenderer>();
    lineRenderer.SetColors(lineColour1, lineColour2);
    lineRenderer.material = new Material(lineShader);
    lineRenderer.SetVertexCount(2);
    lineRenderer.useWorldSpace = true;
    lineRenderer.sortingLayerName = "UI";

    gameOverUI = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
    HideGameOverUI();
	}

	// Update is called once per frame
  void Update () {
    if (playerController.alive) {
      LineRenderer lineRenderer = GetComponent<LineRenderer>();
      if (playerController.isInputCurrentlyDown()) {
        lineRenderer.SetWidth(0.03F, 0.001F);
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
    UpdateCounterText();

    if (!playerController.alive) {
      ShowGameOverUI();
    } else {
      CheckSpeed();
    }

	}

  private void UpdateHealthSlider()
  {
    healthSlider.value = playerController.getCurrentHealth();
    if (healthSlider.value <= 50 && healthSlider.value > 30) {
      healthFill.color = Color.yellow;
    } else if (healthSlider.value < 30) {
      if (!quickHealthLoss) {
        playerController.healthLossRate *= .4f;
        playerController.healthLossAmount *= .4f;
        quickHealthLoss = true;
      }
      healthFill.color = Color.red;
    }
    if (healthSlider.value <= 0) {
      healthFill.enabled = false;
    } else {
      healthFill.enabled = true;
    }
  }

  private void UpdateMovementSlider()
  {
    movementSlider.value = playerController.getCurrentMoveCapacity();
    movementFill.enabled = (movementSlider.value > 0);
    if (playerController.getCurrentMoveCapacity() <= 0) {
      lineRenderer.SetColors(Color.red, Color.black);
    } else {
      lineRenderer.SetColors(lineColour1, lineColour2);
    }
  }

  public void UpdateCounterText()
  {
    healthCountText.text = "X " + playerController.activeHealthCount;
    movementCountText.text = "X " + playerController.activeMovementCount;
  }

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

  public void CheckSpeed()
  {
    if (isFastForward) {
      Time.timeScale = fastSpeed;
    } else {
      Time.timeScale = normalSpeed;
    }
  }

  public void toggleSpeed()
  {
    isFastForward = !isFastForward;
  }

  public void RestartGame()
  {
    SceneManager.LoadScene("Main");
  }

  private void UpdateScoreText()
  {
    int score = playerController.getCurrentScore();
    scoreText.text = "Score: " + score;
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
