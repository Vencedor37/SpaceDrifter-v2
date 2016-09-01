using UnityEngine;
using System.Collections;

public class LineController : MonoBehaviour {
  private LineRenderer lineRenderer;
  public PlayerController playerController;
  public Camera mainCamera;
  public Color c1 = Color.red;
  public Color c2 = Color.white;

	// Use this for initialization
	void Start () {
    LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
    lineRenderer.SetColors(c1, c2);
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
	}
}
