using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Manager;

    private AudioSource _audioSource;
    void Start()
    {
        Manager = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
    }

    public void PlayRandomSound(AudioClip[] possibleClips)
    {
        if (possibleClips.Length == 0)
        {
            return;
        }
        
        var randomClipIndex = Random.Range(0, possibleClips.Length);
        print(randomClipIndex);
        
        _audioSource.PlayOneShot(possibleClips[randomClipIndex]);
    }

}
