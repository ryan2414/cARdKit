using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Scenemanager_Start : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject HelpPanel;
    public Button FreeMode;
    public float faderTime;
    public CanvasGroup IMG_Fader;
    public AudioSource soundClick;

    bool isMenuOn;

    private void Awake()
    {
        IMG_Fader.gameObject.SetActive(true);
        StartCoroutine(IEHideBoard(IMG_Fader));
    }
    private void Start()
    {
        soundClick = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        FreeMode.interactable = false;
        FreeMode.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0.5f);
        OptionPanel.SetActive(false);
        HelpPanel.SetActive(false);

    }

    //스토리모드
    public void OnClickStory()
    {
        soundClick.Play();
        SceneManager.LoadScene("2SJ_WorldMap");
    }

    //설정
    public void OnClickOption()
    {
        if (!isMenuOn)
        {
            soundClick.Play();
            isMenuOn = true;
            OptionPanel.SetActive(true);
            //OptionPanel.GetComponent<ScaleUp>().enabled = true;
        }
    }

    //게임 종료
    public void OnClickQuit()
    {
        soundClick.Play();
        Application.Quit();
    }

    //도움말
    public void OnClickHelp()
    {
        if (!isMenuOn)
        {
            soundClick.Play();
            isMenuOn = true;
            HelpPanel.SetActive(true);
            //HelpPanel.GetComponent<ScaleUp>().enabled = true;
        }
    }

    //설정 창 끄기
    public void OnClickX()
    {
        if (OptionPanel.activeSelf == true)
        {
            soundClick.Play();
            isMenuOn = false;
            //OptionPanel.GetComponent<ScaleDown>().enabled = true;
        }
        else
        {
            soundClick.Play();
            isMenuOn = false;
            //HelpPanel.GetComponent<ScaleDown>().enabled = true;
        }
    }

    public void OnClickDeleteSaveData()
    {
        PlayerPrefs.DeleteKey("ClearLevel");
        PlayerPrefs.DeleteKey("GoodStamp1-1");
        PlayerPrefs.DeleteKey("GoodStamp1-2");
        PlayerPrefs.DeleteKey("GoodStamp1-3");
        PlayerPrefs.DeleteKey("GoodStamp1-4");
        PlayerPrefs.DeleteKey("GoodStamp2-1");
        PlayerPrefs.DeleteKey("GoodStamp2-2");
        PlayerPrefs.DeleteKey("GoodStamp2-3");
        PlayerPrefs.DeleteKey("ChapterClear");
        PlayerPrefs.DeleteKey("playingChpater");


        soundClick.Play();
        print("데이터 삭제!!");
    }

    //페이드인
    public IEnumerator IEHideBoard(CanvasGroup canvasGroup)
    {
        while (0 < canvasGroup.alpha)
        {
            canvasGroup.alpha -= Time.deltaTime / faderTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false);

    }
}
