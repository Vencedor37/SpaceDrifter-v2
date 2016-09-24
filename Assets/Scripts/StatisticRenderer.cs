using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatisticRenderer : MonoBehaviour {
  public StatTracker stats;
  public PlayerController player;
  public Text healthText;
  public Text movementText;
  public Text pointsText;
  public Text closeCallText;
  public Text tightSqueezeText;
  public Text timeText;
  public Text levelText;
  public Text scoreText;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if (!player.alive) {
      string pad = "    ";
      float time = stats.timeLasted;
      string formattedTime;
      if (time < 3600) {
        formattedTime = string.Format("{0}:{1:00}", (int)time/60, (int)time%60);
      } else {
        formattedTime = System.TimeSpan.FromSeconds((int)time).ToString();
      }
      healthText.text = "x " + stats.healthPickupsCollected;	
      movementText.text = "x " + stats.movementPickupsCollected;
      pointsText.text = "x " + stats.pointsPickupsCollected;
      closeCallText.text = "Close Calls: " + stats.closeCallsMedium;
      tightSqueezeText.text = "Tight Squeezes: " + stats.closeCallsHigh;
      string row1 = "Time: " + formattedTime + pad + "Bonus: x" + stats.highestLevel + pad + "Score: " + stats.score;
      string row3 = "Close Calls: " + stats.closeCallsMedium + pad + "Tight Squeezes: " + stats.closeCallsHigh;
      timeText.text = row1;
      closeCallText.text = row3;
//      levelText.text = "Level: " + stats.highestLevel;
//      scoreText.text = "Score: " + stats.score;
    }
	}
}
