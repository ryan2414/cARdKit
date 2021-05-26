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

    private void Start()
    {
        int stageNum = FlagManager.instance.stageNum;

        switch (stageNum)
        {
            case 1:
                Sh_QstTxt.text = "ī�带 ������ �ùٸ� ��ġ�� ����!";

                    break;
            case 2:
                Sh_QstTxt.text = "1. ī�带 ������ �ùٸ� ��ġ�� ����!\n2.ȭ���� ����ġ�� ��ġ�ؼ� ȸ�θ� ��������!";
                    break;
            case 3:
                Sh_QstTxt.text = "1. ī�带 ������ �ùٸ� ��ġ�� ����!\n2.ȭ���� ����ġ�� ��ġ�ؼ� ȸ�θ� ��������!\n3.���������� �����̸� ������ ���� �� ����� ����!";
                    break;
            case 5:
                Sh_QstTxt.text = "1. ī�带 ������ �ùٸ� ��ġ�� ����!\n2.ȭ���� ����ġ�� ��ġ�ؼ� ȸ�θ� ��������!";
                break;
            case 6:
                Sh_QstTxt.text = "1. ī�带 ������ �ùٸ� ��ġ�� ����!\n2.ȭ���� ����ġ�� ��ġ�ؼ� ȸ�θ� ��������!\n3.���������� �����̸� ������ ��ǳ�⸦ �� ������ �غ���";
                break;

            default:
                break;
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
