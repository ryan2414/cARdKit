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

    
    int textCount = 0;              //������� ����� �ϱ� ���� ī��Ʈ

    char[] scriptsCharArr;          //scripts�� ���ڷ� �ޱ� ���� �Լ�
    string printWord;               //scriptsCharArr�� �׾ư��� ���� ���� ���ڿ�

    bool isPrinting = false;        //���ڸ� ����Ʈ ���̴�
    bool isSentenseSkip = false;    //������ ��ŵ�ϱ� ���� bool ��

    // Start is called before the first frame update
    void Start()
    {
        LogUpdate();
    }
    void LogUpdate()
    {
        //Table�� ���� name�� script�� List�� �����Ѵ�.
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
        //�迭 �ʰ��� ���� ���� ��
        if (textCount >= Table.instance.textLength)
        {
            return;
        }

        //���� ���ڿ��� ��� ���϶�  �ѹ� �� ������ 
        if (isPrinting)
        {
            //��� ������ �ѹ��� ���
            isSentenseSkip = true;

            return;
        }

        //scripts�� char�� ��ȯ
        scriptsCharArr = scripts[textCount].ToCharArray();

        StopCoroutine("IEPrintWord");
        StartCoroutine("IEPrintWord");
    }

    IEnumerator IEPrintWord()
    {
        isPrinting = true;

        
        int printWordCount = 0;             //���ڰ� �� ����� �Ǹ� textCount�� �ø��� ���� ���� 

        for (int i = 0; i < scripts[textCount].Length && !isSentenseSkip; i++)
        {
            printWord += scriptsCharArr[i].ToString();
            textScript.text = printWord;
            textName.text = names[textCount];//��� ��ü �̸� ���

            printWordCount++;
            yield return new WaitForSeconds(0.1f);
        }

        //���� ������ ��ŵ�ϸ� PrintWordCoutn�� �÷� ���� �������� �Ѿ� �����Ѵ�
        if (isSentenseSkip)
        {
            printWordCount = scripts[textCount].Length;
        }

        printWord = "";

        textScript.text = scripts[textCount];
        textName.text = names[textCount];

        //���ڿ��� �� ����� �Ǿ��ٸ�
        //���� �������� �Ѿ��
        if (printWordCount == scripts[textCount].Length)
        {
            textCount++;
        }
        isPrinting = false;
        isSentenseSkip = false;
    }

}
