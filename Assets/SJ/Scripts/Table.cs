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
        TalbeIndexScriptDic = new Dictionary<int, string>();
        SetTable();
    }

    public int index { get; set; }
    public string names { get; set; }
    public string script { get; set; }
    public int textLength;

    Dictionary<int, string> TableIndexNameDic;
    Dictionary<int, string> TalbeIndexScriptDic;

    private void Start()
    {
        
    }

    public void SetTable()
    {
        //Resocurecs ������ Scripts.cvs������ �ҷ��´�
        TextAsset text = Resources.Load<TextAsset>("Table");
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
            script = column[count++];           //column[2];

            TableIndexNameDic.Add(index - 1, names);
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

}

