using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundParameters : MonoBehaviour
{
    private static SoundParameters instance;

    public float volumeEffect = 0.5f;
    public float volumeMusic = 0.5f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void OnVolumeEffectChanged(float newVolume)
    {
        volumeEffect = newVolume;
        Audio.AudioManager am;
        if ((am = FindObjectOfType<Audio.AudioManager>()) != null)
        {
            am.OnVolumeChange(volumeEffect);
        }
    }

    public void OnVolumeMusicChanged(float newVolume)
    {
        volumeMusic = newVolume;
        BGMusic bg;
        if ((bg = FindObjectOfType<BGMusic>()) != null)
        {
            bg.OnVolumeChange(volumeMusic);
        }
    }
}
