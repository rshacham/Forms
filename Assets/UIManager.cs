using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pcSprites;
    [SerializeField] private GameObject[] joystickSprites;

    [SerializeField] private GameObject[] activePlayers;
    [SerializeField] private Sprite[] activePlayersSprites;
    [SerializeField] private Sprite[] inactivePlayerSprites;
    

    public static UIManager Manager;
    void Start()
    {
        Manager = this;
        if (Input.GetJoystickNames().Length > 0)
        {
            SwitchToJoystickUI();
        }
    }

    private void SwitchToJoystickUI()
    {
        foreach (var sprite in pcSprites)
        {
            sprite.SetActive(false);
        }

        foreach (var sprite in joystickSprites)
        {
            sprite.SetActive(true);
        }
    }

    public void ChangeActivePlayerUI(string activePlayerName)
    {
        ResetActivePlayerUI();
        switch (activePlayerName)
        {
            case "Circle":
                activePlayers[0].GetComponent<Image>().sprite = activePlayersSprites[0];
                break;
            case "Square":
                activePlayers[1].GetComponent<Image>().sprite = activePlayersSprites[1];
                break;
            case "Triangle":
                activePlayers[2].GetComponent<Image>().sprite = activePlayersSprites[2];
                break;
        }
    }

    public void ResetActivePlayerUI()
    {
        for (var i = 0; i < 3; i++)
        {
            activePlayers[i].GetComponent<Image>().sprite = inactivePlayerSprites[i];
        }
    }
}
