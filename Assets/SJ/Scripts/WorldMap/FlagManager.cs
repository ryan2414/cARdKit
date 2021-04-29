using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public static FlagManager instance;
    private void Awake()
    {
        instance = this;

    }

    public List<bool> clearBool = new List<bool>();
    public int stageCount = 0;

    //��ü �������� ��
    const int TotalStage = 8;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //0~7���������� Ŭ���� ���θ� �ľ��ϴ� flag
        for (int i = 0; i < 8; i++)
        {
            clearBool.Add(false);
        }
    }
}
