using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//회로에 필요한 소자들이 켜지면 컴플리트 UI를 출력해주고 싶다

//Stage 오브젝트 안에 있는 parts들의 정보를 가지고 와서
//그것들이 다 켜지면 클리어를 하고 싶다.

public class CircuitFlag : MonoBehaviour
{
    //클리어에 필요한 파츠를 배열로 받아오기 위한 리스트
    public List<GameObject> partsList = new List<GameObject>();
    public List<bool> partsON = new List<bool>();
    //한 번만 조건을 만족하면 계속 클리어 창을 띄워 놓기 위한 플래그
    bool isClear;

    public GameObject ClearArrow;

    private void Start()
    {
        //UI_Clear.SetActive(false);
        if(ClearArrow != null)
        {
            ClearArrow.SetActive(false);
        }

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
            // 회로가 작동되면 게임 클리어 되기
            if (CircuitManager_MH.instance.isCircuitActivated && CircuitManager_MH.instance.isStageClearSatisfied)
            {
                ClearUIActive_SJ.instance.isStageClear = true;
                isClear = true;
            }


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

            if (!partsON.Contains(false) && ClearArrow != null)
            {
                ClearArrow.SetActive(true);
            }

        }

        //[Header("클리어대화")]
        //public GameObject img_SH;
        //public GameObject img_ML;
        //public GameObject txt_Sh;
        //public GameObject txt_Ml;
        //public GameObject UI_Clear;
        //public GameObject btn_Clear;

        ////클리어 조건을 만족하면 Clear UI와 대사를 띄운다
        //IEnumerator IEClearUIActive()
        //{
        //    txt_Sh.SetActive(false);
        //    txt_Ml.SetActive(false);
        //    UI_Clear.SetActive(true);
        //    yield return new WaitForSeconds(3f);


        //    //클리어 UI ON
        //    btn_Clear.SetActive(true);

        //    //스테이지가 클리어가 되면 그 스테이지가 클리어 됬다는 정보를 보내주고 싶다.
        //    int _stageNum = FlagManager.instance.stageNum - 1;
        //    FlagManager.instance.clearBool[_stageNum] = true;
        //}
    }
}