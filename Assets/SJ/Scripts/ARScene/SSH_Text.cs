using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSH_Text : MonoBehaviour
{
    public Animator Ani_anim;
    public AudioSource soundWhip;
    bool isPlayed;
    bool isStartAnim;

    public Text Sh_QstTxt;
    public GameObject gm;

    private void Start()
    {
        int stageNum = FlagManager.instance.stageNum;

        switch (stageNum)
        {
            case 1:
                Sh_QstTxt.text = "1. 카드를 가져다 올바른 위치에 놓자!";

                    break;
            case 2:
                Sh_QstTxt.text = "1. 카드를 가져다 올바른 위치에 놓자!\n2. 화면의 스위치를 터치해서 회로를 연결하자!";
                    break;
            case 3:
                Sh_QstTxt.text = "1. 카드를 가져다 올바른 위치에 놓자!\n2. 화면의 스위치를 터치해서 회로를 연결하자!\n3. 가변저항의 손잡이로 불을 더 밝게해 보자!";
                    break;
            case 5:
                Sh_QstTxt.text = "1. 카드를 가져다 올바른 위치에 놓자!\n2. 화면의 스위치를 터치해서 회로를 연결하자!";
                break;
            case 6:
                Sh_QstTxt.text = "1. 카드를 가져다 올바른 위치에 놓자!\n2. 화면의 스위치를 터치해서 회로를 연결하자!\n3. 가변저항의 손잡이로 선풍기를 더 빠르게 해보자!";
                break;

            default:
                break;
        }
    }
    private void Update()
    {
        if (gm.GetComponent<ClearUIActive_SJ>().isStageClear == true)
        {
            gameObject.SetActive(false);
        }
    }


    public void OnClickActQst()
    {
        if (isPlayed) {
            Ani_anim.SetTrigger("isStart");
            isPlayed = false;
        }
        
    }
    public void FinishAnim()
    {
        if (!isStartAnim)
        {
            Ani_anim.SetTrigger("isFinish");
            isStartAnim = true;
            isPlayed = true;
        }
    }
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
