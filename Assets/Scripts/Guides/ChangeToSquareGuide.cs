using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToSquareGuide : Guide
{
    protected override void Update()
    {
        base.Update();
        conditionToNextGuide = GameManager.Manager.HasChangedToSquare;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayersManager.Manager.CanSquare = true;
    }
}
