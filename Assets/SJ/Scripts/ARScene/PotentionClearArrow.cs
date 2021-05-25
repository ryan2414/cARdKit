using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentionClearArrow : MonoBehaviour
{
    [SerializeField]
    Switch_MH switch_MH;

    [SerializeField]
    GameObject canvas_ClearArrow;

    // Start is called before the first frame update
    void Start()
    {
        canvas_ClearArrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //����ġ ��ư�� ������ ����ġ�� ȭ��ǥ�� ���� ���������� ����ġ�� Ȱ��ȭ �Ѵ�.
        if (switch_MH.isOn == true)
        {
            switch_MH.GetComponent<Transform>().Find("Canvas_ClearArrow").gameObject.SetActive(false);
            canvas_ClearArrow.SetActive(true);
        }
        //���� Ŭ��� �Ǹ� ���������� ����ġ�� ����.
        if (ClearUIActive_SJ.instance.isStageClear == true)
        {
            canvas_ClearArrow.SetActive(false);

        }
    }
}
