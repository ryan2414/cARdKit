using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//회로에 필요한 소자들이 켜지면 컴플리트 UI를 출력해주고 싶다
public class CircuitFlag : MonoBehaviour
{
    public GameObject bulb;
    public GameObject power;
    public GameObject obj_Switch;
    public GameObject UI_clearPanel;
    private void Start()
    {
        UI_clearPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(bulb.activeSelf == true && power.activeSelf == true && obj_Switch.activeSelf == true)
        {
            Invoke("UIActive", 3f);
        }
    }
    //스테이지 변수
    public int stageNumber;
    
    void UIActive()
    {
        UI_clearPanel.SetActive(true);
        FlagManager.instance.stageCount = stageNumber - 1;
        FlagManager.instance.clearBool[FlagManager.instance.stageCount] = true ;
    }
}
