using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableClearArrow : MonoBehaviour
{
    [SerializeField]
    GameObject canvas_ClearArrow;

    // Update is called once per frame
    void Update()
    {
        //���� Ŭ��� �Ǹ� ���������� ����ġ�� ����.
        if (ClearUIActive_SJ.instance.isStageClear == true)
        {
            canvas_ClearArrow.SetActive(false);

        }
    }
}
