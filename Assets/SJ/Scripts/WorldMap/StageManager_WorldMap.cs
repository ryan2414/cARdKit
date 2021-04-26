using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���� AR ������ ���������� Ŭ���� �ߴٸ�
//AR ������ Ŭ������ ������ �ְ�
//����ʿ��� �������� ��ư�� ���� ���ϰ� �ϰ� �ʹ�.
//
public class StageManager_WorldMap : MonoBehaviour
{
    public static StageManager_WorldMap instance;
    private void Awake()
    {
        instance = this;


    }
    //�������� ��ư ������ ������ �´�
    public List<Button> btnStage = new List<Button>();
    int stageTotalNumber;

    private void Start()
    {
        //1-1�� ������ ������ �������� ��ư�� ����
        for (int i = 1; i < btnStage.Count; i++)
        {
            btnStage[i].interactable = false;
        }

    }

    private void Update()
    {
        ButtonColorChange();
    }

    public void ButtonColorChange()
    {
        //1-1�� Ŭ���� �ϸ� 1-1�� ��ư ���� ������ �ְ� �ʹ�.
        //1-2���������� ��ư�� ���� �� �ֵ��� Ȱ��ȭ �Ѵ�.
        //if (FlagManager.instance.clearFlags[0] == true)
        //{
        //    btnStage[0].gameObject.GetComponent<Image>().color = Color.yellow;
        //    btnStage[1].interactable = true;
        //}

        //�ε��� ���� �޾Ƽ�
        //�� ���������� ��ư�� Ȱ��ȭ ���ְ�
        int clearStage = FlagManager.instance.stageCount;

        if (FlagManager.instance.clearBool[clearStage] == true)
        {
            btnStage[clearStage].gameObject.GetComponent<Image>().color = Color.yellow;
            btnStage[clearStage + 1].interactable = true;
        }

    }

}
