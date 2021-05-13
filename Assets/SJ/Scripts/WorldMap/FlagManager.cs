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

    //현제 스테이지
    public int stageNum;
    public GameObject stageNumObject;

    //0~7스테이지의 클리어 여부를 파악하는 flag
    public List<bool> clearBool = new List<bool>();

    //전체 스테이지 수
    const int TotalStage = 7;

  
    private void Start()
    {
            DontDestroyOnLoad(gameObject);


        //0~7스테이지의 클리어 여부를 파악하는 flag
        for (int i = 0; i < TotalStage; i++)
        {
            clearBool.Add(false);
        }
    }

    private void Update()
    {
        //베스트 스테이지만큼 스테이지를 켜준다
        for (int i = 0; i < TotalStage; i++)
        {
            if(i < PlayerPrefs.GetInt("ClearLevel"))
            {
                clearBool[i] = true;

            }
            else
            {
                clearBool[i] = false;

            }
        }
    }

    public void Call()
    {
        if(stageNum == 4)
        {
            SceneManager.LoadScene("4SJ_StudyScene");
        }
        else if(stageNum == 7)
        {
            SceneManager.LoadScene("5SJ_StudyScene2");
        }
        else
        {
            SceneManager.LoadScene("3SJ_Story1-1");
        }
    }
}
