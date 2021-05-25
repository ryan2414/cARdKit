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

    #region MH_Image편집
    public Image Image_LeftSide;
    public Image Image_RightSide;
    #endregion
    //현재 플레이어가 있는 Chapter를 기억하고
    //World 맵으로 돌아왔을 때
    //그 Chapter가 바로 보이게 하고 싶다.
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
            //이전 스테이지로 넘어갈 수 없도록 버튼을 막는다.
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

        //버튼을 누르면 챕터 2를 활성화 한다
        Btn_Stage1.SetActive(false);
        Btn_Stage2.SetActive(true);

        //만약 다음 챕터가 없다면
        if (nowChapter >= maxChapter)
        {
            //다음 챕터로 넘어갈 수 없도록 버튼을 막는다
            Image_RightSide.color = new Color(1, 1, 1, 0.5f);
            Btn_RightSide.GetComponentInChildren<Button>().interactable = false;
        }

        //이전 챕터로 갈 수 있는 버튼을 활성화 한다.
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


        //버튼을 누르면 챕터 1으로 돌아간다.
        Btn_Stage2.SetActive(false);
        Btn_Stage1.SetActive(true);

        if (nowChapter <= 1)
        {
            //이전 챕터로 넘어갈 수 없도록 버튼을 막는다.
            Image_LeftSide.color = new Color(1, 1, 1, 0.5f);
            Btn_LeftSide.GetComponentInChildren<Button>().interactable = false;
        }

        //챕터 2로 갈 수 있도록 버튼을 활성화 한다.
        Image_RightSide.color = new Color(1, 1, 1, 1);
        Btn_RightSide.GetComponentInChildren<Button>().interactable = true;

        PlayerPrefs.SetInt("playingChpater", 1);

    }
}
