using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


//���� ��ȯ �Ǿ��� �� �������� �ε����� ���� ���� �Ѱ� �ش�.

public class FlagManager : MonoBehaviour
{
    public static FlagManager instance;
    private void Awake()
    {
        instance = this;
      
       
    }

    //���� ��������
    public int stageNum;
    public GameObject stageNumObject;

    //0~7���������� Ŭ���� ���θ� �ľ��ϴ� flag
    public List<bool> clearBool = new List<bool>();

    //��ü �������� ��
    const int TotalStage = 7;

  
    private void Start()
    {
            DontDestroyOnLoad(gameObject);


        //0~7���������� Ŭ���� ���θ� �ľ��ϴ� flag
        for (int i = 0; i < TotalStage; i++)
        {
            clearBool.Add(false);
        }
    }

    private void Update()
    {
        //����Ʈ ����������ŭ ���������� ���ش�
        for (int i = 0; i < PlayerPrefs.GetInt("ClearLevel"); i++)
        {
            clearBool[i] = true;
        }
    }

    public void Call()
    {
        SceneManager.LoadScene("3SJ_Story1-1");
    }
}
