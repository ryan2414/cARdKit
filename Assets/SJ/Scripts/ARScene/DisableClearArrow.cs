using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableClearArrow : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //���� Ŭ��� �Ǹ� ���������� ����ġ�� ����.
        if (ClearUIActive_SJ.instance.isStageClear == true)
        {
            gameObject.SetActive(false);

        }
    }
}
