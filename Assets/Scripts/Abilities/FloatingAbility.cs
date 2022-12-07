using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAbility : MonoBehaviour, Ability
{
    public void RunAbility(ColorfulObject colorfulObject)
    {
        colorfulObject._rigidbody2D.gravityScale = -1;
    }
}