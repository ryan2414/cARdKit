using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFireWorkSound : MonoBehaviour
{
    public AudioSource firework1;
    public AudioSource clearSound;
    public AudioSource bgm_AR;

    public void PlayFirework1()
    {
        firework1.Play();

    }

    public void PlayClearSound()
    {
        bgm_AR.Stop();
        clearSound.Play();
    }
    
}
