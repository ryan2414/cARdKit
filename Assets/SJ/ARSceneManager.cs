using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARSceneManager : MonoBehaviour
{
    //도움말 열기
    public Button upHelp;
    //도움말 닫기
    public Button downHelp;
    public GameObject helpPanel;
    //helpPanel의 위치정보
    Vector3 startPosition = new Vector3(0, -250, 0);

    public float speed;
    float deltaSpeed;

    private void Start()
    {
        downHelp.gameObject.SetActive(false);
        upHelp.gameObject.SetActive(true);
    }
    private void Update()
    {
        deltaSpeed = speed * Time.deltaTime;

    }



    public void OnClickUP()
    {
        StartCoroutine("MoveUp");
    }

    IEnumerator MoveUp()
    {
        //HelpPanel이 아래에서 위로 나타나게 하고 싶다.
        upHelp.gameObject.SetActive(false);
        downHelp.gameObject.SetActive(true);

        helpPanel.transform.localPosition = startPosition;

        Vector3 vector3Y = helpPanel.transform.localPosition;

        while (vector3Y.y < 5)
        {
            vector3Y.y += deltaSpeed;
            helpPanel.transform.localPosition = vector3Y;
            yield return null;
        }

    }

    public void OnClickDOWN()
    {
        StartCoroutine("MoveDown");
    }
    IEnumerator MoveDown()
    {
        Vector3 vector3Y = helpPanel.transform.localPosition;
        while (vector3Y.y > -250)
        {
            vector3Y.y -= deltaSpeed;
            helpPanel.transform.localPosition = vector3Y;
            yield return null;
        }

        upHelp.gameObject.SetActive(true);
        downHelp.gameObject.SetActive(false);
    }
}