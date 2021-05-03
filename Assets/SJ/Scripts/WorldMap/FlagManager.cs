using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


//씬이 전환 되었을 때 스테이지 인덱스를 다음 씬에 넘겨 준다.

public class FlagManager : MonoBehaviour
{
    public static FlagManager instance;
    private void Awake()
    {
        instance = this;

    }
    public int stageNum;
    public GameObject stageNumObject;

    public void Call()
    {
        SceneManager.LoadScene("3SJ_Story1-1");
    }

    //0~7스테이지의 클리어 여부를 파악하는 flag
    public List<bool> clearBool = new List<bool>();

    //전체 스테이지 수
    const int TotalStage = 8;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //0~7스테이지의 클리어 여부를 파악하는 flag
        for (int i = 0; i < TotalStage; i++)
        {
            clearBool.Add(false);
        }
    }
}
