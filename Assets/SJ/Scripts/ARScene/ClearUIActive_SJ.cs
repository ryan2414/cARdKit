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
    public GameObject go_Sh_MoveTarget;
    public GameObject go_Ml_MoveTarget;
    public GameObject txt_Sh;
    public GameObject txt_Ml;
    public GameObject btn_Clear;

    [Header("캐릭터 이동 속도")]
    public float moveSpeed;
    [Header("캐릭터 대사창 속도")]
    public float dialogSpeed;

    bool isPlay;
    bool isMoveFinish;
    bool isShSay;
    bool isTalkFinish;

    float timer;
    float deltaDis;

    Vector3 sh_OriginPos;
    Vector3 ml_OriginPos;
    // Start is called before the first frame update
    void Start()
    {
        UI_Clear.SetActive(false);
        txt_Sh.SetActive(false);
        txt_Ml.SetActive(false);
        btn_Clear.SetActive(false);
        txt_Sh.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        txt_Ml.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        sh_OriginPos = img_SH.transform.position;
        ml_OriginPos = img_ML.transform.position;

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
            deltaDis = moveSpeed * Time.deltaTime;
            Vector3 moveDistance = new Vector3(deltaDis, 0, 0);
            StartTalk();

            if (isShSay && isTalkFinish && timer >= 1f)
            {
                StopTalk();
            }
        }
    }

    private void StopTalk()
    {
        txt_Sh.GetComponent<Image>().color -= new Color(0, 0, 0, Time.deltaTime / dialogSpeed);
        txt_Ml.GetComponent<Image>().color -= new Color(0, 0, 0, Time.deltaTime / dialogSpeed);

        if (txt_Ml.GetComponent<Image>().color.a <= 0)
        {
            img_SH.transform.position = Vector3.MoveTowards(img_SH.transform.position, sh_OriginPos, deltaDis);
            img_ML.transform.position = Vector3.MoveTowards(img_ML.transform.position, ml_OriginPos, deltaDis);

            if (Vector3.Distance(img_SH.transform.position, sh_OriginPos) <= 0)
            {
                //스테이지가 클리어가 되면 그 스테이지가 클리어 됬다는 정보를 보내주고 싶다.
                int _stageNum = FlagManager.instance.stageNum - 1;
                FlagManager.instance.clearBool[_stageNum] = true;
                //클리어 버튼 활성화
                btn_Clear.SetActive(true);

                UI_Clear.SetActive(false);
                //한번만 재생을 위한 
                isPlay = true;
            }
        }
    }

    private void StartTalk()
    {
        if (isPlay == false && timer >= 4f)
        {
            UI_Clear.SetActive(true);

            if (!isMoveFinish)
            {
                //승형이가 왼쪽에서 나타난다
                img_SH.transform.position = Vector3.MoveTowards(img_SH.transform.position, go_Sh_MoveTarget.transform.position, deltaDis);

                //멀린이 오른쪽에서 나타난다
                img_ML.transform.position = Vector3.MoveTowards(img_ML.transform.position, go_Ml_MoveTarget.transform.position, deltaDis);
            }

            //목표 위치로 이동하면 대사를 시작한다.
            if (Vector3.Distance(img_SH.transform.position, go_Sh_MoveTarget.transform.position) <= 0)
            {
                isShSay = true;
                isMoveFinish = true;
            }

            //움직이는게 끝나면 승형이가 말한다
            if (isShSay && !isTalkFinish)
            {
                if (txt_Sh.GetComponent<Image>().color.a < 1)
                {
                    txt_Sh.SetActive(true);
                    txt_Sh.GetComponent<Image>().color += new Color(0, 0, 0, Time.deltaTime / dialogSpeed);
                }

                //멀린이 말한다.
                if (txt_Sh.GetComponent<Image>().color.a >= 1 && timer >= 5f)
                {
                    txt_Ml.SetActive(true);
                    txt_Ml.GetComponent<Image>().color += new Color(0, 0, 0, Time.deltaTime / dialogSpeed);

                    if (txt_Ml.GetComponent<Image>().color.a >= 1)
                    {
                        isTalkFinish = true;
                        timer = 0;
                    }
                }
            }
        }
    }
}
