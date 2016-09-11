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
  public bool needsCleaning = false;
  public bool needsPositionCheck = true;
  public float cleanFrequency = 1;
  public float positionCheckFrequency = 1;

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
      newObject.transform.parent = GetComponent<Transform>();
      setInitialValues(newObject);
      newObject.gameObject.SetActive(false);
      pool[i] = newObject;
    }
  }

  public void setInitialValues(SpaceObject spaceObject)
  {
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
          Vector3 newPos = playerController.getRandomAcceptablePosition();
          spaceObject.GetComponent<Transform>().position = newPos;
          spaceObject.gameObject.SetActive(true);
          activeSpaceObjects.Add(spaceObject);
          activeRequired --;
        }
      } else {
        break;
      }
    }
  }

  void CheckObjectPositioning()
  {
    foreach (SpaceObject activeObject in activeSpaceObjects) {
      SpriteRenderer spriteRenderer = activeObject.GetComponent<SpriteRenderer>();
      Bounds bounds = spriteRenderer.bounds;
      if (!bounds.Intersects(playerController.getDrawBounds())) {
        Vector3 newPosition = playerController.getRandomAcceptablePosition();
        activeObject.GetComponent<Transform>().position = newPosition;
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
