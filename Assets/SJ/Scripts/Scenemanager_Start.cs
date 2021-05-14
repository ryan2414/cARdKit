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

    bool isMenuOn;

    private void Awake()
    {
        IMG_Fader.gameObject.SetActive(true);
        StartCoroutine(IEHideBoard(IMG_Fader));
    }
    private void Start()
    {
        FreeMode.interactable = false;
        FreeMode.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0.5f);

    }

    //���丮���
    public void OnClickStory()
    {
        SceneManager.LoadScene("2SJ_WorldMap");
    }

    //����
    public void OnClickOption()
    {
        if (!isMenuOn)
        {
            isMenuOn = true;
            OptionPanel.SetActive(true);
            OptionPanel.GetComponent<ScaleUp>().enabled = true;
        }
    }

    //���� ����
    public void OnClickQuit()
    {
        Application.Quit();
    }

    //����
    public void OnClickHelp()
    {
        if (!isMenuOn)
        {
            isMenuOn = true;
            HelpPanel.SetActive(true);
            HelpPanel.GetComponent<ScaleUp>().enabled = true;
        }
    }

    //���� â ����
    public void OnClickX()
    {
        if (OptionPanel.activeSelf == true)
        {
            isMenuOn = false;
            OptionPanel.GetComponent<ScaleDown>().enabled = true;
        }
        else
        {
            isMenuOn = false;
            HelpPanel.GetComponent<ScaleDown>().enabled = true;
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

        print("������ ����!!");
    }

    //���̵���
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
