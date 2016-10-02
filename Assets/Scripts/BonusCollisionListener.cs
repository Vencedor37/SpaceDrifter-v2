using UnityEngine;
using System.Collections;

public class BonusCollisionListener : MonoBehaviour {

  public Transform bonusCircleHigh;
  public Transform bonusCircleMedium;
  public string outerLayer = "Bonus_Medium";

  public float highScale;
  public float mediumScale;

  public bool recentDamage = false;
  public bool recentPoints = false;

  public int mediumBonus = 25;
  public int highBonus = 50;

	// Use this for initialization
	void Start () {
    bonusCircleHigh.localScale = new Vector3(highScale, highScale, 0);
    bonusCircleMedium.localScale = new Vector3(mediumScale, mediumScale, 0);
	}

	// Update is called once per frame
	void Update () {

	}

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Player")) {
      recentDamage = true;
    }
  }

  public void onChildTriggerExit2D(Collider2D other, string name) {
    if (other.gameObject.CompareTag("Player")) {
      PlayerController player = other.gameObject.GetComponent<PlayerController>();
      if (!recentDamage && !recentPoints && !player.getIsInvincible()) {
        if (name == "Bonus_High") {
          recentPoints = true;
          player.StartBonus(highBonus, "Tight Squeeze");
        } else if (name == "Bonus_Medium") {
          recentPoints = true;
          player.StartBonus(mediumBonus, "Close Call");
        }
      }
      if (name == outerLayer) {
        recentPoints = false;
        recentDamage = false;
      }
    }
  }

}
