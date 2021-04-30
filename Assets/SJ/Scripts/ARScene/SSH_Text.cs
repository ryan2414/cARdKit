using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSH_Text : MonoBehaviour
{
    public Animator Ani_anim;
    // Start is called before the first frame update
    void Start()
    {

    }
    bool isPlayed;

    public void OnClickText()
    {
        if (!isPlayed)
        {
            Ani_anim.SetTrigger("isFinish");
            isPlayed = true;

        }
        //BTN_SSH_Text.gameObject.SetActive(false);
    }
}
