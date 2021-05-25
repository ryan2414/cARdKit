using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ȸ�ο� �ʿ��� ���ڵ��� ������ ���ø�Ʈ UI�� ������ְ� �ʹ�

//Stage ������Ʈ �ȿ� �ִ� parts���� ������ ������ �ͼ�
//�װ͵��� �� ������ Ŭ��� �ϰ� �ʹ�.

public class CircuitFlag : MonoBehaviour
{
    //Ŭ��� �ʿ��� ������ �迭�� �޾ƿ��� ���� ����Ʈ
    public List<GameObject> partsList = new List<GameObject>();
    public List<bool> partsON = new List<bool>();
    //�� ���� ������ �����ϸ� ��� Ŭ���� â�� ��� ���� ���� �÷���
    bool isClear;

    public GameObject ClearArrow;

    private void Start()
    {
        //UI_Clear.SetActive(false);
        if(ClearArrow != null)
        {
            ClearArrow.SetActive(false);
        }

        //Ŭ��� �ʿ��� ������ �迭�� �޾ƿ���
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
            // ȸ�ΰ� �۵��Ǹ� ���� Ŭ���� �Ǳ�
            if (CircuitManager_MH.instance.isCircuitActivated && CircuitManager_MH.instance.isStageClearSatisfied)
            {
                ClearUIActive_SJ.instance.isStageClear = true;
                isClear = true;
            }


            //��� ������ �����ִٸ� ���� Ŭ���� �Ǳ�
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

        //[Header("Ŭ�����ȭ")]
        //public GameObject img_SH;
        //public GameObject img_ML;
        //public GameObject txt_Sh;
        //public GameObject txt_Ml;
        //public GameObject UI_Clear;
        //public GameObject btn_Clear;

        ////Ŭ���� ������ �����ϸ� Clear UI�� ��縦 ����
        //IEnumerator IEClearUIActive()
        //{
        //    txt_Sh.SetActive(false);
        //    txt_Ml.SetActive(false);
        //    UI_Clear.SetActive(true);
        //    yield return new WaitForSeconds(3f);


        //    //Ŭ���� UI ON
        //    btn_Clear.SetActive(true);

        //    //���������� Ŭ��� �Ǹ� �� ���������� Ŭ���� ��ٴ� ������ �����ְ� �ʹ�.
        //    int _stageNum = FlagManager.instance.stageNum - 1;
        //    FlagManager.instance.clearBool[_stageNum] = true;
        //}
    }
}