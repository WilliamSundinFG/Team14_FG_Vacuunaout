using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningScneAudio : MonoBehaviour
{
    private AudioManager audio;
    private AudioSource playingAtTheMoment;

    void Start()
    
    {
        audio= AudioManager.Instance;
        playingAtTheMoment = audio.PlayAudio(audio.GameSounds.MenuSounds[2], AudioType.Music, true);
    }

    public void PlayButtonSound()
    {
        audio.PlayAudio(audio.GameSounds.MenuSounds[1], AudioType.SFX);
    }

    public void DisableSelf()
    {
        audio.ambFade = true;
        
        Destroy(playingAtTheMoment);
    }
}
