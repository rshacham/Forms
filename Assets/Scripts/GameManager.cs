using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;
    public enum Colors
    {
        Red,
        Blue,
        Green
    };

    public static Dictionary<Colors, List<ColorfulObject>> colorfulObjectsMap =
        new();

    public static Dictionary<Colors, Ability> colorsAbilityMap = new();
    void Awake()
    {
        Manager = this;
        foreach (Colors color in Enum.GetValues(typeof(Colors)))
        {
            colorfulObjectsMap[color] = new List<ColorfulObject>();
            colorsAbilityMap[color] = gameObject.AddComponent<EmptyAbility>();
        }
    }

    
}
