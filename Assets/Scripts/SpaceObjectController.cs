using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceObjectController : MonoBehaviour {

  public SpaceObject[] pool;
  public List<SpaceObject> activeSpaceObjects;
  public SpaceObject type;
  public int poolCount = 0;
  public int activeCount = 0;
  public PlayerController playerController;
  public UIController UI;
  public bool replaceActive = false;
  public bool avoidCentreDuringGeneration = true;
  public bool needsCleaning = false;
  public bool needsPositionCheck = true;
  public float cleanFrequency = 1;
  public float positionCheckFrequency = 1;
  private bool firstBuild = true;
  public bool needsSpeedCheck = true;
  public float speedCheckFrequency = 1;
  public bool randomiseTransparency = false;



	// Use this for initialization
	void Start () {
    InitialisePool();
    BuildActiveList();
    if (needsCleaning) {
      InvokeRepeating("CleanActiveList", 0.2f, cleanFrequency);
    }
    if (needsPositionCheck) {
      InvokeRepeating("CheckObjectPositioning", 0.2f, positionCheckFrequency);
    }
    if (needsSpeedCheck) {
      InvokeRepeating("CheckObjectSpeed", 0.2f, speedCheckFrequency);
    }
	}

	// Update is called once per frame
	void Update () {

	}

  float RandomHorizontalCoordinates()
  {
    return Random.Range(UI.getLeftBoundary(), UI.getRightBoundary());
  }

  float RandomVerticalCoordinates()
  {
    return Random.Range(UI.getBottomBoundary(), UI.getTopBoundary());
  }

  void InitialisePool()
  {
    pool = new SpaceObject[poolCount];
    for (int i = 0; i < poolCount; i++) {
      SpaceObject newObject = Instantiate(type, new Vector3(0, 0, 0), Quaternion.identity) as SpaceObject;
      newObject.transform.SetParent(GetComponent<Transform>(), false);
      setInitialValues(newObject);
      newObject.gameObject.SetActive(false);
      pool[i] = newObject;
    }
  }

  public void setInitialValues(SpaceObject spaceObject)
  {
    if (randomiseTransparency) {
      SpriteRenderer spriteRenderer = spaceObject.GetComponent<SpriteRenderer>();
      Color newColor = spriteRenderer.color;
      float newAlpha = Random.Range(.4f, 1f);
      newColor.a = newColor.a * newAlpha;
      spriteRenderer.color = newColor;
    }
  }

  void CleanActiveList()
  {
    // remove all that are not active
    for (int i = 0; i < activeSpaceObjects.Count; i++) {
      SpaceObject spaceObject = activeSpaceObjects[i];
      if (!spaceObject.gameObject.activeInHierarchy) {
        activeSpaceObjects.RemoveAt(i);
      }
    }

    if (!replaceActive) {
      activeCount = activeSpaceObjects.Count;
    } else {
      BuildActiveList();
    }
  }

  void BuildActiveList()
  {
    int currentActive = activeSpaceObjects.Count;
    int activeRequired = activeCount - currentActive;
    for (int i = 0; i < pool.Length; i++) {
      if (activeRequired > 0) {
        SpaceObject spaceObject = pool[i];
        if (!spaceObject.gameObject.activeInHierarchy) {
          Vector3 newPos = playerController.getRandomPosition(!(firstBuild && !avoidCentreDuringGeneration));
          spaceObject.GetComponent<Transform>().position = newPos;
          spaceObject.gameObject.SetActive(true);
          spaceObject.startMoving();
          activeSpaceObjects.Add(spaceObject);
          activeRequired --;
        }
      } else {
        break;
      }
    }

    GameObject[] debugVisible = GameObject.FindGameObjectsWithTag("VisibleInDebugMode");
    if (!playerController.debugMode) {
      foreach (GameObject debugObject in debugVisible) {
        if (debugObject.GetComponent<SpriteRenderer>() != null) {
          debugObject.GetComponent<SpriteRenderer>().enabled = false;
        }
      }
    } else {
      foreach (GameObject debugObject in debugVisible) {
        if (debugObject.GetComponent<SpriteRenderer>() != null) {
          debugObject.GetComponent<SpriteRenderer>().enabled = true;
        }
      }
    }
    firstBuild = false;
  }

  void CheckObjectPositioning()
  {
    foreach (SpaceObject activeObject in activeSpaceObjects) {
      SpriteRenderer spriteRenderer = activeObject.GetComponent<SpriteRenderer>();
      Bounds bounds = spriteRenderer.bounds;
      if (!bounds.Intersects(playerController.getDrawBounds())) {
        Vector3 newPosition = playerController.getRandomPosition(true);
        activeObject.GetComponent<Transform>().position = newPosition;
        activeObject.startMoving();
      }
    }
  }

  void CheckObjectSpeed()
  {
    foreach (SpaceObject activeObject in activeSpaceObjects) {
      SpriteRenderer spriteRenderer = activeObject.GetComponent<SpriteRenderer>();
      Bounds bounds = spriteRenderer.bounds;
      if (!bounds.Intersects(playerController.getSafetyBounds()) && activeObject.GetComponent<Rigidbody2D>().velocity.magnitude < activeObject.MIN_SPEED) {
//        Debug.Log("moving object");
        activeObject.startMoving();
      }
    }
  }

  public int AddActive(int number)
  {
    activeCount += number;
    BuildActiveList();
    return activeCount;
  }

}
