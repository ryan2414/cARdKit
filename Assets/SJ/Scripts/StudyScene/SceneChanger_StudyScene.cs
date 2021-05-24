using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger_StudyScene : MonoBehaviour
{
    public void OnClickWorldBtn()
    {
        //버튼을 누르면 스테이지 4 & 7 클리어 후 월드맵으로 이동
        int _stageNum = FlagManager.instance.stageNum;
        if (_stageNum < 7)
        {
            FlagManager.instance.clearBool[_stageNum - 1] = true;

        }

        int bestStage = PlayerPrefs.GetInt("ClearLevel");

        //만약 스코어가 이전 스코어 보다 높으면 점수를 저장
        if (_stageNum > bestStage)
        {
            bestStage = _stageNum;
            PlayerPrefs.SetInt("ClearLevel", bestStage);
        }

        SceneManager.LoadScene("2SJ_WorldMap");
    }
}
