using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//�ϴ� ���� â ����
//�÷��̸� �����ϸ� ȭ�鿡 ��ȭâ�� ������
//�ؽ�Ʈ�� �ѱ��ھ� ������� ������ �ȴ�.
//
public class ARSceneManager : MonoBehaviour
{
    public GameObject UI_optionPanel;
    //public GameObject UI_helpPanel;
    public GameObject UI_clearPanel;
    public GameObject UI_FadeIn;
   
    //���� ����
    //public Button Btn_upHelp;
    //���� �ݱ�
    //public Button Btn_downHelp;

    public Button Btn_clear;

    //helpPanel�� ��ġ����
    Vector3 startPosition = new Vector3(0, -250, 0);

    public float speed;
    float deltaSpeed;

    private void Start()
    {
        //Btn_downHelp.gameObject.SetActive(false);
        //Btn_upHelp.gameObject.SetActive(true);
        UI_FadeIn.SetActive(true);
        Btn_clear.gameObject.SetActive(false);
        UI_optionPanel.SetActive(false);
        UI_clearPanel.SetActive(false);
    }

    private void Update()
    {
        deltaSpeed = speed * Time.deltaTime;

    }


    #region �ϴ� UI
    //public void OnClickUP()
    //{
    //    StartCoroutine("MoveUp");
    //}

    //IEnumerator MoveUp()
    //{
    //    //HelpPanel�� �Ʒ����� ���� ��Ÿ���� �ϰ� �ʹ�.
    //    Btn_upHelp.gameObject.SetActive(false);
    //    Btn_downHelp.gameObject.SetActive(true);

    //    UI_helpPanel.transform.localPosition = startPosition;

    //    Vector3 vector3Y = UI_helpPanel.transform.localPosition;

    //    while (vector3Y.y < 5)
    //    {
    //        vector3Y.y += deltaSpeed;
    //        UI_helpPanel.transform.localPosition = vector3Y;
    //        yield return null;
    //    }

    //}

    //public void OnClickDOWN()
    //{
    //    StartCoroutine("MoveDown");
    //}
    //IEnumerator MoveDown()
    //{
    //    Vector3 vector3Y = UI_helpPanel.transform.localPosition;
    //    while (vector3Y.y > -250)
    //    {
    //        vector3Y.y -= deltaSpeed;
    //        UI_helpPanel.transform.localPosition = vector3Y;
    //        yield return null;
    //    }

    //    Btn_upHelp.gameObject.SetActive(true);
    //    Btn_downHelp.gameObject.SetActive(false);
    //}
    #endregion

    public void OnClickOption()
    {
        UI_optionPanel.SetActive(true);
    }
    public void OnClickX()
    {
        UI_optionPanel.SetActive(false);
    }
    public void OnClickToWorld()
    {
        SceneManager.LoadScene("2SJ_WorldMap");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickNextStage()
    {
        int stageNum = FlagManager.instance.stageNum ;
        FlagManager.instance.stageNum = stageNum + 1;
        //���� ���������� 3�̸� 1é�� �����ϱ��
        if (stageNum == 3)
        {
            SceneManager.LoadScene("4SJ_StudyScene");
        }
        //���� ���������� 6�̸� 2é�� �����ϱ��
        else if (stageNum == 6)
        {
            SceneManager.LoadScene("5SJ_StudyScene2");
        }
        else
        {
            SceneManager.LoadScene("3SJ_Story1-1");

        }
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("9SJ_ARScene");
    }

    public void OnClickClear()
    {
        ClearUIActive_SJ.instance.UI_Clear.SetActive(false);
        UI_clearPanel.SetActive(true);
    }

    public void OnClickToGame()
    {
        UI_clearPanel.SetActive(false);
    }
}