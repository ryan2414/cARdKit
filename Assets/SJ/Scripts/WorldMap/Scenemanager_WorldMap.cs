using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class Scenemanager_WorldMap : MonoBehaviour
{
    public GameObject UI_Finish;
    AudioSource soundClick;
    AudioSource bgm_Start;
    private void Start()
    {
        soundClick = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        bgm_Start = GameObject.Find("BGM_Start").GetComponent<AudioSource>();
        //노래가 플레이 중이 아니라면 BGM 틀기
        if (!bgm_Start.isPlaying)
        {
            bgm_Start.Play();
        }

    }
    //public void OnClickNextStage() => SceneManager.LoadScene("3SJ_Story1-1");

    public void OnClickBack()
    {
        soundClick.Play();
        SceneManager.LoadScene("1SJ_StartScene");
    }

    public void OnClickX()
    {
        soundClick.Play();
        UI_Finish.SetActive(false);
    }

    //특정 스테이지 버튼을 누르면 그 버튼에 스테이지 인덱스를 다음씬에 넘겨준다
    public void OnClickBox()
    {
        string nowButton = EventSystem.current.currentSelectedGameObject.name;

        soundClick.Play();
        //배경음악 끄기
        bgm_Start.Stop();

        if (nowButton == "Btn_Stage1-1") FlagManager.instance.stageNum = 1;
        else if (nowButton == "Btn_Stage1-2") FlagManager.instance.stageNum = 2;
        else if (nowButton == "Btn_Stage1-3") FlagManager.instance.stageNum = 3;
        else if (nowButton == "Btn_Stage1-4") FlagManager.instance.stageNum = 4;
        else if (nowButton == "Btn_Stage2-1") FlagManager.instance.stageNum = 5;
        else if (nowButton == "Btn_Stage2-2") FlagManager.instance.stageNum = 6;
        else if (nowButton == "Btn_Stage2-3") FlagManager.instance.stageNum = 7;

        if (FlagManager.instance.stageNum != 0) FlagManager.instance.Call();
    }
}
