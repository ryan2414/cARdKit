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
}
