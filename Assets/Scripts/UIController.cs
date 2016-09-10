using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
  private LineRenderer lineRenderer;
  private const float LEFT_BOUNDARY   = -100;
  private const float RIGHT_BOUNDARY  =  100;
  private const float TOP_BOUNDARY    =  100;
  private const float BOTTOM_BOUNDARY = -100;

  private const float LEFT_SAFETY     = -10;
  private const float RIGHT_SAFETY    =  10;
  private const float TOP_SAFETY      =  10;
  private const float BOTTOM_SAFETY   = -10;


  public PlayerController playerController;
  public Camera mainCamera;
  public Color lineColour1;
  public Color lineColour2;
  public Slider healthSlider;
  public Image healthFill;

  public Slider movementSlider;
  public Image movementFill;

  public Text scoreText;



	// Use this for initialization
	void Start () {
    lineRenderer = gameObject.AddComponent<LineRenderer>();
    lineRenderer.SetColors(lineColour1, lineColour2);
    lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    lineRenderer.SetVertexCount(2);
    lineRenderer.useWorldSpace = true;
    lineRenderer.sortingLayerName = "UI";
	}

	// Update is called once per frame
	void Update () {
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

    UpdateHealthSlider();
    UpdateMovementSlider();
    UpdateScoreText();
	}

  private void UpdateHealthSlider()
  {
    healthSlider.value = playerController.getCurrentHealth();
    if (healthSlider.value <= 50 && healthSlider.value > 30) {
      healthFill.color = Color.yellow;
    } else if (healthSlider.value < 30) {
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
      lineColour1 = Color.red;
      lineColour2 = Color.black;
    }
    lineRenderer.SetColors(lineColour1, lineColour2);
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
