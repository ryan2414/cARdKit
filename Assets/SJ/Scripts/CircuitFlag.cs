using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ȸ�ο� �ʿ��� ���ڵ��� ������ ���ø�Ʈ UI�� ������ְ� �ʹ�

//Stage ������Ʈ �ȿ� �ִ� parts���� ������ ������ �ͼ�
//�װ͵��� �� ������ Ŭ��� �ϰ� �ʹ�.

public class CircuitFlag : MonoBehaviour
{
    ////Ŭ��� �ʿ��� ����
    //public GameObject bulb;
    //public GameObject power;
    //public GameObject obj_Switch;

    //Ŭ���� �ǳ�
    public GameObject UI_clearPanel;

    //Ŭ��� �ʿ��� ������ �迭�� �޾ƿ��� ���� ����Ʈ
    public List<GameObject> partsList = new List<GameObject>();
    public List<bool> partsON = new List<bool>();

    //�� ���� ������ �����ϸ� ��� Ŭ���� â�� ��� ���� ���� �÷���
    bool isClear;

    private void Start()
    {
        UI_clearPanel.SetActive(false);

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

            if (!partsON.Contains(false))
            {
                Invoke("UIActive", 3f);
                isClear = true;
            }
        }

    }


    void UIActive()
    {
        //Ŭ���� UI ON
        UI_clearPanel.SetActive(true);

        //���������� Ŭ��� �Ǹ� �� ���������� Ŭ���� ��ٴ� ������ �����ְ� �ʹ�.
        int _stageNum = FlagManager.instance.stageNum-1;
        FlagManager.instance.clearBool[_stageNum] = true;
    }
}
