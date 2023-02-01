using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int sceneNum;
    [SerializeField] private string scene;

    private DropPlayer _dropPlayer;
    private void Start()
    {
        _dropPlayer = FindObjectOfType<DropPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(GameManager.Manager.Fade.StartFade(true, sceneNum, scene, _dropPlayer));
        PlayersManager.Manager.ActivePlayerScript.CanMove = false;
    }
}
