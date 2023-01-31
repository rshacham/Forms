using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private GameObject[] joystickInactivePlayersNew;
    [SerializeField] private GameObject[] joystickActivePlayersNew;
    [SerializeField] private GameObject[] activePlayersNew;
    [SerializeField] private GameObject[] inactivePlayersNew;

    [SerializeField] private Color colorInactive;
    
    public static UIManager Manager;

    private void Awake()
    {
        Manager = this;
    }

    void Start()
    {
        JoystickHandler();
        colorsActivePlayers();
        for (var i = 0; i < 3; i++)
        {
            inactivePlayersNew[i].gameObject.SetActive(true);
        }
    }

    private void JoystickHandler()
    {
        String[] joysticks = Input.GetJoystickNames();
        int lenJoystick = joysticks.Length;
        bool isJoystick = false; 
        if (lenJoystick > 0)
        {
            for (int i = 0; i < lenJoystick; i++)
            {
                if (!joysticks[i].Equals(""))
                {
                    isJoystick = true;
                    break;
                }
            }
            if (isJoystick)
            {
                SwitchToJoystickUI();
            }
        }
    }
    
    private void SwitchToJoystickUI()
    {
        for (var i = 0; i < 3; i++)
        {
            inactivePlayersNew[i] = joystickInactivePlayersNew[i];
            activePlayersNew[i] = joystickActivePlayersNew[i];
        }
    }
    
    private void colorsActivePlayers()
    {
        activePlayersNew[0].GetComponent<Image>().color = ColorsManager.Manager.colorCircle;
        activePlayersNew[1].GetComponent<Image>().color = ColorsManager.Manager.colorSquare;
        activePlayersNew[2].GetComponent<Image>().color = ColorsManager.Manager.colorTriangle;
    }

    public void ChangeActivePlayerUI(string activePlayerName)
    {
        ResetActivePlayerUI();
        switch (activePlayerName)
        {
            case "Circle":
                activePlayersNew[0].SetActive(true);
                inactivePlayersNew[0].SetActive(false);
                break;
            case "Square":
                activePlayersNew[1].SetActive(true);
                inactivePlayersNew[1].SetActive(false);
                break;
            case "Triangle":
                activePlayersNew[2].SetActive(true);
                inactivePlayersNew[2].SetActive(false);
                break;
        }
    }

    public void ResetActivePlayerUI()
    {
        for (var i = 0; i < 3; i++)
        {
            activePlayersNew[i].SetActive(false);
            inactivePlayersNew[i].gameObject.SetActive(true);
            inactivePlayersNew[i].GetComponent<Image>().color = colorInactive;
        }
    }
}
