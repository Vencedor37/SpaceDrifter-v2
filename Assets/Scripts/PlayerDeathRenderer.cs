using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDeathRenderer : MonoBehaviour {
  public Text causeOfDeathText;
  public Text livesRemainingText;
  public Text scoreText;
  public PlayerController player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if (!player.getAlive() && !player.getIsGameOver()) {
      string causeOfDeath = player.getCauseOfDeath();
      switch (causeOfDeath) {
        case "oxygen":
          causeOfDeathText.text = "Ran out of oxygen";
          break;
        case "asteroid":
          causeOfDeathText.text = "Hit by an asteroid";
          break;
        case "spaceship":
          causeOfDeathText.text = "Rammed by a UFO";
          break;
        case "spaceship_blast":
          causeOfDeathText.text = "Shot by a UFO";
          break;
        default:
          causeOfDeathText.text = "";
          break;
      }
      livesRemainingText.text = "x " + player.getCurrentLives();
      scoreText.text = "Score: " + player.getCurrentScore() + "\n\n" + "High Score: " + player.getHighScore();

    }
	}
}
