using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private float logoAppearTime;
    
    [SerializeField] private GameObject resetText;
    [SerializeField] private float resetAppearTime;

    [SerializeField] private AudioClip endingTheme;
    [SerializeField] private AudioSource themeMusicSource;

    
    [SerializeField] private GameObject end;

    private Animator[] _animators;
    
    void Start()
    {
        _animators = GetComponentsInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayersManager.Manager.ActivePlayerScript.ResetMovement();
        PlayersManager.Manager.ActivePlayerScript.RemoveMoveables();
        PlayersManager.Manager.ActivePlayerScript.CanMove = false;
        
        if (_animators != null)
        {
            foreach (var animator in _animators)
                animator.SetBool("got", true);
        }
        
        end.SetActive(true);
        StartCoroutine(ActiveLogo());
        StartCoroutine(ActiveReset());
        EndSound();
    }

    private void EndSound()
    {
        themeMusicSource.Stop();
        themeMusicSource.volume = 1;
        themeMusicSource.clip = endingTheme;
        themeMusicSource.Play();
    }
    
    private IEnumerator ActiveLogo()
    {
        yield return new WaitForSeconds(logoAppearTime);
        logo.SetActive(true);
    }

    private IEnumerator ActiveReset()
    {
        yield return new WaitForSeconds(resetAppearTime);
        resetText.SetActive(true);
    }
    
    
}
