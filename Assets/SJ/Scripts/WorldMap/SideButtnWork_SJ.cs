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
        //���� ���������� �Ѿ �� ������ ��ư�� ���´�.
        Btn_LeftSide.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
        Btn_LeftSide.GetComponentInChildren<Button>().interactable = false;
    }
    public void OnClickRightButton()
    {
        nowChapter++;

        //��ư�� ������ �������� 2�� Ȱ��ȭ �Ѵ�
        Btn_Stage1.SetActive(false);
        Btn_Stage2.SetActive(true);
        //���� ���� ���������� ���ٸ�
        if(nowChapter >= maxChapter)
        {
            //���� ���������� �Ѿ �� ������ ��ư�� ���´�
            Btn_RightSide.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
            Btn_RightSide.GetComponentInChildren<Button>().interactable = false;
        }
        //���� ���������� �� �� �ִ� ��ư�� Ȱ��ȭ �Ѵ�.
        Btn_LeftSide.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        Btn_LeftSide.GetComponentInChildren<Button>().interactable = true;

    }

    public void OnClickLeftButton()
    {
        nowChapter--;

        //��ư�� ������ �������� 1���� ���ư���.
        Btn_Stage2.SetActive(false);
        Btn_Stage1.SetActive(true);
        if(nowChapter <= 1)
        {
            //���� ���������� �Ѿ �� ������ ��ư�� ���´�.
            Btn_LeftSide.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
            Btn_LeftSide.GetComponentInChildren<Button>().interactable = false;
        }
        //�������� 2�� �� �� �ֵ��� ��ư�� Ȱ��ȭ �Ѵ�.
        Btn_RightSide.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        Btn_RightSide.GetComponentInChildren<Button>().interactable = true;
    }
}
