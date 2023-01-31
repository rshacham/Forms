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
        if (activePlayersNew.Length == 0)
        {
            return;
        }
        
        JoystickHandler();
        ColorsActivePlayers();
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
    
    public void ColorsActivePlayers()
    {
        for (var i = 0; i < 3; i++)
        {
            ColorsActiveSinglePlayer(i);
        }
    }

    public void ColorsActiveSinglePlayer(int index)
    {
        activePlayersNew[index].GetComponent<Image>().color = ColorsManager.Manager.colorCircle;
    }

    public void ChangeActivePlayerUI(string activePlayerName)
    {
        if (activePlayersNew.Length == 0)
        {
            return;
        }
        
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
        for (var i = 0; i < activePlayersNew.Length; i++)
        {
            if (i == 1 && !GameManager.Manager.HasChangedToSquare)
            {
                continue;
            }

            if (i == 2 && !GameManager.Manager.HasChangedToTriangle)
            {
                continue;
            }
            
            activePlayersNew[i].SetActive(false);
            inactivePlayersNew[i].gameObject.SetActive(true);
            inactivePlayersNew[i].GetComponent<Image>().color = colorInactive;
        }
    }

    public void MakeTransparentUI(string playerName, int opacity)
    {
        var color = new Color(1, 1, 1, 0);
        
        switch (playerName)
        {
            case "Circle":
                activePlayersNew[0].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                inactivePlayersNew[0].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                break;
            
            case "Square":
                activePlayersNew[1].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                inactivePlayersNew[1].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                break;
            
            case "Triangle":
                activePlayersNew[2].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                inactivePlayersNew[2].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                break;
        }
    }
}
