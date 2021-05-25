using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StudySceneManager : MonoBehaviour
{
    int index = 0;

    public GameObject[] Pages;

    public Button rightArrow;
    public Button leftArrow;

    public Image image_R;
    public Image image_L;

    public GameObject UI_optionPanel;
    AudioSource clickSound;
    AudioSource sound_change;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        clickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        sound_change = GameObject.Find("ChChangeSound").GetComponent<AudioSource>();

        UI_optionPanel.SetActive(false);

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
        image_L.color = new Color(0.5660378f, 0.5660378f, 0.5660378f, 1);
        image_R.color = new Color(0.5660378f, 0.5660378f, 0.5660378f, 1);

        if (index <= 0)
        {
            leftArrow.interactable = false;
            image_L.color = new Color(0.5660378f, 0.5660378f, 0.5660378f, 0.5f);
        }
        if (index >= Pages.Length - 1)
        {
            rightArrow.interactable = false;
            image_R.color = new Color(0.5660378f, 0.5660378f, 0.5660378f, 0.5f);
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
        sound_change.Play();
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
        sound_change.Play();
        PageChange();
        print(index + "Page");
    }

    public void OnClickOption()
    {
        clickSound.Play();
        UI_optionPanel.SetActive(true);
    }

    public void OnClickX()
    {
        clickSound.Play();
        //에셋에 함수가 포함되어 있음
        //UI_optionPanel.SetActive(false);
    }

    public void OnClickToWorld()
    {
        clickSound.Play();
        SceneManager.LoadScene("2SJ_WorldMap");
    }

    public void OnClickQuit()
    {
        clickSound.Play();
        Application.Quit();
    }
}
