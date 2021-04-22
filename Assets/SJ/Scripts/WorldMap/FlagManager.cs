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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //0~4���������� Ŭ���� ���θ� �ľ��ϴ� flag
        for (int i = 0; i < 5; i++)
        {
            clearBool.Add(false);
        }
    }
}
