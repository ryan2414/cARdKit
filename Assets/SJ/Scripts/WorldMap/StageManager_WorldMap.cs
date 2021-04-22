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
        if (FlagManager.instance.clearFlags[0] == true)
        {
            btnStage[0].gameObject.GetComponent<Image>().color = Color.yellow;
        }
    }

    //1-1�� Ŭ���� �ϸ� 1-1�� ��ư ���� ������ �ְ� �ʹ�.
    public void ButtonColorChange()
    {

    }

}
