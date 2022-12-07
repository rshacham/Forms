using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesFactory : MonoBehaviour
{
    public Ability CreateAbilities(String ability)
    {
        if (ability.Equals("Empty"))
        {
            return new EmptyAbility();
        }
        if (ability.Equals("Floating"))
        {
            return new FloatingAbility();
        }

        return null;
    }
}
