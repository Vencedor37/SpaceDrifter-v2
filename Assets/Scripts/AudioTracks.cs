using UnityEngine;
using System.Collections;

public class AudioTracks : MonoBehaviour {
  public AudioSource spraySource;
  public AudioSource enemySource;
  public AudioSource levelUpSource;
  public AudioSource fuelSource;
  public AudioSource oxygenSource;
  public AudioSource pointsSource;
  public AudioSource spaceshipBlast;
  public AudioSource upgradePowerUpSource;
  public AudioSource hitFromBlastSource;
  public AudioSource spaceshipExplosionSource;
  public AudioSource newLifeSource;

  public AudioSource backgroundMusic;
  private float backgroundVolume = 1;


	// Use this for initialization
	void Start () {
    backgroundVolume = backgroundMusic.volume;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void ResetBackgroundVolume()
  {
    backgroundVolume = .4f;
    backgroundMusic.volume = backgroundVolume;
  }


  public void BackgroundFadeOut()
  {
    if (backgroundMusic.volume > 0) {
      backgroundVolume -= .2f * Time.unscaledDeltaTime;
      backgroundMusic.volume = backgroundVolume;
    } else if (backgroundMusic.volume <= 0) {
      backgroundMusic.Stop();
    }
  }

}
