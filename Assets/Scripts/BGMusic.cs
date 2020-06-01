using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class BGMusic : MonoBehaviour
{
    public Sound music;

    public float volume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        music.source = gameObject.AddComponent<AudioSource>();
        music.source.clip = music.clip;

        music.source.loop = music.loop;
        music.source.volume = music.volume;
        music.source.pitch = music.pitch;

        music.source.Play();
    }

    public void OnVolumeChange(float newVolume)
    {
        volume = newVolume;
        music.source.volume = music.volume * volume;
    }
}
