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
        new Dictionary<Colors, List<ColorfulObject>>();

    public static Dictionary<Colors, Ability> colorsAbilityMap = new Dictionary<Colors, Ability>();
    void Start()
    {
        Manager = this;
        foreach (Colors color in Enum.GetValues(typeof(Colors)))
        {
            colorfulObjectsMap[color] = new List<ColorfulObject>();
            colorsAbilityMap[color] = new EmptyAbility();
        }
    }

    
}
