using UnityEngine;
using System.Collections;

public class Spaceship : SpaceObject {
  public float minShootDistance;
  public float blastDamage;
  public float blastFrequency = 0;
  public float blastSpeed;
  public SpaceObject primaryBlast;
  private GameObject player;

	// Use this for initialization
	void Start () {
    player = GameObject.Find("Player");
    if (blastFrequency > 0) {
      InvokeRepeating("Shoot", blastFrequency, blastFrequency);
    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void Shoot ()
  {
    if (GetComponent<Renderer>().isVisible) {
      float dist = Vector3.Distance(player.transform.position, transform.position);
      if (dist > minShootDistance) {
        SpaceObject blast = (SpaceObject)Instantiate(primaryBlast, transform.position, transform.rotation);
        blast.transform.SetParent(GetComponent<Transform>(), true);
        setInitialValues(blast);
      }
    }
  }

  void setInitialValues(SpaceObject blast)
  {
    blast.damage = blastDamage;
    Vector3 dir = player.transform.position - transform.position;
    Vector3 force = dir.normalized * blastSpeed;
    blast.GetComponent<Rigidbody2D>().velocity = force;
  }

}
