using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public static Table instance;
    private void Awake()
    {
        instance = this;
        TableIndexNameDic = new Dictionary<int, string>();
        TableIndexEmotionDic = new Dictionary<int, string>();
        TalbeIndexScriptDic = new Dictionary<int, string>();
        SetTable();
    }

    public int index { get; set; }
    public string names { get; set; }
    public string emotion { get; set; }
    public string script { get; set; }
    public int textLength;

    Dictionary<int, string> TableIndexNameDic;
    Dictionary<int, string> TableIndexEmotionDic;
    Dictionary<int, string> TalbeIndexScriptDic;

    TextAsset text;

    public void SetTable()
    {
        //world�ʿ��� ���� �ε����� ������ ��
        int stageNum = FlagManager.instance.stageNum;

        //Resocurecs ������ Scripts.cvs������ �ҷ��´�
        //������ ������ ���� �ٸ� ���丮�� �ҷ��´�.
        if (stageNum == 1) text = Resources.Load<TextAsset>("Table");
        else if (stageNum == 2) text = Resources.Load<TextAsset>("Table2");
        else if (stageNum == 3) text = Resources.Load<TextAsset>("Table3");
        else if (stageNum == 4) text = Resources.Load<TextAsset>("Table4");
        else if (stageNum == 5) text = Resources.Load<TextAsset>("Table4");
        else if (stageNum == 6) text = Resources.Load<TextAsset>("Table5");
        else if (stageNum == 7) text = Resources.Load<TextAsset>("Table5");

        string content = text.text;

        //�ٴ����� �ؽ�Ʈ�� �ڸ�
        string[] line = content.Split('\n');

        //2��° �ٺ��� ����
        for (int i = 2; i < line.Length - 1; i++)
        {
            string[] column = line[i].Split(',');
            textLength++;

            int count = 0;
            index = int.Parse(column[count++]); //column[0];
            names = column[count++];             //column[1];
            emotion = column[count++];           //column[2];
            script = column[count++];           //column[3];

            //��� ��ũ��Ʈ�� �ۼ��ϴ� ����� ���ڸ� 1���� �ϴ� ���� �ͼ��� �� ���Ƽ�
            //1���� �����ϵ��� ����
            TableIndexNameDic.Add(index - 1, names);
            TableIndexEmotionDic.Add(index - 1, emotion);
            TalbeIndexScriptDic.Add(index - 1, script);
        }
    }

    public string GetTableScript(int _index)
    {
        if (TalbeIndexScriptDic.ContainsKey(_index))
        {
            return TalbeIndexScriptDic[_index];
        }
        else
        {
            return null;
        }
    }

    public string GetTableName(int _index)
    {
        if (TableIndexNameDic.ContainsKey(_index))
        {
            return TableIndexNameDic[_index];
        }
        else
        {
            return null;
        }
    }

    public string GetTableEmotion(int _index)
    {
        if (TableIndexEmotionDic.ContainsKey(_index))
        {
            return TableIndexEmotionDic[_index];
        }
        else
        {
            return null;
        }
    }

}

