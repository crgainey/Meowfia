using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Pauses menu music in levels
        GameObject menuMusic = GameObject.FindWithTag("MenuMusic");
        if (menuMusic != null)
        {
            AudioSource menuAudio = menuMusic.GetComponent<AudioSource>();
            if (menuAudio != null)
            {
                menuAudio.mute = true;
            }
        }
    }

}
