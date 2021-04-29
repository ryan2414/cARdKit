using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class Scenemanager_WorldMap : MonoBehaviour
{
    public GameObject UI_Finish;
    //public void OnClickNextStage() => SceneManager.LoadScene("3SJ_Story1-1");

    public void OnClickBack() => SceneManager.LoadScene("1SJ_StartScene");

    public void OnClickX() => UI_Finish.SetActive(false);


    //특정 스테이지 버튼을 누르면 그 버튼에 스테이지 인덱스를 다음씬에 넘겨준다
    public void OnClickBox()
    {
        string nowButton = EventSystem.current.currentSelectedGameObject.name;

        if (nowButton == "Btn_Stage1-1") FlagManager.instance.stageNum = 1;
        else if (nowButton == "Btn_Stage1-2") FlagManager.instance.stageNum = 2;
        else if (nowButton == "Btn_Stage1-3") FlagManager.instance.stageNum = 3;
        else if (nowButton == "Btn_Stage1-4") FlagManager.instance.stageNum = 4;
        else if (nowButton == "Btn_Stage2-1") FlagManager.instance.stageNum = 5;
        else if (nowButton == "Btn_Stage2-2") FlagManager.instance.stageNum = 6;
        else if (nowButton == "Btn_Stage2-3") FlagManager.instance.stageNum = 7;
        else if (nowButton == "Btn_Stage2-4") FlagManager.instance.stageNum = 8;

        if (FlagManager.instance.stageNum != 0) FlagManager.instance.Call();
    }
}
