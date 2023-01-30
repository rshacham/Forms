using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int sceneNum;
    [SerializeField] private string scene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (scene != "")
        {
            SceneManager.LoadScene(scene);
            return;
        }

        SceneManager.LoadScene(sceneNum);
    }
}
