using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager_Story : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject NextStagePanel;
    public GameObject nextBtn;
    public GameObject UI_FaderPanel;

    AudioSource soundClick;
    private void Start()
    {
        soundClick = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        OptionPanel.SetActive(false);
        NextStagePanel.SetActive(false);
        UI_FaderPanel.SetActive(false);
    }

    //다음 씬으로 넘어가는 버튼을 활성화
    public void OnNextScene()
    {
        nextBtn.SetActive(false);
        NextStagePanel.SetActive(true);
    }

    //다음 씬으로 넘어가기 위해 페이더를 활성화
    public void FaderStart() => UI_FaderPanel.SetActive(true);

    //다음 씬으로 넘어가는 액션
    public void OnClickNextScene()
    {
            SceneManager.LoadScene("9SJ_ARScene");
    }
    public void OnClickOption()
    {
        soundClick.Play();
        OptionPanel.SetActive(true);
    }
    public void OnClickX()
    {
        soundClick.Play();

        OptionPanel.SetActive(false);
    }
    public void OnClickToWorld()
    {
        soundClick.Play();

        SceneManager.LoadScene("2SJ_WorldMap");
    }

    public void OnClickQuit()
    {
        soundClick.Play();

        Application.Quit();
    }
}
