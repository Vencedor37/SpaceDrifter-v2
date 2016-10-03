using UnityEngine;
using System.Collections;

public class Spaceship : SpaceObject {
  public AudioTracks audioTracks;
  public float minShootDistance;
  public float blastDamage;
  public float blastFrequency = 0;
  public float blastSpeed;
  public SpaceObject primaryBlast;
  private GameObject player;
  private bool fireOnceOnly = false;
  private bool hasFired;

	// Use this for initialization
	void Start () {
    player = GameObject.FindWithTag("Player");
    audioTracks = GameObject.FindWithTag("Audio").GetComponent<AudioTracks>();
    if (blastFrequency > 0) {
      InvokeRepeating("Shoot", blastFrequency, blastFrequency);
    }
	}

	// Update is called once per frame
	void Update () {

	}

  void Shoot ()
  {
    if (!hasFired || !fireOnceOnly) {

      if (GetComponent<Renderer>().isVisible && gameObject.transform.rotation == Quaternion.identity) {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > minShootDistance) {
          audioTracks.spaceshipBlast.Play();
          BlastObject blast = (BlastObject)Instantiate(primaryBlast, transform.position, transform.rotation);
          blast.transform.SetParent(GetComponent<Transform>(), true);
          blast.playerT = player.transform;
          blast.player = player.GetComponent<PlayerController>();
          setInitialValues(blast);
          hasFired = true;
        }
      }
    }
  }

  void setInitialValues(SpaceObject blast)
  {
    blast.damage = blastDamage;
    Vector2 dir = new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(transform.position.x, transform.position.y);
    Vector2 force = dir.normalized * blastSpeed;
    force = CalculateShot(force);

    blast.GetComponent<Rigidbody2D>().velocity = force;
  }


  Vector2 CalculateShot(Vector2 velocity)
  {
    float timeThreshold = 0.2f;
    Vector2 targetPos = new Vector2(player.transform.position.x, player.transform.position.y);
    Vector2 origin = new Vector2(transform.position.x, transform.position.y);
    float timeToPlayer;
    timeToPlayer = TimeToReachTarget(targetPos, origin, velocity);
    Vector2 predictedPoint = EstimatePlayerPosition(timeToPlayer);
    Vector2 newDir = predictedPoint - origin;
    Vector2 force = newDir.normalized * blastSpeed;
    float newTimeToPlayer = TimeToReachTarget(predictedPoint, origin, force);

    int i = 0;
    while (!(Mathf.Abs(newTimeToPlayer - timeToPlayer) < timeThreshold) && i < 5) {
      timeToPlayer = newTimeToPlayer;
      predictedPoint = EstimatePlayerPosition(timeToPlayer);
      newDir = predictedPoint - origin;
      force = newDir.normalized * blastSpeed;
      newTimeToPlayer = TimeToReachTarget(predictedPoint, origin, force);
      i++;
    }

    return force;
  }

  float TimeToReachTarget(Vector2 target, Vector2 origin, Vector2 velocity)
  {
    // distance = velocity * time
    // time = distance / velocity
    float distance = Vector3.Distance(target, origin);
    float time = distance / velocity.magnitude;
    return time;
  }

  Vector2 EstimatePlayerPosition(float time)
  {
    Rigidbody2D rigidbody = player.gameObject.GetComponent<Rigidbody2D>();
    Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
    return playerPos + rigidbody.velocity * time;
  }

  public IEnumerator BlowUp( )
  {
    audioTracks.spaceshipExplosionSource.Play();
    int points = gameObject.GetComponent<SpaceObject>().getPointsBonus();
    float newAlpha = 0;
    float dieTimeLimit = 0.15f;
    float dieTimeCount = 0;
    while (dieTimeCount < dieTimeLimit) {
      dieTimeCount += Time.deltaTime;
      GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, newAlpha);
      newAlpha = newAlpha == 0 ? 1 : 0;
      yield return new WaitForSeconds(0.10f);
    }
    GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    GetComponent<Rigidbody2D>().isKinematic = false;
    gameObject.SetActive(false);
    player.GetComponent<PlayerController>().StartBonus(points, "UFO");
  }

}
