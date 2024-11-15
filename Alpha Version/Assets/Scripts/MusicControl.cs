using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public AudioSource musicAudio;

    bool triggered = false;

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        //musicAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.raceActive == false && triggered == false)
        {
            musicAudio.Stop();
            triggered = true;
        }
    }
}
