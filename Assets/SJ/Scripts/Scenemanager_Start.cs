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
    public float fillTime;
    public CanvasGroup IMG_Fader;
    private void Awake()
    {
        StartCoroutine(IEHideBoard(IMG_Fader));
    }
    private void Start()
    {
        FreeMode.interactable = false;
        FreeMode.GetComponentInChildren<Text>().color = new Color(0,0,0,0.5f);
    }

    //���丮���
    public void OnClickStory()
    {
        SceneManager.LoadScene("2SJ_WorldMap");
    }

    //����
    public void OnClickOption()
    {
        OptionPanel.SetActive(true);
    }

    //���� ����
    public void OnClickQuit()
    {
        Application.Quit();
    }

    //����
    public void OnClickHelp()
    {
        HelpPanel.SetActive(true);
    }

    //���� â ����
    public void OnClickX()
    {
        if(OptionPanel.activeSelf == true)
        {
            OptionPanel.SetActive(false);
        }
        else
        {
            HelpPanel.SetActive(false);
        }
    }

    //���̵���
    public IEnumerator IEHideBoard(CanvasGroup canvasGroup)
    {
        while (0 < canvasGroup.alpha)
        {
            canvasGroup.alpha -= Time.deltaTime / fillTime;
            yield return new WaitForSeconds(Time.deltaTime / fillTime);
        }
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false);

    }
}
