using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public Text textName;
    public Text textScript;

    List<string> names = new List<string>();
    List<string> scripts = new List<string>();

    
    int textCount = 0;              //순서대로 출력을 하기 위한 카운트

    char[] scriptsCharArr;          //scripts를 문자로 받기 위한 함수
    string printWord;               //scriptsCharArr를 쌓아가기 위해 만든 문자열

    bool isPrinting = false;        //글자를 프린트 중이다
    bool isSentenseSkip = false;    //문장을 스킵하기 위한 bool 값

    // Start is called before the first frame update
    void Start()
    {
        LogUpdate();
    }
    void LogUpdate()
    {
        //Table의 정보 name과 script를 List로 저장한다.
        for (int i = 0; i < Table.instance.textLength; i++)
        {
            names.Add(Table.instance.GetTableName(i));
            scripts.Add(Table.instance.GetTableScript(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void OnClickNext()
    {
        //배열 초과를 막기 위한 것
        if (textCount >= Table.instance.textLength)
        {
            return;
        }

        //만약 문자열이 출력 중일때  한번 더 누르면 
        if (isPrinting)
        {
            //모든 문장을 한번에 출력
            isSentenseSkip = true;

            return;
        }

        //scripts를 char로 변환
        scriptsCharArr = scripts[textCount].ToCharArray();

        StopCoroutine("IEPrintWord");
        StartCoroutine("IEPrintWord");
    }

    IEnumerator IEPrintWord()
    {
        isPrinting = true;

        
        int printWordCount = 0;             //글자가 다 출력이 되면 textCount를 올리기 위한 변수 

        for (int i = 0; i < scripts[textCount].Length && !isSentenseSkip; i++)
        {
            printWord += scriptsCharArr[i].ToString();
            textScript.text = printWord;
            textName.text = names[textCount];//대사 주체 이름 출력

            printWordCount++;
            yield return new WaitForSeconds(0.1f);
        }

        //만약 문장을 스킵하면 PrintWordCoutn를 올려 다음 문장으로 넘어 가게한다
        if (isSentenseSkip)
        {
            printWordCount = scripts[textCount].Length;
        }

        printWord = "";

        textScript.text = scripts[textCount];
        textName.text = names[textCount];

        //문자열이 다 출력이 되었다면
        //다음 문장으로 넘어간다
        if (printWordCount == scripts[textCount].Length)
        {
            textCount++;
        }
        isPrinting = false;
        isSentenseSkip = false;
    }

}
