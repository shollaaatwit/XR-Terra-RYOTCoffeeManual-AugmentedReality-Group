using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] audioClips;
    private AudioSource _audioSource;

    void Awake()
    {
        PourDetector.onPouringEvent.AddListener(PlayPourSound);
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void PlayPourSound(bool pouring)
    {
        if (pouring)
        {
            PlaySound(0);
        }
    }

    public void PlaySound(int clipNum)
    {
        if (audioClips[clipNum])
        {
            _audioSource.PlayOneShot(audioClips[clipNum]);
        }
    }
}
