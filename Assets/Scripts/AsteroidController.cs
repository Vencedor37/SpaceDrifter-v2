using UnityEngine;
using System.Collections;

public class AsteroidController : SpaceObjectController {
  public float scale = 1;
  public float minSpeed = 1;
  public float maxSpeed = 1;

  override public void setInitialValues(SpaceObject spaceObject)
  {
    spaceObject.AdjustSize(scale);
    spaceObject.AdjustSpeed(minSpeed, maxSpeed);
    spaceObject.AdjustMass(scale);
    if (overrideSprite != null) {
      SpriteRenderer spriteRenderer = spaceObject.GetComponent<SpriteRenderer>();
      spriteRenderer.sprite = overrideSprite;
    }
  }

}
