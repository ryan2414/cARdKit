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

    //스토리모드
    public void OnClickStory()
    {
        SceneManager.LoadScene(1);
    }
    //자유모드
    public void OnClickAR()
    {

    }
    //설정
    public void OnClickOption()
    {
        OptionPanel.SetActive(true);
    }
    //도움말
    public void OnClickHelp()
    {
        HelpPanel.SetActive(true);
    }

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
    public IEnumerator IEHideBoard(CanvasGroup canvasGroup)
    {
        while (0 < canvasGroup.alpha)
        {
            canvasGroup.alpha -= Time.deltaTime / fillTime;
            yield return new WaitForSeconds(Time.deltaTime / fillTime);
        }
        canvasGroup.alpha = 0;

    }
}
