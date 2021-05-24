using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger_StudyScene : MonoBehaviour
{
    public void OnClickWorldBtn()
    {
        //��ư�� ������ �������� 4 & 7 Ŭ���� �� ��������� �̵�
        int _stageNum = FlagManager.instance.stageNum;
        if (_stageNum < 7)
        {
            FlagManager.instance.clearBool[_stageNum - 1] = true;

        }

        int bestStage = PlayerPrefs.GetInt("ClearLevel");

        //���� ���ھ ���� ���ھ� ���� ������ ������ ����
        if (_stageNum > bestStage)
        {
            bestStage = _stageNum;
            PlayerPrefs.SetInt("ClearLevel", bestStage);
        }

        SceneManager.LoadScene("2SJ_WorldMap");
    }
}
