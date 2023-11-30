using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static List<AudioSource> PlayerShootingSound = new List<AudioSource>();
    public static bool playerShootingSoundPlayable;

    public static bool PlayPlayerShooting(AudioSource source)
    {
        for (int x = 0; x < PlayerShootingSound.Count; x++)
        {
            if (PlayerShootingSound[x].isPlaying)
            {
                return false;
            }
        }
        source.Play();
        return true;
    }

}
