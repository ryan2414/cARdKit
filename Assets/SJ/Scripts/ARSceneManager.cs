using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//하단 도움말 창 삭제
//플레이를 시작하면 화면에 대화창이 나오고
//텍스트가 한글자씩 순서대로 실행이 된다.
//
public class ARSceneManager : MonoBehaviour
{
    public GameObject UI_optionPanel;
    //public GameObject UI_helpPanel;
    public GameObject UI_clearPanel;
   
    //도움말 열기
    //public Button Btn_upHelp;
    //도움말 닫기
    //public Button Btn_downHelp;

    public Button Btn_clear;

    //helpPanel의 위치정보
    Vector3 startPosition = new Vector3(0, -250, 0);

    public float speed;
    float deltaSpeed;

    private void Start()
    {
        //Btn_downHelp.gameObject.SetActive(false);
        //Btn_upHelp.gameObject.SetActive(true);
        Btn_clear.gameObject.SetActive(false);
        UI_optionPanel.SetActive(false);
        UI_clearPanel.SetActive(false);
    }

    private void Update()
    {
        deltaSpeed = speed * Time.deltaTime;

    }


    #region 하단 UI
    //public void OnClickUP()
    //{
    //    StartCoroutine("MoveUp");
    //}

    //IEnumerator MoveUp()
    //{
    //    //HelpPanel이 아래에서 위로 나타나게 하고 싶다.
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
        //SceneManager.LoadScene("");
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("9SJ_ARScene");
    }

    public void OnClickClear()
    {
        UI_clearPanel.SetActive(true);
    }

    public void OnClickToGame()
    {
        UI_clearPanel.SetActive(false);
    }
}