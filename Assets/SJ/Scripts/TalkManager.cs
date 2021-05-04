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
    int maxTextCount;

    char[] scriptsCharArr;          //scripts�� ���ڷ� �ޱ� ���� �Լ�
    string printWord;               //scriptsCharArr�� �׾ư��� ���� ���� ���ڿ�

    bool isPrinting = false;        //���ڸ� ����Ʈ ���̴�
    bool isSentenseSkip = false;    //������ ��ŵ�ϱ� ���� bool ��
    bool isCanTouch = false;

    // Start is called before the first frame update
    void Start()
    {
        //Table�� ���� name�� script�� List�� �����Ѵ�.
        for (int i = 0; i < Table.instance.textLength; i++)
        {
            names.Add(Table.instance.GetTableName(i));
            scripts.Add(Table.instance.GetTableScript(i));
        }
        maxTextCount = Table.instance.textLength;
        OnClickNext();
    }
    // Update is called once per frame
    void Update()
    {
        //���� ������ �Ѿ�� ����
        //�ؽ�Ʈ ī��Ʈ�� ��ȭ���� ���̰� �������� 
        if (textCount >= maxTextCount)
        {
            //���Ŵ����� OnNextScene �Լ��� �����Ѵ�.
            gameObject.GetComponent<Scenemanager_Story>().OnNextScene();
        }
    }

    //��ư�� ������ ���� ������ ����Ѵ�.
    public void OnClickNext()
    {
        //�迭 �ʰ��� ���� ���� ��
        if (textCount >= Table.instance.textLength)
        {
            return;
        }

        if (!isCanTouch)
        {
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

        printWord = ""; //printWord ���� �ʱ�ȭ

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

    //��ŵ��ư�� ������ ��ȭ ��ũ��Ʈ�� �� ������ �̵��Ѵ�.
    //AR ������ ȭ���� �ڵ����� ��ȯ�� �ȴ�.
    public void OnClickSkip()
    {
        StopCoroutine("IEPrintWord");
        isCanTouch = true;
        textName.text = names[maxTextCount-1];
        textScript.text = scripts[maxTextCount-1];
        textCount = maxTextCount;
    }
}
