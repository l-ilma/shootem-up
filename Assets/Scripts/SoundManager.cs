using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { private set; get; }

    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }
    

    public void PlaySound(AudioClip source)
    {
        _audioSource.PlayOneShot(source);
    }
}
