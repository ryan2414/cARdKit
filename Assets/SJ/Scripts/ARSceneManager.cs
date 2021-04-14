using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARSceneManager : MonoBehaviour
{
    //���� ����
    public Button upHelp;
    //���� �ݱ�
    public Button downHelp;
    public GameObject helpPanel;
    //helpPanel�� ��ġ����
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
        //HelpPanel�� �Ʒ����� ���� ��Ÿ���� �ϰ� �ʹ�.
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