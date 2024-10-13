using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    /*Title: How To Make A HORROR Game In Unity | Flickering Lights | Horror Series Part 010
     
Author: User1 Productions
Date: 31 December 2021
Code Version: 2019.4.28f1
Availability: https://www.youtube.com/watch?v=oOmAB0rs518&t=218s*/

    public Light Light;

    public float MinTime;
    public float MaxTime;
    public float Timer;

    public AudioSource AudioSource;
    public AudioClip LightAudio;

    // Start is called before the first frame update
    void Start()
    {
        Timer = Random.Range(MinTime, MaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        LightFlicker();
    }

    void LightFlicker ()
    {
        if(Timer > 0)

            Timer -= Time.deltaTime;

            if(Timer <= 0)
            {
                Light.enabled = ! Light.enabled;
                Timer = Random.Range(MinTime, MaxTime);
                AudioSource.PlayOneShot(LightAudio);
            }

    }
}