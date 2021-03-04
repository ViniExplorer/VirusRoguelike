using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = 1f;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string soundName)
    {
        Sound soundy = Array.Find(sounds, sound => sound.name == soundName);
        if (soundy == null)
            return;
        soundy.source.Play();
    }
}
