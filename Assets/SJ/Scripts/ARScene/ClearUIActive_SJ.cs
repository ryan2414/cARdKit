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
    //외부에서 받아올 클리어 변수
    public bool isStageClear;

    [Header("클리어대화")]
    public GameObject UI_Clear;
    public GameObject img_SH;
    public GameObject img_ML;
    public GameObject txt_Sh;
    public GameObject txt_Ml;
    public GameObject btn_Clear;

    [Header("캐릭터 이동 속도")]
    public float moveSpeed;
    [Header("캐릭터 대사창 속도")]
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
                //승형이가 왼쪽에서 나타난다
                if (img_SH.transform.position.x < -47)
                {
                    img_SH.transform.position += moveDistance;
                }
                else if (img_SH.transform.position.x >= -47)
                    isShSay = true;

                //멀린이 오른쪽에서 나타난다
                if (img_ML.transform.position.x > 2129.0f)
                {
                    img_ML.transform.position -= moveDistance;
                }

                //움직이는게 끝나면 승형이가 말한다
                if (isShSay == true)
                {
                    txt_Sh.SetActive(true);
                    txt_Sh.GetComponent<Image>().color += new Color(0, 0, 0, dialogSpeed * Time.deltaTime);

                    //멀린이 말한다.
                    if (txt_Sh.GetComponent<Image>().color.a >= 1 && timer >= 5f)
                    {
                        txt_Ml.SetActive(true);
                        txt_Ml.GetComponent<Image>().color += new Color(0, 0, 0, dialogSpeed * Time.deltaTime);

                        if (txt_Ml.GetComponent<Image>().color.a >= 1 && timer >= 6f)
                        {
                            //클리어 버튼 활성화
                            btn_Clear.SetActive(true);
                            isPlay = true;
                        }

                    }
                }
            }


        }
    }
}
