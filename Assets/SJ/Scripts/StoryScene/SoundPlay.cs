using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public AudioSource soundWind;
    public AudioSource soundStrongWind;

    public void OnClickWind()
    {
        soundWind.Play();
    }
    public void OnClickStrongWind()
    {
        soundStrongWind.Play();
    }
}
