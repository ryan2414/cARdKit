using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//회로에 필요한 소자들이 켜지면 컴플리트 UI를 출력해주고 싶다

//Stage 오브젝트 안에 있는 parts들의 정보를 가지고 와서
//그것들이 다 켜지면 클리어를 하고 싶다.

public class CircuitFlag : MonoBehaviour
{
    ////클리어에 필요한 파츠
    //public GameObject bulb;
    //public GameObject power;
    //public GameObject obj_Switch;

    //클리어 판넬
    public GameObject UI_clearPanel;

    //클리어에 필요한 파츠를 배열로 받아오기 위한 리스트
    public List<GameObject> partsList = new List<GameObject>();
    public List<bool> partsON = new List<bool>();

    //한 번만 조건을 만족하면 계속 클리어 창을 띄워 놓기 위한 플래그
    bool isClear;

    private void Start()
    {
        UI_clearPanel.SetActive(false);

        //클리어에 필요한 파츠를 배열로 받아오기
        int partsCount = transform.Find("Parts").gameObject.transform.childCount;
        GameObject parts = transform.Find("Parts").gameObject;
        for (int i = 0; i < partsCount; i++)
        {
            partsList.Add(parts.transform.GetChild(i).gameObject);
            partsON.Add(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isClear)
        {
            //모든 파츠가 켜져있다면 게임 클리어 되기
            for (int i = 0; i < partsList.Count; i++)
            {
                if (partsList[i].activeSelf == true)
                {
                    partsON[i] = true;
                }
                else if (partsList[i].activeSelf == false)
                {
                    partsON[i] = false;
                }
            }

            if (!partsON.Contains(false))
            {
                Invoke("UIActive", 3f);
                isClear = true;
            }
        }

    }


    void UIActive()
    {
        //클리어 UI ON
        UI_clearPanel.SetActive(true);

        //스테이지가 클리어가 되면 그 스테이지가 클리어 됬다는 정보를 보내주고 싶다.
        int _stageNum = FlagManager.instance.stageNum-1;
        FlagManager.instance.clearBool[_stageNum] = true;
    }
}
