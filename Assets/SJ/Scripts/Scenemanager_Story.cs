using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager_Story : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject NextStagePanel;
    public GameObject nextBtn;

    private void Start()
    {
        OptionPanel.SetActive(false);
        NextStagePanel.SetActive(false);
    }

    public void OnNextScene()
    {
        //다음 씬으로 넘어가는 버튼을 활성화
        nextBtn.SetActive(false);
        NextStagePanel.SetActive(true);

    }

    public void OnClickNextScene()
    {
        SceneManager.LoadScene("9SJ_ARScene");
    }
    public void OnClickOption()
    {
        OptionPanel.SetActive(true);
    }
    public void OnClickX()
    {
        OptionPanel.SetActive(false);
    }
    public void OnClickToWorld()
    {
        SceneManager.LoadScene("2SJ_WorldMap");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
