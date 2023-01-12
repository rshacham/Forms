using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToCircleGuide : Guide
{
    protected override void Update()
    {
        base.Update();
        conditionToNextGuide = GameManager.Manager.HasChangedToCircle;
    }
}
