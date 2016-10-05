using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioTracks : MonoBehaviour {
  public UIController UI;
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
  public float backgroundCurrentVolume = 1;
  public float backgroundMax = 0;

  public float sprayVolume = 1;
  public float enemyVolume = 1;
  public float levelUpVolume = 1;
  public float fuelVolume = 1;
  public float oxygenVolume = 1;
  public float pointsVolume = 1;
  public float spaceshipBlastVolume = 1;
  public float upgradePowerUpVolume = 1;
  public float hitFromBlastVolume = 1;
  public float spaceshipExplosionVolume = 1;
  public float newLifeVolume = 1;


	// Use this for initialization
	void Start () {
    backgroundMax = backgroundMusic.volume;
    backgroundCurrentVolume = backgroundMax;
    InitialSetVolumes();
    InvokeRepeating("CheckVolumesProd", 0.5f, 0.5f);
	}

	// Update is called once per frame
	void Update () {

	}

  void InitialSetVolumes()
  {
    enemySource.volume = enemyVolume * UI.muteStatus;
    fuelSource.volume = fuelVolume * UI.muteStatus;
    hitFromBlastSource.volume = hitFromBlastVolume * UI.muteStatus;
    levelUpSource.volume = levelUpVolume * UI.muteStatus;
    newLifeSource.volume = newLifeVolume * UI.muteStatus;
    oxygenSource.volume = oxygenVolume * UI.muteStatus;
    pointsSource.volume = pointsVolume * UI.muteStatus;
    spaceshipBlast.volume = spaceshipBlastVolume * UI.muteStatus;
    spaceshipExplosionSource.volume = spaceshipExplosionVolume * UI.muteStatus;
    spraySource.volume = sprayVolume * UI.muteStatus;
    upgradePowerUpSource.volume = upgradePowerUpVolume * UI.muteStatus;
  }


  public void ResetBackgroundVolume()
  {
    backgroundCurrentVolume = backgroundMax;
    backgroundMusic.volume = backgroundCurrentVolume;
  }


  public void BackgroundFadeOut()
  {
    if (backgroundMusic.volume > 0) {
      backgroundCurrentVolume -= .1f * Time.unscaledDeltaTime;
      backgroundMusic.volume = backgroundCurrentVolume;
    } else if (backgroundMusic.volume <= 0) {
      backgroundMusic.Stop();
    }
  }

  void CheckVolumesProd()
  {
    enemySource.volume = enemyVolume * UI.muteStatus;
    fuelSource.volume = fuelVolume * UI.muteStatus;
    hitFromBlastSource.volume = hitFromBlastVolume * UI.muteStatus;
    levelUpSource.volume = levelUpVolume * UI.muteStatus;
    newLifeSource.volume = newLifeVolume * UI.muteStatus;
    oxygenSource.volume = oxygenVolume * UI.muteStatus;
    pointsSource.volume = pointsVolume * UI.muteStatus;
    spaceshipBlast.volume = spaceshipBlastVolume * UI.muteStatus;
    spaceshipExplosionSource.volume = spaceshipExplosionVolume * UI.muteStatus;
    spraySource.volume = sprayVolume * UI.muteStatus;
    upgradePowerUpSource.volume = upgradePowerUpVolume * UI.muteStatus;
    backgroundMusic.volume = backgroundCurrentVolume * UI.muteStatus;
  }

  void CheckVolumesDebug()
  {
    float oldEnemyVolume = enemySource.volume;
    enemySource.volume = enemyVolume;
    if (oldEnemyVolume != enemySource.volume) {
      enemySource.Play();
    }
    float oldFuelVolume = fuelSource.volume;
    fuelSource.volume = fuelVolume;
    if (oldFuelVolume != fuelSource.volume) {
      fuelSource.Play();
    }
    float oldHitFromBlastVolume = hitFromBlastSource.volume;
    hitFromBlastSource.volume = hitFromBlastVolume;
    if (oldHitFromBlastVolume != hitFromBlastSource.volume) {
      hitFromBlastSource.Play();
    }
    float oldLevelUpVolume = levelUpSource.volume;
    levelUpSource.volume = levelUpVolume;
    if (oldLevelUpVolume != levelUpSource.volume) {
      levelUpSource.Play();
    }
    float oldNewLifeVolume = newLifeSource.volume;
    newLifeSource.volume = newLifeVolume;
    if (oldNewLifeVolume != newLifeSource.volume) {
      newLifeSource.Play();
    }
    float oldOxygenVolume = oxygenSource.volume;
    oxygenSource.volume = oxygenVolume;
    if (oldOxygenVolume != oxygenSource.volume) {
      oxygenSource.Play();
    }
    float oldPointsVolume = pointsSource.volume;
    pointsSource.volume = pointsVolume;
    if (oldPointsVolume != pointsSource.volume) {
      pointsSource.Play();
    }
    float oldSpaceshipBlastVolume = spaceshipBlast.volume;
    spaceshipBlast.volume = spaceshipBlastVolume;
    if (oldSpaceshipBlastVolume != spaceshipBlast.volume) {
      spaceshipBlast.Play();
    }
    float oldSpaceshipExplosionVolume = spaceshipExplosionSource.volume;
    spaceshipExplosionSource.volume = spaceshipExplosionVolume;
    if (oldSpaceshipExplosionVolume != spaceshipExplosionSource.volume) {
      spaceshipExplosionSource.Play();
    }
    float oldSprayVolume = spraySource.volume;
    spraySource.volume = sprayVolume;
    if (oldSprayVolume != spraySource.volume) {
      spraySource.Play();
    }
    float oldUpgradePowerUpVolume = upgradePowerUpSource.volume;
    upgradePowerUpSource.volume = upgradePowerUpVolume;
    if (oldUpgradePowerUpVolume != upgradePowerUpSource.volume) {
      upgradePowerUpSource.Play();
    }
  }

}
