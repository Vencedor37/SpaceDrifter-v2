using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	private float shakeAmount;
	public Camera mainCamera;
	public float duration;
	public float magnitude;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}


	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Player")) {
      Debug.Log("player col");
			StartCoroutine(Shake());
		}
	}

	IEnumerator Shake() {

    Debug.Log("shake");
		float elapsed = 0.0f;
		Vector3 originalCamPos = mainCamera.transform.position;

		while (elapsed < duration) {

			elapsed += Time.deltaTime;

			float percentComplete = elapsed / duration;
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

      mainCamera.transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);
		//	mainCamera.transform.position = new Vector3(x, y, originalCamPos.z);
			yield return null;
		}
	}

}
