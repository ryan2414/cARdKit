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


    int lineNum = 0;              //������� ����� �ϱ� ���� ī��Ʈ
    int maxTextCount;

    char[] scriptsCharArr;          //scripts�� ���ڷ� �ޱ� ���� �Լ�
    string printWord;               //scriptsCharArr�� �׾ư��� ���� ���� ���ڿ�

    bool isPrinting = false;        //���ڸ� ����Ʈ ���̴�
    bool isSentenseSkip = false;    //������ ��ŵ�ϱ� ���� bool ��
    bool isCanTouch = false;

    // Start is called before the first frame update
    void Start()
    {
        //Table�� ���� name, emotion, script�� List�� �����Ѵ�.
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
        //���� ������ �Ѿ�� ����
        //�ؽ�Ʈ ī��Ʈ�� ��ȭ���� ���̰� �������� 
        if (lineNum >= maxTextCount)
        {
            //���Ŵ����� OnNextScene �Լ��� �����Ѵ�.
            Invoke("NextScene", invokeTimer);
        }
    }
    void NextScene()
    {
        gameObject.GetComponent<Scenemanager_Story>().OnNextScene();

    }
    //��ư�� ������ ���� ������ ����Ѵ�.
    public void OnClickNext()
    {
        if (lineNum > 0 && !isPrinting)
            GameObject.Find("TxtClickSound").GetComponent<AudioSource>().Play();

        //�迭 �ʰ��� ���� ���� ��
        if (lineNum >= Table.instance.textLength)
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
            scriptsCharArr = scripts[lineNum].ToCharArray();

            StopCoroutine("IEPrintWord");
            StartCoroutine("IEPrintWord");
        }
    }

    IEnumerator IEPrintWord()
    {
        isPrinting = true;

        int printWordCount = 0;             //���ڰ� �� ����� �Ǹ� textCount�� �ø��� ���� ���� 

        textName.text = names[lineNum];//��� ��ü �̸� ���

        #region ���丮�� ���� ĳ���� ����
        ActEmotion(emotions[lineNum]);
        #endregion

        for (int i = 0; i < scripts[lineNum].Length && !isSentenseSkip; i++)
        {
            printWord += scriptsCharArr[i].ToString();
            textScript.text = printWord;

            printWordCount++;
            yield return new WaitForSeconds(0.05f);
        }

        //���� ������ ��ŵ�ϸ� PrintWordCoutn�� �÷� ���� �������� �Ѿ� �����Ѵ�
        if (isSentenseSkip)
        {
            printWordCount = scripts[lineNum].Length;
        }

        printWord = ""; //printWord ���� �ʱ�ȭ

        textScript.text = scripts[lineNum];
        textName.text = names[lineNum];

        //���ڿ��� �� ����� �Ǿ��ٸ�
        //���� �������� �Ѿ��
        if (printWordCount == scripts[lineNum].Length)
        {
            lineNum++;
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
        textName.text = names[maxTextCount - 1];
        textScript.text = scripts[maxTextCount - 1];
        ActEmotion(emotions[maxTextCount - 1]);
        lineNum = maxTextCount;
    }

    #region ���丮�� ���� ĳ���� ����
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
