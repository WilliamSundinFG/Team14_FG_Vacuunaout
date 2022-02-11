using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenyAudio : MonoBehaviour
{
    private AudioManager audio;

    void Start()
    
    {
        audio= AudioManager.Instance;
        audio.PlayAudio(audio.GameSounds.MenuSounds[0], AudioType.Music);
    }

    public void PlayButtonSound()
    {
        audio.PlayAudio(audio.GameSounds.MenuSounds[1], AudioType.SFX);
    }
}
