using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {

  public float dampTime = 0.15f;
  private Vector3 velocity = Vector3.zero;
  private Camera mainCamera;

  public Transform target;

  void Start()
  {
    mainCamera = GetComponent<Camera>();
  }

  // Update is called once per frame
  void FixedUpdate ()
  {
    if (target) {
      Vector3 point = mainCamera.WorldToViewportPoint(target.position);
      Vector3 delta = target.position - mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
      Vector3 destination = transform.position + delta;
      transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
  }
}
