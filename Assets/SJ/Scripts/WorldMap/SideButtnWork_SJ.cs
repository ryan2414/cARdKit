using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideButtnWork_SJ : MonoBehaviour
{
    [SerializeField]
    GameObject Btn_LeftSide;
    
    [SerializeField]
    GameObject Btn_Stage1;

    [SerializeField]
    GameObject Btn_RightSide;

    [SerializeField]
    GameObject Btn_Stage2;

    public int maxChapter;
    int nowChapter = 1;

    AudioSource sound_change;

    #region MH_Image����
    public Image Image_LeftSide;
    public Image Image_RightSide;
    #endregion
    //���� �÷��̾ �ִ� Chapter�� ����ϰ�
    //World ������ ���ƿ��� ��
    //�� Chapter�� �ٷ� ���̰� �ϰ� �ʹ�.
    private void Start()
    {
        sound_change = GameObject.Find("ChChangeSound").GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("playingChpater") == 1)
        {
            OnClickLeftButton();

        }
        else if (PlayerPrefs.GetInt("playingChpater") == 2)
        {
            OnClickRightButton();

        }
        else
        {
            Btn_Stage2.SetActive(false);
            //���� ���������� �Ѿ �� ������ ��ư�� ���´�.
            Image_LeftSide.color = new Color(1, 1, 1, 0.5f);
            Btn_LeftSide.GetComponentInChildren<Button>().interactable = false;
        }
    }

    public void OnClickRightButton()
    {
        if (nowChapter < maxChapter)
        {
            nowChapter++;
            sound_change.Play();
        }

        //��ư�� ������ é�� 2�� Ȱ��ȭ �Ѵ�
        Btn_Stage1.SetActive(false);
        Btn_Stage2.SetActive(true);

        //���� ���� é�Ͱ� ���ٸ�
        if (nowChapter >= maxChapter)
        {
            //���� é�ͷ� �Ѿ �� ������ ��ư�� ���´�
            Image_RightSide.color = new Color(1, 1, 1, 0.5f);
            Btn_RightSide.GetComponentInChildren<Button>().interactable = false;
        }

        //���� é�ͷ� �� �� �ִ� ��ư�� Ȱ��ȭ �Ѵ�.
        Image_LeftSide.color = new Color(1, 1, 1, 1);
        Btn_LeftSide.GetComponentInChildren<Button>().interactable = true;

        PlayerPrefs.SetInt("playingChpater", 2);
    }

    public void OnClickLeftButton()
    {

        if (nowChapter > 1)
        {
            nowChapter--;
            sound_change.Play();
        }


        //��ư�� ������ é�� 1���� ���ư���.
        Btn_Stage2.SetActive(false);
        Btn_Stage1.SetActive(true);

        if (nowChapter <= 1)
        {
            //���� é�ͷ� �Ѿ �� ������ ��ư�� ���´�.
            Image_LeftSide.color = new Color(1, 1, 1, 0.5f);
            Btn_LeftSide.GetComponentInChildren<Button>().interactable = false;
        }

        //é�� 2�� �� �� �ֵ��� ��ư�� Ȱ��ȭ �Ѵ�.
        Image_RightSide.color = new Color(1, 1, 1, 1);
        Btn_RightSide.GetComponentInChildren<Button>().interactable = true;

        PlayerPrefs.SetInt("playingChpater", 1);

    }
}
