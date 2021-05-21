using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public Text textName;
    public Text textScript;
    public Animator anim;
    public float invokeTimer;
    public GameObject fog;
    public GameObject ML_LumosAnim;

    List<string> names = new List<string>();
    List<string> emotions = new List<string>();
    List<string> scripts = new List<string>();


    int lineNum = 0;              //순서대로 출력을 하기 위한 카운트
    int maxTextCount;

    char[] scriptsCharArr;          //scripts를 문자로 받기 위한 함수
    string printWord;               //scriptsCharArr를 쌓아가기 위해 만든 문자열

    bool isPrinting = false;        //글자를 프린트 중이다
    bool isSentenseSkip = false;    //문장을 스킵하기 위한 bool 값
    bool isCanTouch = false;

    // Start is called before the first frame update
    void Start()
    {
        //Table의 정보 name, emotion, script를 List로 저장한다.
        for (int i = 0; i < Table.instance.textLength; i++)
        {
            names.Add(Table.instance.GetTableName(i));
            scripts.Add(Table.instance.GetTableScript(i));
            emotions.Add(Table.instance.GetTableEmotion(i));
        }
        maxTextCount = Table.instance.textLength;
        fog.SetActive(false);
        ML_LumosAnim.SetActive(false);
        OnClickNext();
    }
    // Update is called once per frame
    void Update()
    {
        //다음 씬으로 넘어가기 위해
        //텍스트 카운트랑 대화집의 길이가 같아지면 
        if (lineNum >= maxTextCount)
        {
            //씬매니져의 OnNextScene 함수를 실행한다.
            Invoke("NextScene", invokeTimer);
        }
    }
    void NextScene()
    {
        gameObject.GetComponent<Scenemanager_Story>().OnNextScene();

    }
    //버튼을 누르면 다음 문장을 출력한다.
    public void OnClickNext()
    {
        if (lineNum > 0 && !isPrinting)
            GameObject.Find("TxtClickSound").GetComponent<AudioSource>().Play();

        //배열 초과를 막기 위한 것
        if (lineNum >= Table.instance.textLength)
        {
            return;
        }

        if (!isCanTouch)
        {
            //만약 문자열이 출력 중일때  한번 더 누르면 
            if (isPrinting)
            {
                //모든 문장을 한번에 출력
                isSentenseSkip = true;

                return;
            }

            //scripts를 char로 변환
            scriptsCharArr = scripts[lineNum].ToCharArray();

            StopCoroutine("IEPrintWord");
            StartCoroutine("IEPrintWord");
        }
    }

    IEnumerator IEPrintWord()
    {
        isPrinting = true;

        int printWordCount = 0;             //글자가 다 출력이 되면 textCount를 올리기 위한 변수 

        textName.text = names[lineNum];//대사 주체 이름 출력

        #region 스토리에 따른 캐릭터 변경
        ActEmotion(emotions[lineNum]);
        #endregion

        for (int i = 0; i < scripts[lineNum].Length && !isSentenseSkip; i++)
        {
            printWord += scriptsCharArr[i].ToString();
            textScript.text = printWord;

            printWordCount++;
            yield return new WaitForSeconds(0.05f);
        }

        //만약 문장을 스킵하면 PrintWordCoutn를 올려 다음 문장으로 넘어 가게한다
        if (isSentenseSkip)
        {
            printWordCount = scripts[lineNum].Length;
        }

        printWord = ""; //printWord 변수 초기화

        textScript.text = scripts[lineNum];
        textName.text = names[lineNum];

        //문자열이 다 출력이 되었다면
        //다음 문장으로 넘어간다
        if (printWordCount == scripts[lineNum].Length)
        {
            lineNum++;
        }

        isPrinting = false;
        isSentenseSkip = false;

    }

    //스킵버튼을 누르면 대화 스크립트가 맨 끝으로 이동한다.
    //AR 씬으로 화면이 자동으로 전환이 된다.
    public void OnClickSkip()
    {
        StopCoroutine("IEPrintWord");
        isCanTouch = true;
        textName.text = names[maxTextCount - 1];
        textScript.text = scripts[maxTextCount - 1];
        ActEmotion(emotions[maxTextCount - 1]);
        lineNum = maxTextCount;
    }

    #region 스토리에 따른 캐릭터 변경
    public List<Sprite> emoImgList = new List<Sprite>();
    public GameObject playerImg;

    void ActEmotion(string emoTxt)
    {
        if (emoTxt == "emty")
            playerImg.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        else
        {
            playerImg.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }



        switch (emoTxt)
        {
            case "emty":
                playerImg.GetComponent<Image>().sprite = null;
                break;
            case "dam_SH":
                fog.SetActive(true);
                playerImg.GetComponent<Image>().sprite = emoImgList[0];
                break;
            case "idle_SH":
                fog.SetActive(true);
                playerImg.GetComponent<Image>().sprite = emoImgList[1];
                break;
            case "sus_SH":
                fog.SetActive(true);
                playerImg.GetComponent<Image>().sprite = emoImgList[2];
                break;
            case "wow_SH":
                fog.SetActive(true);
                playerImg.GetComponent<Image>().sprite = emoImgList[3];
                break;
            case "idle_ML":
                fog.SetActive(true);
                playerImg.GetComponent<Image>().sprite = emoImgList[4];

                break;
            case "ohh_ML":
                fog.SetActive(true);
                playerImg.GetComponent<Image>().sprite = emoImgList[5];
                break;
            case "wind":
                fog.SetActive(true);
                anim.SetTrigger("isWind");
                break;
            case "strongWind":
                fog.SetActive(true);
                anim.SetTrigger("isStrongWind");
                break;
            case "lumos":
                ML_LumosAnim.SetActive(true);
                GameObject.Find("MagicSound").GetComponent<AudioSource>().Play();
                playerImg.GetComponent<Image>().sprite = emoImgList[4];
                break;
            default:
                fog.SetActive(true);
                playerImg.GetComponent<Image>().sprite = emoImgList[4];
                break;

        }
    }
    #endregion
}
