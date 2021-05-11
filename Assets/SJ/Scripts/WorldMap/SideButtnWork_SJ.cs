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
    int nowChapter =1;

    private void Start()
    {
        Btn_Stage2.SetActive(false);
        //이전 스테이지로 넘어갈 수 없도록 버튼을 막는다.
        Btn_LeftSide.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
        Btn_LeftSide.GetComponentInChildren<Button>().interactable = false;
    }
    public void OnClickRightButton()
    {
        nowChapter++;

        //버튼을 누르면 스테이지 2를 활성화 한다
        Btn_Stage1.SetActive(false);
        Btn_Stage2.SetActive(true);
        //만약 다음 스테이지가 없다면
        if(nowChapter >= maxChapter)
        {
            //다음 스테이지로 넘어갈 수 없도록 버튼을 막는다
            Btn_RightSide.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
            Btn_RightSide.GetComponentInChildren<Button>().interactable = false;
        }
        //이전 스테이지로 갈 수 있는 버튼을 활성화 한다.
        Btn_LeftSide.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        Btn_LeftSide.GetComponentInChildren<Button>().interactable = true;

    }

    public void OnClickLeftButton()
    {
        nowChapter--;

        //버튼을 누르면 스테이지 1으로 돌아간다.
        Btn_Stage2.SetActive(false);
        Btn_Stage1.SetActive(true);
        if(nowChapter <= 1)
        {
            //이전 스테이지로 넘어갈 수 없도록 버튼을 막는다.
            Btn_LeftSide.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
            Btn_LeftSide.GetComponentInChildren<Button>().interactable = false;
        }
        //스테이지 2로 갈 수 있도록 버튼을 활성화 한다.
        Btn_RightSide.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        Btn_RightSide.GetComponentInChildren<Button>().interactable = true;
    }
}
