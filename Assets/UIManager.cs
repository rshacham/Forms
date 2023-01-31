using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pcSprites;
    [SerializeField] private GameObject[] joystickSpritesOld;

    [SerializeField] private GameObject[] activePlayers;
    [SerializeField] private Sprite[] activePlayersSprites;
    [SerializeField] private Sprite[] inactivePlayerSprites;

    [SerializeField] private GameObject[] joystickSprites;
    [SerializeField] private GameObject[] activePlayersNew;
    [SerializeField] private GameObject[] inactivePlayersNew;

    [SerializeField] private Color colorInactive;

    private ColorsManager colorsManager;
    
    public static UIManager Manager;
    void Start()
    {
        Manager = this;
        JoystickHandler();
        colorsManager = FindObjectOfType<ColorsManager>();
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
            inactivePlayersNew[i] = joystickSprites[i];
        }
    }
    
    private void colorsActivePlayers()
    {
        activePlayersNew[0].GetComponent<Image>().color = colorsManager.colorCircle;
        activePlayersNew[1].GetComponent<Image>().color = colorsManager.colorSquare;
        activePlayersNew[2].GetComponent<Image>().color = colorsManager.colorTriangle;
    }

    public void ChangeActivePlayerUI(string activePlayerName)
    {
        // ResetActivePlayerUI();
        // switch (activePlayerName)
        // {
        //     case "Circle":
        //         activePlayers[0].GetComponent<Image>().sprite = activePlayersSprites[0];
        //         break;
        //     case "Square":
        //         activePlayers[1].GetComponent<Image>().sprite = activePlayersSprites[1];
        //         break;
        //     case "Triangle":
        //         activePlayers[2].GetComponent<Image>().sprite = activePlayersSprites[2];
        //         break;
        // }
        
        
        ResetActivePlayerUI();
        switch (activePlayerName)
        {
            case "Circle":
                activePlayersNew[0].SetActive(true);
                inactivePlayersNew[0].SetActive(false);
                
                //activePlayers[0].GetComponent<Image>().sprite = activePlayersSpritesNew[0];
                break;
            case "Square":
                activePlayersNew[1].SetActive(true);
                inactivePlayersNew[1].SetActive(false);
                
                //activePlayers[1].GetComponent<Image>().sprite = activePlayersSpritesNew[1];
                break;
            case "Triangle":
                activePlayersNew[2].SetActive(true);
                inactivePlayersNew[2].SetActive(false);
                
                //activePlayers[2].GetComponent<Image>().sprite = activePlayersSpritesNew[2];
                break;
        }
    }

    public void ResetActivePlayerUI()
    {
        // for (var i = 0; i < 3; i++)
        // {
        //     activePlayers[i].GetComponent<Image>().sprite = inactivePlayerSprites[i];
        // }
        
        for (var i = 0; i < 3; i++)
        {
            activePlayersNew[i].SetActive(false);
            inactivePlayersNew[i].gameObject.SetActive(true);
            inactivePlayersNew[i].GetComponent<Image>().color = colorInactive;
        }
    }
}
