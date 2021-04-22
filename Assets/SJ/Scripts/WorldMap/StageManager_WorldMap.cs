using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//만약 AR 씬에서 스테이지를 클리어 했다면
//AR 씬에서 클리어한 정보를 주고
//월드맵에서 스테이지 버튼의 색이 변하게 하고 싶다.
//
public class StageManager_WorldMap : MonoBehaviour
{
    public static StageManager_WorldMap instance;
    private void Awake()
    {
        instance = this;


    }
    //스테이지 버튼 정보를 가지고 온다
    public List<Button> btnStage = new List<Button>();
    private void Start()
    {
        //1-1을 제외한 나머지 스테이지 버튼을 끈다
        for (int i = 1; i < btnStage.Count; i++)
        {
            btnStage[i].interactable = false;
        }

        

    }

    private void Update()
    {
        if (FlagManager.instance.clearFlags[0] == true)
        {
            btnStage[0].gameObject.GetComponent<Image>().color = Color.yellow;
        }
    }

    //1-1을 클리어 하면 1-1의 버튼 색을 변경해 주고 싶다.
    public void ButtonColorChange()
    {

    }

}
