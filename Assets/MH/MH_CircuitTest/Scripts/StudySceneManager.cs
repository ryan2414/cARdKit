using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudySceneManager : MonoBehaviour
{
    int index = 0;

    public GameObject[] Pages;

    public Button rightArrow;
    public Button leftArrow;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        PageChange();
    }

    void PageChange()
    {
        switch (index)
        {
            case 0:
                ResetPage();
                Pages[0].SetActive(true);
                break;
            case 1:
                ResetPage();
                Pages[1].SetActive(true);
                break;
            default:
                print("페이지 수를 벗어났습니다.");
                break;
        }

        leftArrow.interactable = true;
        rightArrow.interactable = true;

        if (index <= 0)
        {
            leftArrow.interactable = false;
        }
        if(index >= Pages.Length - 1)
        {
            rightArrow.interactable = false;
        }
    }

    void ResetPage()
    {
        foreach (GameObject page in Pages)
        {
            page.SetActive(false);
        }
    }

    public void OnClickLeft()
    {
        index--;
        if (index <= 0)
        {
            index = 0;
        }
        PageChange();
        print(index + "Page");
    }

    public void OnClickRight()
    {
        index++;
        if (index >= Pages.Length - 1)
        {
            index = Pages.Length - 1;
        }
        PageChange();
        print(index + "Page");
    }

}
