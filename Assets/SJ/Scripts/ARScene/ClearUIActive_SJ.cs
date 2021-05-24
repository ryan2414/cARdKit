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
    public GameObject go_Sh_MoveTarget;
    public GameObject go_Ml_MoveTarget;
    public GameObject txt_Sh;
    public GameObject txt_Ml;
    public GameObject btn_Clear;

    [Header("ĳ���� �̵� �ӵ�")]
    public float moveSpeed;
    [Header("ĳ���� ���â �ӵ�")]
    public float dialogSpeed;

    public GameObject clearAnim;

    public AudioSource sound_ML;
    public AudioSource sound_SH;

    //Ŭ���� �ִϸ��̼� ���� �ð� ����
    public float animStartTime;

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
        clearAnim.SetActive(false);
        UI_Clear.SetActive(false);
        txt_Sh.SetActive(false);
        txt_Ml.SetActive(false);
        btn_Clear.SetActive(false);
        txt_Sh.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        txt_Sh.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0);
        txt_Ml.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        txt_Ml.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0);

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
            if(timer >= animStartTime)
            {
                //Ŭ���� �ִϸ��̼�
                clearAnim.SetActive(true);
                
                //��ȭ �ִϸ��̼� ����
                StartTalk();
            }
           
            

            if (isShSay && isTalkFinish && timer >= 1f)
            {
                StopTalk();
            }
        }
    }
    bool isSHspeak;
    bool isMLspeak;
    private void StartTalk()
    {
        if (isPlay == false && timer >= animStartTime + 4f)
        {
            UI_Clear.SetActive(true);

            if (!isMoveFinish)
            {
                //�����̰� ���ʿ��� ��Ÿ����
                img_SH.transform.position = Vector3.MoveTowards(img_SH.transform.position, go_Sh_MoveTarget.transform.position, deltaDis);

                //�ָ��� �����ʿ��� ��Ÿ����
                img_ML.transform.position = Vector3.MoveTowards(img_ML.transform.position, go_Ml_MoveTarget.transform.position, deltaDis);

            }

            //��ǥ ��ġ�� �̵��ϸ� ��縦 �����Ѵ�.
            if (Vector3.Distance(img_ML.transform.position, go_Ml_MoveTarget.transform.position) <= 0)
            {
                isShSay = true;
                isMoveFinish = true;
            }

            //�����̴°� ������ �����̰� ���Ѵ�
            if (isShSay && !isTalkFinish)
            {
                if (txt_Sh.GetComponent<Image>().color.a < 1)
                {
                    txt_Sh.SetActive(true);
                    txt_Sh.GetComponent<Image>().color += new Color(0, 0, 0, Time.deltaTime / dialogSpeed);
                    txt_Sh.GetComponentInChildren<Text>().color += new Color(0, 0, 0, Time.deltaTime / dialogSpeed);
                    //������ ȿ����
                    if (!isSHspeak)
                    {
                        sound_SH.Play();
                        isSHspeak = true;
                    }

                }
               
                //�ָ��� ���Ѵ�.
                if (txt_Sh.GetComponent<Image>().color.a >= 1 && timer >= animStartTime + 5f)
                {
                    txt_Ml.SetActive(true);
                    txt_Ml.GetComponent<Image>().color += new Color(0, 0, 0, Time.deltaTime / dialogSpeed);
                    txt_Ml.GetComponentInChildren<Text>().color += new Color(0, 0, 0, Time.deltaTime / dialogSpeed);

                    //�ָ� ȿ����
                    if (!isMLspeak)
                    {
                        sound_ML.Play();
                        isMLspeak = true;
                    }

                    if (txt_Ml.GetComponent<Image>().color.a >= 1)
                    {
                        isTalkFinish = true;
                        timer = 0;
                    }
                }
            }
        }
    }

    private void StopTalk()
    {
        txt_Sh.GetComponent<Image>().color -= new Color(0, 0, 0, Time.deltaTime / dialogSpeed);
        txt_Sh.GetComponentInChildren<Text>().color -= new Color(0, 0, 0, Time.deltaTime / dialogSpeed);
        txt_Ml.GetComponent<Image>().color -= new Color(0, 0, 0, Time.deltaTime / dialogSpeed);
        txt_Ml.GetComponentInChildren<Text>().color -= new Color(0, 0, 0, Time.deltaTime / dialogSpeed);

        if (txt_Ml.GetComponent<Image>().color.a <= 0)
        {
            img_SH.transform.position = Vector3.MoveTowards(img_SH.transform.position, sh_OriginPos, deltaDis);
            img_ML.transform.position = Vector3.MoveTowards(img_ML.transform.position, ml_OriginPos, deltaDis);

            if (Vector3.Distance(img_SH.transform.position, sh_OriginPos) <= 0)
            {
                ClearStage();

            }
        }
    }
    bool isOnce;
    private void ClearStage()
    {
        if (!isOnce)
        {
            //���������� Ŭ��� �Ǹ� �� ���������� Ŭ���� ��ٴ� ������ �����ְ� �ʹ�.
            int _stageNum = FlagManager.instance.stageNum;
            FlagManager.instance.clearBool[_stageNum - 1] = true;

            int bestStage = PlayerPrefs.GetInt("ClearLevel");
            print(_stageNum);

            //���� ���ھ ���� ���ھ� ���� ������ ������ ����
            if (_stageNum > bestStage)
            {
                bestStage = _stageNum;
                PlayerPrefs.SetInt("ClearLevel", bestStage);
            }
            isOnce = true;
        }


        //Ŭ���� ��ư Ȱ��ȭ
        btn_Clear.SetActive(true);

        UI_Clear.SetActive(false);
        //�ѹ��� ����� ���� 
        isPlay = true;
    }
}
