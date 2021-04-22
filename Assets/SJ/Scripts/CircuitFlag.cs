using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ȸ�ο� �ʿ��� ���ڵ��� ������ ���ø�Ʈ UI�� ������ְ� �ʹ�
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

    void UIActive()
    {
        UI_clearPanel.SetActive(true);
        FlagManager.instance.clearFlags[0] = true ;
    }
}
