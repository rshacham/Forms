using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAbilityColor : MonoBehaviour
{
    [SerializeField] private GameManager.Colors color;
    [SerializeField] private String ability;

    private Ability desiredAbility;
    
    private AbilitiesFactory factory;

    public void Start()
    {
        factory = new AbilitiesFactory();
    }

    public void ChangeAbility()
    {
        desiredAbility = factory.CreateAbilities(ability);
        GameManager.colorsAbilityMap[color] = desiredAbility;
        foreach (var obj in GameManager.colorfulObjectsMap[color])
        {
            GameManager.colorsAbilityMap[color].RunAbility(obj);
        }
    }
}
