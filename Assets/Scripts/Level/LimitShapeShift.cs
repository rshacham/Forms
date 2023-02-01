using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitShapeShift : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayersManager.Manager.CanChangeShape = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayersManager.Manager.CanChangeShape = true;
    }
}
