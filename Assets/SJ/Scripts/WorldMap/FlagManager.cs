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

    //전체 스테이지 수
    const int TotalStage = 8;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //0~7스테이지의 클리어 여부를 파악하는 flag
        for (int i = 0; i < 8; i++)
        {
            clearBool.Add(false);
        }
    }
}
