using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUIActive_SJ : MonoBehaviour
{
    public static ClearUIActive_SJ instance;
    private void Awake()
    {
        instance = this;
    }
    //�ܺο��� �޾ƿ� Ŭ���� ����
    public bool isStageClear;

    [Header("Ŭ�����ȭ")]
    public GameObject UI_Clear;
    public GameObject img_SH;
    public GameObject img_ML;
    public GameObject txt_Sh;
    public GameObject txt_Ml;
    public GameObject btn_Clear;

    [Header("ĳ���� �̵� �ӵ�")]
    public float moveSpeed;
    [Header("ĳ���� ���â �ӵ�")]
    public float dialogSpeed;

    bool isPlay;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        UI_Clear.SetActive(false);
        txt_Sh.SetActive(false);
        txt_Ml.SetActive(false);
        btn_Clear.SetActive(false);
        txt_Sh.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        txt_Ml.GetComponent<Image>().color = new Color(1, 1, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        GameClear();
    }



    private void GameClear()
    {
        if (isStageClear == true)
        {
            timer += Time.deltaTime;
            float deltaDis = moveSpeed / Time.deltaTime;
            Vector3 moveDistance = new Vector3(deltaDis, 0, 0);

            if (isPlay == false && timer >= 4f)
            {
                UI_Clear.SetActive(true);

                bool isShSay = false;
                //�����̰� ���ʿ��� ��Ÿ����
                if (img_SH.transform.position.x < -47)
                {
                    img_SH.transform.position += moveDistance;
                }
                else if (img_SH.transform.position.x >= -47)
                    isShSay = true;

                //�ָ��� �����ʿ��� ��Ÿ����
                if (img_ML.transform.position.x > 2129.0f)
                {
                    img_ML.transform.position -= moveDistance;
                }

                //�����̴°� ������ �����̰� ���Ѵ�
                if (isShSay == true)
                {
                    txt_Sh.SetActive(true);
                    txt_Sh.GetComponent<Image>().color += new Color(0, 0, 0, dialogSpeed * Time.deltaTime);

                    //�ָ��� ���Ѵ�.
                    if (txt_Sh.GetComponent<Image>().color.a >= 1 && timer >= 5f)
                    {
                        txt_Ml.SetActive(true);
                        txt_Ml.GetComponent<Image>().color += new Color(0, 0, 0, dialogSpeed * Time.deltaTime);

                        if (txt_Ml.GetComponent<Image>().color.a >= 1 && timer >= 6f)
                        {
                            //Ŭ���� ��ư Ȱ��ȭ
                            btn_Clear.SetActive(true);
                            isPlay = true;
                        }

                    }
                }
            }


        }
    }
}
