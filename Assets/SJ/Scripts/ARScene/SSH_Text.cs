using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSH_Text : MonoBehaviour
{
    public Animator Ani_anim;
    public AudioSource soundWhip;
    bool isPlayed;

    public void OnClickText()
    {
        if (!isPlayed)
        {
            Ani_anim.SetTrigger("isFinish");
            isPlayed = true;

        }
    }

    public void WhipSound()
    {
        soundWhip.Play();
    }
}
