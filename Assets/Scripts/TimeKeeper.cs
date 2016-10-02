using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeKeeper : MonoBehaviour {

  public PlayerController player;
  public float[] objectSpawnTimes = new float[1];
  public List<SpaceObjectCheckpoint> spawnCheckpoints;
  public List<bool>hasSpawned;
  public SpaceObjectController largeAsteroids;
  public SpaceObjectController mediumAsteroids;
  public SpaceObjectController smallAsteroids;
  public SpaceObjectController spaceships;
  public SpaceObjectController healthPickups;
  public SpaceObjectController movementPickups;
  public SpaceObjectController pointsPickupsLevel1;
  public SpaceObjectController pointsPickupsLevel2;
  public SpaceObjectController pointsPickupsLevel3;
  public SpaceObjectController healthUpgrades;
  public SpaceObjectController movementUpgrades;
  public SpaceObjectController lifePickups;

  public SpaceObjectController[] spawnableObjects;
  private float recurringCheckpointTime = 60f;


	// Use this for initialization
	void Start () {
    InitialisePools();
    GameObject[] checkpointGameObjects = GameObject.FindGameObjectsWithTag("SpawnCheckpoint");
    foreach (GameObject checkpointGameObject in checkpointGameObjects) {
      spawnCheckpoints.Add(checkpointGameObject.GetComponent<SpaceObjectCheckpoint>());
      hasSpawned.Add(false);
    }
    spawnCheckpoints.Sort((a, b) => a.spawnTime.CompareTo(b.spawnTime));
    spawnableObjects = new SpaceObjectController[]{largeAsteroids, mediumAsteroids, smallAsteroids, spaceships, healthPickups, movementPickups, pointsPickupsLevel1, pointsPickupsLevel2, pointsPickupsLevel3, healthUpgrades, movementUpgrades, lifePickups};

    InvokeRepeating("CheckSpawners", 1f, 1f);
	}

	// Update is called once per frame
	void Update () {
	}

  public void InitialisePools()
  {
    largeAsteroids.Spawn();
    mediumAsteroids.Spawn();
    smallAsteroids.Spawn();
    spaceships.Spawn();
    healthPickups.Spawn();
    movementPickups.Spawn();
    pointsPickupsLevel1.Spawn();
    pointsPickupsLevel2.Spawn();
    pointsPickupsLevel3.Spawn();
    healthUpgrades.Spawn();
    movementUpgrades.Spawn();
    lifePickups.Spawn();
  }

  public void CheckSpawners()
  {
    SpaceObjectCheckpoint lastCheckpoint = null;
    for (int i = 0; i < spawnCheckpoints.Count; i ++) {
      lastCheckpoint = spawnCheckpoints[i];
      if (Time.time > spawnCheckpoints[i].spawnTime && !hasSpawned[i]) {
        SpawnCheckpoint(spawnCheckpoints[i]);
        hasSpawned[i] = true;
      }
    }
    if (Time.time > lastCheckpoint.spawnTime + recurringCheckpointTime) {
      SpawnCheckpoint(lastCheckpoint);
      recurringCheckpointTime += 60;
    }
  }

  private void SpawnCheckpoint(SpaceObjectCheckpoint checkpoint)
  {
    largeAsteroids.AddActive(checkpoint.largeAsteroids);
    mediumAsteroids.AddActive(checkpoint.mediumAsteroids);
    smallAsteroids.AddActive(checkpoint.smallAsteroids);
    spaceships.AddActive(checkpoint.spaceships);
    healthPickups.AddActive(checkpoint.healthPickups);
    movementPickups.AddActive(checkpoint.movementPickups);
    pointsPickupsLevel1.AddActive(checkpoint.pointsPickupsLevel1);
    pointsPickupsLevel2.AddActive(checkpoint.pointsPickupsLevel2);
    pointsPickupsLevel3.AddActive(checkpoint.pointsPickupsLevel3);
    if (!player.getHasHealthUpgrade()) {
      healthUpgrades.AddActive(checkpoint.healthUpgrades);
    }
    if (!player.getHasMovementUpgrade()) {
      movementUpgrades.AddActive(checkpoint.movementUpgrades);
    }
    lifePickups.AddActive(checkpoint.lifePickups);
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
