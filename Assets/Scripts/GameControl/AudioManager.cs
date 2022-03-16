using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source)
    {
        audioSource = source;
        audioClips.Clear();
        audioClips.Add(AudioClipName.AsteroidHit, 
            Resources.Load<AudioClip>("Sound/hit"));
        audioClips.Add(AudioClipName.PlayerDeath,
            Resources.Load<AudioClip>("Sound/die"));
        audioClips.Add(AudioClipName.PlayerShot,
            Resources.Load<AudioClip>("Sound/shoot"));
        audioClips.Add(AudioClipName.IonBlaster,
            Resources.Load<AudioClip>("Sound/Ion Blaster"));
        audioClips.Add(AudioClipName.LaserCannon,
            Resources.Load<AudioClip>("Sound/Laser Cannon"));
        audioClips.Add(AudioClipName.EatDrumStick,
            Resources.Load<AudioClip>("Sound/Eat Drumstick"));
        audioClips.Add(AudioClipName.NeutronGun,
            Resources.Load<AudioClip>("Sound/Neutron Gun"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
