using UnityEngine;
using System.Collections;

public class TimeKeeper : MonoBehaviour {

  public SpaceObjectController[] spawnableObjects = new SpaceObjectController[1];
  public float[] objectSpawnTimes = new float[1];


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    CheckSpawns();
	}

  public void CheckSpawns()
  {
    for (int i = 0; i < spawnableObjects.Length; i ++) {
      if (objectSpawnTimes[i] != 0 && Time.timeSinceLevelLoad >= objectSpawnTimes[i]) {
        spawnableObjects[i].Spawn();  
      }
    }
  }

  public void ClearCentre()
  {
    foreach (SpaceObjectController spawner in spawnableObjects) {
      spawner.ClearCentre(); 
    }
  }

  public void RestoreNonRenewableObjects()
  {
    foreach (SpaceObjectController spawner in spawnableObjects) {
      if (spawner.canBeDepleted && !spawner.replaceActive) {
        spawner.StartCoroutine("RestoreObjects");
      }
    }
  }



}
